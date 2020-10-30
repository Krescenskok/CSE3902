using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class GoriyaDamagedSprite : ISprite
    {

        public Texture2D texture { get; set; }
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row;
        private int startColumn;
        private int previousAnimatedFrame;
        private int currentAnimatedFrame;
        private const int totalAnimatedFrames = 2;
        private const int frameOffsets = 3;
        private int currentOffset = 0;

        private int currentFrame;
        private int trueFrameCount;

        private const int frameRate = 20;
        private const int maxFrameRate = 60;

        private Point spriteSize;
        private Point drawSize;

        public GoriyaDamagedSprite(Texture2D texture, string sheetID)
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


            
            currentAnimatedFrame = currentFrame / (maxFrameRate / frameRate);
            if (currentAnimatedFrame != previousAnimatedFrame) currentOffset++;
            if (currentOffset > frameOffsets) currentOffset = 0;
            previousAnimatedFrame = currentAnimatedFrame;

            currentAnimatedFrame += startColumn + currentOffset;

            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;
            }
            

            Rectangle sourceRectangle = new Rectangle(spriteSize.X * currentAnimatedFrame, spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

    }
}
