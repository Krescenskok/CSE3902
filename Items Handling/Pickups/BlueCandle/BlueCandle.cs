﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;



namespace Sprint5.Items
{
    public class BlueCandle : IItems
    {
        private ItemCollider collider;
        private XElement saveInfo;
        private Vector2 location;
        private ISprite item;
        private IItemsState state;
        private int drawnFrame;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }
        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public BlueCandle(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BlueCandleState(this, location);
            collider = new ItemCollider((item as BlueCandleSprite).Hitbox, this, this.state);
        }

        public BlueCandle(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BlueCandleState(this, location);
            collider = new ItemCollider((item as BlueCandleSprite).Hitbox, this, this.state);
            saveInfo = xml;
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
