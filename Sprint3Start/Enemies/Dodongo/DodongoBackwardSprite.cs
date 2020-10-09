﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class DodongoBackwardSprite : ISprite
    {
        Texture2D texture;
        private static int[] spriteSize = { 16, 16 };
        private Vector2 sourcePos = new Vector2(0, 43);
        private const int NumUpdatePerSec = 30;
        private const int FrameRate = 5;
        private int numUpdatePerFrame = NumUpdatePerSec / FrameRate;
        private int updateCounter;
        private int frameIndex = 2;
        public DodongoBackwardSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            if (updateCounter == numUpdatePerFrame)
            {
                if (frameIndex == 3)
                {
                    frameIndex = 2;
                }
                else
                {
                    frameIndex = 3;
                }
                updateCounter = 0;
                sourcePos.X = frameIndex * spriteSize[0];
            }
            else
            {
                updateCounter++;
            }
            Rectangle sourceRectangle = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, spriteSize[0], spriteSize[1]);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, spriteSize[0], spriteSize[1]);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Load(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
