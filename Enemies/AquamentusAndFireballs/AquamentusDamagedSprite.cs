using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    class AquamentusDamagedSprite : ISprite
    {
        Texture2D texture;
        Vector2 aquamentusPos;
        private static int[] spriteSize = { 24, 32 };
        private Vector2 sourcePos;
        private const int NumUpdatePerSec = 60;
        private const int FrameRate = 10;
        private int numUpdatePerFrame = NumUpdatePerSec / FrameRate;
        private int updateCounter = 0;
        private int frameIndex = 3;
        private Point drawSize;
        private int spriteSizeIndex = 3;

        public AquamentusDamagedSprite(Texture2D texture)
        {
            this.texture = texture;
            sourcePos.X = EnemySpriteFactory.GetColumn("Dragon") * spriteSize[0];
            sourcePos.Y = EnemySpriteFactory.GetRow("Dragon") * spriteSize[1];
            drawSize.X = spriteSize[0] * spriteSizeIndex;
            drawSize.Y = spriteSize[1] * spriteSizeIndex;
        }
        public void AttackSprite()
        {
            //do nothing;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            if (updateCounter == numUpdatePerFrame)
            {
                if (frameIndex == 3)
                {
                    frameIndex = 4;
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
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
