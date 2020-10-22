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
    public class KeeseMoveSprite : ISprite
    {
        public Texture2D texture { get; set; }
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private const int row = 0;
        private const int startColumn = 2;
        private int currentAnimatedFrame;
        private int totalAnimatedFrames;

        private int currentFrame;
        private int trueFrameCount;



        private  int frameRate = 5;
        private const int maxFrameRate = 60;



        private Point spriteSize;
        private Point drawSize;


        public KeeseMoveSprite(Texture2D texture)
        {

            RandomizeFrameRate();

            currentAnimatedFrame = 0;

            totalAnimatedFrames = 2;

            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (maxFrameRate / frameRate);

            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;

            drawSize.X = spriteSize.X * 2;
            drawSize.Y = spriteSize.Y * 2;

        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {
  

            Rectangle sourceRectangle = new Rectangle(spriteSize.X * currentAnimatedFrame, spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

            
            currentAnimatedFrame = currentFrame / (maxFrameRate / frameRate);
            currentAnimatedFrame += startColumn;

            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;
                RandomizeFrameRate();


            }
        }

        public void Load(Game game)
        {
            texture = game.Content.Load<Texture2D>("EnemySpriteSheet");
        }

        public void RandomizeFrameRate()
        {
            Random rand = new Random();
            bool shouldRandomize = rand.Next(0, 2) == 0;
            if (shouldRandomize)
            {
                frameRate = rand.Next(1, 6);
                trueFrameCount = totalAnimatedFrames * (maxFrameRate / frameRate);
            }
            
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(), new Point(drawSize.X, drawSize.Y));
        }
    }
}
