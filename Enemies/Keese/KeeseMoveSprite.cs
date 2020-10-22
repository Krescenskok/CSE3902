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

        

        private Vector2 spriteSize;

        public Vector2 SpriteSize()
        {
            return spriteSize;
        }




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

        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {
            int width = (int)spriteSize.X;
            int height = (int)spriteSize.Y;


            Rectangle sourceRectangle = new Rectangle(width * currentAnimatedFrame, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;
                RandomizeFrameRate();


            }
            currentAnimatedFrame = currentFrame / (maxFrameRate / frameRate);
            currentAnimatedFrame += startColumn;

       
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
    }
}
