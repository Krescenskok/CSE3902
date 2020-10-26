﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class Key : IItems
    {
        private Vector2 location;
        private XElement saveInfo;
        private ItemCollider collider;
        private ISprite item;
        private int drawnFrame;

        private IItemsState state;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public Key(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new KeyState(this, location);
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
            saveInfo.SetElementValue("Alive", "false");
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
