﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Items
{
    //this is the full heart for link's lives, not the collectable object
    public class FullHeart : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private ItemCollider collider;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; set => location = value; }

        public IItemsState State { get => state; set => state = value; }

        public FullHeart(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new FullHeartState(this);
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Update()
        {
            state.Update();
        }

        public void Expire()
        {
            state.Expire();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
