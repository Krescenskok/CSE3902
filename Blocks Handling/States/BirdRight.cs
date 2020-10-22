using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Final.Blocks
{
    public class BirdRight : IBlockState
    {
        private Vector2 spriteLocation;
        private ISprite block;
        private int SHEET_LOCATION = 3;
        private int currentFrame;
        private int drawnFrame; 

        public BirdRight(ISprite block)
        {
            spriteLocation = new Vector2(300, 50);
            this.block = block;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
           
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            block.Draw(spriteBatch, spriteLocation, drawnFrame, Color.White);

        }

        public Vector2 GetLocation()
        {
            return spriteLocation;
        }

    }
}
