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
    class FlameSprite : ISprite
    {
        private Texture2D texture;
        private static int[] SpriteSize = { 16, 16 };
        private Vector2 sourcePos;
        private const int NumUpdatePerSec = 30;
        private const int FrameRate = 5;
        private int numUpdatePerFrame = NumUpdatePerSec / FrameRate;
        private int updateCounter;
        private int frameIndex = 0;

        public FlameSprite(Texture2D texture)
        {
            updateCounter = 0;
            this.texture = texture;
            sourcePos.X = EnemySpriteFactory.GetColumn("Flame") * SpriteSize[0];
            sourcePos.Y = EnemySpriteFactory.GetRow("Flame") * SpriteSize[1];
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            if(updateCounter == numUpdatePerFrame)
            {
                if(frameIndex == 0)
                {
                    frameIndex = 1;
                }
                else
                {
                    frameIndex = 0;
                }
                updateCounter = 0;
                sourcePos.X = frameIndex * SpriteSize[0];
            }
            else
            {
                updateCounter++;
            }
            Rectangle sourceRectangle = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, SpriteSize[0], SpriteSize[1]);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, SpriteSize[0], SpriteSize[1]);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Load(Game game)
        {
            //do nothing
        }
    }
}
