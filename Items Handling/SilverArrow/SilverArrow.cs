using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2
{
    public class SilverArrow : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;

        private float xPos;
        private float yPos;
        private float initialY;

        public SilverArrow(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            xPos = location.X;
            yPos = location.Y;
            initialY = yPos;
            this.item = item;
            state = new SilverArrowState(this, location, direction);

            SetFrame(direction);
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Update()
        {
            state.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }

        public void SetFrame(string direction)
        {
            if (direction == "right")
            {
                drawnFrame = 1;
            }
            else if (direction == "left")
            {
                drawnFrame = 3;
            }
            else if (direction == "down")
            {
                drawnFrame = 2;
            }
            else
            {
                drawnFrame = 0;
            }
        }

    }
}
