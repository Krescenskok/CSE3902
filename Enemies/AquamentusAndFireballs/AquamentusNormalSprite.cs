using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class AquamentusNormalSprite : ISprite
    {
        Texture2D texture;
        Vector2 aquamentusPos;
        private static int[] spriteSize = {24, 32};
        private Vector2 sourcePos;
        private const int NumUpdatePerSec = 30;
        private const int FrameRate = 6;
        private int numUpdatePerFrame = NumUpdatePerSec / FrameRate;
        private int updateCounter;
        private int frameIndex = 3;
        public AquamentusNormalSprite(Texture2D texture)
        {
            this.texture = texture;
            sourcePos.X = EnemySpriteFactory.GetColumn("Dragon") * spriteSize[0];
            sourcePos.Y = EnemySpriteFactory.GetRow("Dragon") * spriteSize[1];
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            if (updateCounter == numUpdatePerFrame)
            {
                if (frameIndex == 2)
                {
                    frameIndex = 3;
                }
                else
                {
                    frameIndex = 2;
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
            //do nothing
        }
    }
}
