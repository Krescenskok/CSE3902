using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    class GoriyaWalkSprite : ISprite
    {
        public Texture2D texture { get; set; }
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row;
        private int startColumn;
        private int currentAnimatedFrame;
        private const int totalAnimatedFrames = 2;

        private int currentFrame;
        private int trueFrameCount;

        private const int frameRate = 10;
        private const int maxFrameRate = 60;

        private Point spriteSize;
        private Point drawSize;

        public GoriyaWalkSprite(Texture2D texture, string sheetID)
        {
            currentAnimatedFrame = 0;
            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (maxFrameRate / frameRate);

            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;

            drawSize.X = spriteSize.X * 2;
            drawSize.Y = spriteSize.Y * 2;

            row = EnemySpriteFactory.GetRow(sheetID);
            startColumn = EnemySpriteFactory.GetColumn(sheetID);
        }


        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {
            

            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;
            }
            currentAnimatedFrame = currentFrame / (maxFrameRate / frameRate);

            currentAnimatedFrame += startColumn;

            Rectangle sourceRectangle = new Rectangle(spriteSize.X * currentAnimatedFrame,spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X , drawSize.Y );

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
          
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(), new Point(drawSize.X, drawSize.Y));
        }


    }
}
