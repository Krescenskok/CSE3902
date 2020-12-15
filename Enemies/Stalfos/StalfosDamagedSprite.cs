using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class StalfosDamagedSprite : EnemySprite
    {

        private Texture2D texture;
        private int rows = EnemySpriteFactory.SheetSize()[0];
        private int columns = EnemySpriteFactory.SheetSize()[1];

        private readonly int row = EnemySpriteFactory.GetRow("Stalfos");
        private readonly int startColumn = EnemySpriteFactory.GetColumn("Stalfos");
        private int currentAnimatedFrame = 0;
        private const int totalAnimatedFrames = 2;

        private int currentFrame = 0;
        private int trueFrameCount;



        private const int frameRate = 5;
        private const int originalRate = 60;
        private const int spriteSizeMultiplier = 2;

        private Point spriteSize;
        private Point drawSize;

        



        public StalfosDamagedSprite(Texture2D texture)
        {

            trueFrameCount = totalAnimatedFrames * (originalRate / frameRate);

            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;

            drawSize.X = spriteSize.X * spriteSizeMultiplier;
            drawSize.Y = spriteSize.Y * spriteSizeMultiplier;

        }



        public void Update()
        {
            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;

            }
            currentAnimatedFrame = currentFrame / (originalRate / frameRate);
            currentAnimatedFrame += startColumn;
        }


        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {


            Rectangle sourceRectangle = new Rectangle(spriteSize.X * currentAnimatedFrame, spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.Red);

      
        }


        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(), new Point(drawSize.X, drawSize.Y));
        }

    }
}
