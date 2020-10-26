using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class Rupee : IItems
    {
        private Vector2 location;
        private ItemCollider rupeeCollider;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;

        public Vector2 Location { get => location; set => location = value; }

        public IItemsState State { get => state; set => state = value; }

        public Rupee(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new RupeeState(this, location);
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
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
