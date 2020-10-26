using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class DodongoMovingSprite : ISprite
    {
        Texture2D texture;
        private static int[] horizontalSpriteSize = { 32, 16 };
        private static int[] verticalSpriteSize = { 16, 16 };
        private int[] spriteSize = { 0, 0 };
        private Vector2 sourcePos = new Vector2(0, 60);
        private const int NumUpdatePerSec = 30;
        private const int FrameRate = 6;
        private int numUpdatePerFrame = NumUpdatePerSec / FrameRate;
        private int updateCounter;
        private int frameIndex = 0;
        private int spriteSizeIndex = 2;
        private Point drawSize;
        private string direction;

        public DodongoMovingSprite(Texture2D texture, string direction)
        {
            this.texture = texture;
            this.direction = direction;
            if (direction == "Right" || direction == "Left")
            {
                spriteSize[0] = horizontalSpriteSize[0];
                spriteSize[1] = horizontalSpriteSize[1];
                sourcePos.Y = 60;
                if (direction == "Right")
                {
                    sourcePos.X = 0 * drawSize.X;
                }
                else
                {
                    sourcePos.X = 2 * drawSize.X;
                }
            }
            else
            {
                spriteSize[0] = verticalSpriteSize[0];
                spriteSize[1] = verticalSpriteSize[1];
                sourcePos.Y = 46;
                if (direction == "Forward")
                {
                    sourcePos.X = 0 * drawSize.X;
                }
                else
                {
                    sourcePos.X = 2 * drawSize.X;
                }
            }
            drawSize.X = spriteSize[0] * spriteSizeIndex;
            drawSize.Y = spriteSize[1] * spriteSizeIndex;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            if (updateCounter == numUpdatePerFrame)
            {
                if (frameIndex == 1)
                {
                    frameIndex = -1;
                }
                else
                {
                    frameIndex = 1;
                }
                updateCounter = 0;
                sourcePos.X += frameIndex * spriteSize[0];
            }
            else
            {
                updateCounter++;
            }
            Rectangle sourceRectangle = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, spriteSize[0], spriteSize[1]);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        internal Rectangle GetRectangle(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
        }
    }
}
