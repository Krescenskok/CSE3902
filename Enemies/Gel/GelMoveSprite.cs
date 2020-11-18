using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class GelMoveSprite : ISprite
    {

        private Texture2D texture;
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row = EnemySpriteFactory.GetRow("Gel");
        private int startColumn = EnemySpriteFactory.GetColumn("Gel");
        private int currentAnimatedFrame;
        private const int totalAnimatedFrames = 2;

        private int currentFrame;
        private int trueFrameCount;

        private const int frameRate = 30;
        private const int maxFrameRate = 60;

        private Point spriteSize;
        private Point drawSize;
        public Point centerOffset;
        public Vector2 centerLocation;

        public GelMoveSprite(Texture2D texture)
        {
            centerLocation = new Vector2();

            currentAnimatedFrame = 0;
            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (maxFrameRate / frameRate);

            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;
            drawSize.X = spriteSize.X * 2;
            drawSize.Y = spriteSize.Y * 2;

            Point tile = GridGenerator.Instance.GetTileSize();
            centerOffset.X = (tile.X - drawSize.X) / 2;
            centerOffset.Y = (tile.Y - drawSize.Y) / 2;
        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {
            int width = (int)spriteSize.X;
            int height = (int)spriteSize.Y;

            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;
            }
            currentAnimatedFrame = currentFrame / (maxFrameRate / frameRate);
            currentAnimatedFrame += startColumn;

            centerLocation = location + centerOffset.ToVector2();

            Rectangle sourceRectangle = new Rectangle(spriteSize.X * currentAnimatedFrame, spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)centerLocation.X, (int)centerLocation.Y, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(), drawSize);
        }

        public Rectangle GetRectangle2()
        {
            Rectangle rect = new Rectangle(new Point(), GridGenerator.Instance.GetTileSize());
            //increase size to ensure constant collision with surrounding blocks
            rect.Width = (int) (rect.Width * 1.5);
            rect.Height = (int)(rect.Height * 1.5);
            return rect;
        }

        
    }
}
