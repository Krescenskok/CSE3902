﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Items
{
    public class BombObject : IItems
    {
        private Vector2 location;
        private BombCollider collider;
        private ISprite item;
        private IItemsState state;
        private int drawnFrame;
        private bool isExploding = false;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        public bool Exploding { get => isExploding; }

        public BombObject(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BombObjectState(this, location);
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }


        public void Update()
        {
            
        }

        public void Expire()
        {

            CollisionHandler.Instance.RemoveCollider(collider);

            UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Collect()
        {
            state.Collected();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
