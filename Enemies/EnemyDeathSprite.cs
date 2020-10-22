using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public class EnemyDeathSprite : ISprite
    {

        private Texture2D texture;
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row = EnemySpriteFactory.GetRow("EnemyDeath");
        private int startColumn = EnemySpriteFactory.GetColumn("EnemyDeath");
        private int currentAnimatedFrame;
        private const int totalAnimatedFrames = 3;

        private int currentFrame;
        private int trueFrameCount;

        private const int frameRate = 15;
        private const int maxFrameRate = 60;

        private Vector2 spriteSize;
        private int width;
        private int height;
        private Point drawSize;

        private bool done;

        public EnemyDeathSprite(Texture2D texture)
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


            done = false;
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
                done = true;
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

        public bool Done()
        {
            return done;
        }
    }
}
