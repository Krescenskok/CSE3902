using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items
{
    public class Heart : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int currentFrame = 0;
        private int drawnFrame;
        private IItemsState state;

        public Heart(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new HeartState(this, location);
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
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

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
