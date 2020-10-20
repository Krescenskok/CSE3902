using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items
{
    public class MagicalBoomerang : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private bool throwing;

        public MagicalBoomerang(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new MagicalBoomerangState(this, location, direction);
            throwing = true;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void ThrowBoomerang(bool throwing)
        {
            this.throwing = throwing;
        }

        public void Update()
        {
            if (throwing)
            {
                state.Update();
            }
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
