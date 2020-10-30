using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
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

        private Point spriteSize;
        private Point drawSize;

    
       

        public StalfosWalkingSprite(Texture2D texture)
        {
            
            
            currentAnimatedFrame = 0;

            totalAnimatedFrames = 2;

            currentFrame = 0;
            trueFrameCount = totalAnimatedFrames * (originalRate / frameRate);

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

            currentFrame++;
            if (currentFrame == trueFrameCount)
            {
                currentFrame = 0;

            }
            currentAnimatedFrame = currentFrame / (originalRate / frameRate);
            currentAnimatedFrame += startColumn;
        }


        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(), new Point(drawSize.X, drawSize.Y));
        }


    }
}
