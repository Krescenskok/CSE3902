using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint2
{
    /// <summary>
    /// Sprite class for when stalfos (skeleton) is walking normally
    /// </summary>
    class StalfosWalkingSprite : ISprite
    {


        private Texture2D texture;
        private int rows = EnemySpriteFactory.SheetSize()[0];
        private int columns = EnemySpriteFactory.SheetSize()[1];

        private readonly int row = EnemySpriteFactory.GetRow("Stalfos");
        private readonly int startColumn = EnemySpriteFactory.GetColumn("Stalfos");
        private int currentAnimatedFrame;
        private int totalAnimatedFrames;

        private int currentFrame;
        private int trueFrameCount;

        

        private const int frameRate = 5;
        private const int originalRate = 60;

        private Vector2 spriteSize;

        public Vector2 SpriteSize()
        {
            return spriteSize;
        }
        
       


        public StalfosWalkingSprite(Texture2D texture)
        {
            
            
            currentAnimatedFrame = 0;

            totalAnimatedFrames = 2;

            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (originalRate / frameRate);

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

            }
            currentAnimatedFrame = currentFrame / (originalRate / frameRate);
            currentAnimatedFrame += startColumn;
        }

        public void Load(Game game)
        {
            texture = game.Content.Load<Texture2D>("EnemySpriteSheet");
        }

        
    }
}
