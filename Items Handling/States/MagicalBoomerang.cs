using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items.States
{
    public class MagicalBoomerang : IItemsState
    {
        private Vector2 spriteLocation;
        private ISprite item;
        private int SHEET_LOCATION = 59;
        private int currentFrame;
        private int drawnFrame; 
        private bool down = true;
        private int xPos = 100;
        private int yPos = 50;

        public MagicalBoomerang(ISprite item)
        {
            spriteLocation = new Vector2(xPos, yPos);
            this.item = item;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
            if (down)
            {
                yPos++;
            }
            else
            {
                yPos--;
            }

            if (yPos > 150)
            {
                down = false;
            }
            else if (yPos < 50)
            {
                down = true;
            }

            spriteLocation = new Vector2(xPos, yPos);

            currentFrame++;
            if (currentFrame == 5 || currentFrame == 10 || currentFrame == 15)
            {
                drawnFrame++;
            }
            else if (currentFrame == 20)
            {
                
                drawnFrame = SHEET_LOCATION; 
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }

        public Vector2 GetLocation()
        {
            return spriteLocation;
        }

    }
}
