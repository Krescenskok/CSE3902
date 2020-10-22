using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class RopeMoveSprite : ISprite
    {

        private Texture2D texture;
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row = EnemySpriteFactory.GetRow("Rope");
        private int startColumn = EnemySpriteFactory.GetColumn("Rope");
        private int currentAnimatedFrame;
        private const int totalAnimatedFrames = 2;

        private int currentFrame;
        private int trueFrameCount;

        private const int frameRate = 5;
        private const int maxFrameRate = 60;

        private Vector2 spriteSize;
        private int width;
        private int height;
        private Point drawSize;

        public RopeMoveSprite(Texture2D texture, string dir)
        {

            currentAnimatedFrame = 0;
            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (maxFrameRate / frameRate);

            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;

            width = (int)spriteSize.X;
            height = (int)spriteSize.Y;
            drawSize = new Point(width * 2, height * 2);


            if (dir.Equals("left"))
            {
                row = EnemySpriteFactory.GetRow("RopeLeft");
                startColumn = EnemySpriteFactory.GetColumn("RopeLeft");
            }
            
        }

        public Rectangle GetRectangle()
        {
           return new Rectangle(new Point(), new Point(drawSize.X, drawSize.Y));
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


            Rectangle sourceRectangle = new Rectangle(width * currentAnimatedFrame, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

        public void Load(Game game)
        {
            texture = game.Content.Load<Texture2D>("EnemySpriteSheet");
        }
    }
}
