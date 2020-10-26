﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class BlueRing : IItems
    {
        private XElement saveInfo;
        private ItemCollider collider;
        private Vector2 location;
        private ItemCollider blueRingCollider;
        private ISprite item;
        private IItemsState state;
        private int drawnFrame;
        private bool equipped;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public BlueRing(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BlueRingState(this, location);
            equipped = false;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void EquipOrUnequip()
        {
            this.equipped = !this.equipped;
            
            if (this.equipped)
            {
                //link takes half of damage that he normally would
            }
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
