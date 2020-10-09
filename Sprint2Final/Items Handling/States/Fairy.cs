using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items.States
{
    public class Fairy : IItemsState
    {
        private Vector2 spriteLocation;
        private ISprite item;
        private int SHEET_LOCATION = 8;
        private int currentFrame;
        private int drawnFrame; 
        private int yPos = 50;
        private int xPos = 100;
        private bool right = true;
        private bool down = true;

        public Fairy(ISprite item)
        {
            spriteLocation = new Vector2(100, 50);
            this.item = item;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
           if (xPos > 150)
           {
                right = false;
           }
           else if (xPos < 50)
            {
                right = true;
            }

           if (yPos > 100)
            {
                down = false;
            }
           else if (yPos < 20)
            {
                down = true;
            }

            if (right)
            {
                xPos++;
            }
            else
            {
                xPos--;
            }

            if (down)
            {
                yPos++;
            }
            else
            {
                yPos--;
            }
            spriteLocation = new Vector2(xPos, yPos);


            currentFrame++;
            if (currentFrame == 5)
            {
                drawnFrame++;
            }
            else if (currentFrame == 10)
            {
                
                drawnFrame--;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            item.Draw(spriteBatch, spriteLocation, drawnFrame, Color.White);

        }

        public Vector2 GetLocation()
        {
            return spriteLocation;
        }

    }
}
