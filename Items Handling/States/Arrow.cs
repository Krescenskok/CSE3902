﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items.States
{
    public class Arrow : IItemsState
    {
        private Vector2 spriteLocation;
        private ISprite item;
        private int SHEET_LOCATION = 32;
        private int drawnFrame; 
        
        private int xPos = 100;
        private int yPos = 50;

        public Arrow(ISprite item)
        {
            spriteLocation = new Vector2(xPos, yPos);
            this.item = item;
            
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
            yPos--;
            if (yPos < 0)
            {
                yPos = 400;
            }

            spriteLocation = new Vector2(xPos, yPos);

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
