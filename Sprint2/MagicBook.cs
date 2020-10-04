﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items.States
{
    public class MagicBook : IItemsState
    {
        private Vector2 spriteLocation;
        private ISprite item;
        private int SHEET_LOCATION = 4;
        private int currentFrame;
        private int drawnFrame; 

        public MagicBook(ISprite item)
        {
            spriteLocation = new Vector2(100, 50);
            this.item = item;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
           
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
