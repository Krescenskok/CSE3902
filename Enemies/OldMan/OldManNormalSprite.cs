using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.EnemyAndNPC.OldMan
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class OldManNormalSprite : ISprite
    {
        Texture2D texture;
        private static int[] SpriteSize = { 16, 16 };
        private Vector2 sourcePos;
        private const int NumUpdatePerSec = 30;
        private const int FrameRate = 6;
        private int numUpdatePerFrame = NumUpdatePerSec / FrameRate;
        private int updateCounter;
        private int frameIndex = 0;
        private int spriteSizeIndex = 2;
        private Point drawSize;

        public OldManNormalSprite(Texture2D texture)
        {
            updateCounter = 0;
            this.texture = texture;
            sourcePos.X = EnemySpriteFactory.GetColumn("OldMan") * SpriteSize[0];
            sourcePos.Y = EnemySpriteFactory.GetRow("OldMan") * SpriteSize[1];
            drawSize.X = SpriteSize[0] * spriteSizeIndex;
            drawSize.Y = SpriteSize[1] * spriteSizeIndex;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            if (updateCounter == numUpdatePerFrame)
            {
                if (frameIndex == 0)
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
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public Rectangle getRectangle(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
        }

    }
}
