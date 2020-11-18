using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    class DodongoDamagedSprite : ISprite
    {
        Texture2D texture;
        private static int[] horizontalSpriteSize = { 32, 16 };
        private static int[] verticalSpriteSize = { 16, 16 };
        private int[] spriteSize = { 0, 0 };
        private Vector2 sourcePos = new Vector2(0, 60);
        private static int spriteSizeIndex = 2;
        private Point drawSize;
        private string direction;

        public DodongoDamagedSprite(Texture2D texture, string direction)
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
                    sourcePos.X = 4 * drawSize.X;
                }
                else
                {
                    sourcePos.X = 5 * drawSize.X;
                }
            }
            else
            {
                spriteSize[0] = verticalSpriteSize[0];
                spriteSize[1] = verticalSpriteSize[1];
                sourcePos.Y = 46;
                if(direction == "Forward")
                {
                    sourcePos.X = 4 * drawSize.X;
                }
                else
                {
                    sourcePos.X = 5 * drawSize.X;
                }
            }
            drawSize.X = spriteSize[0] * spriteSizeIndex;
            drawSize.Y = spriteSize[1] * spriteSizeIndex;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, spriteSize[0], spriteSize[1]);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
