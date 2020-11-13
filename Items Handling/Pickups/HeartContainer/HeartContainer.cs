﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace Sprint4.Items
{
    public class HeartContainer : IItems
    {
        private XElement saveInfo;
        private HealthCollider collider;
        private ISprite item;
        private int drawnFrame;

        private Vector2 location;

        private IItemsState state;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public HeartContainer(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;

            state = new HeartContainerState(this, location);
            collider = new HealthCollider((item as HeartContainerSprite).Hitbox, this, this.state, "HeartContainer");
        }
        public HeartContainer(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;

            state = new HeartContainerState(this, location);
            collider = new HealthCollider((item as HeartContainerSprite).Hitbox, this, this.state, "HeartContainer");
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

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
