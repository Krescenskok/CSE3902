using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5;

namespace Sprint5.Blocks

{
    public class BlocksSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private int columns = 4;
        private int offset = 2;
        private int rows = 3;
        public int blockDimensionX;
        public int blockDimensionY;

        public BlocksSprite(Texture2D texture)
        {
            this.texture = texture;

            blockDimensionX = GridGenerator.Instance.GetTileSize().X;
            blockDimensionY = GridGenerator.Instance.GetTileSize().Y;

            destinationRectangle = new Rectangle(new Point(), new Point(blockDimensionX, blockDimensionY));
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {



            int width = texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            sourceRectangle = new Rectangle(width * column, height * row, width, height);
            destinationRectangle = new Rectangle((int)location.X, (int)location.Y+offset, blockDimensionX, blockDimensionY);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 spriteLocation, int drawnFrame)
        {
            int width = texture.Width / columns;
          

            int height = texture.Height / rows;
            int row = (int)((float)drawnFrame / (float)columns);
            int column = drawnFrame % columns;

            sourceRectangle = new Rectangle(width * column, height * row, width, height);
            destinationRectangle = new Rectangle((int)spriteLocation.X , (int)spriteLocation.Y+offset , blockDimensionX, blockDimensionY);

            
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
          
        }

        public void DrawRotated(SpriteBatch spriteBatch, Vector2 location, int currentFrame, float angle)
        {
            int width = texture.Width / columns;
            int drawnWidth = AdjustWidth(width, currentFrame);
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            // Vector2 spriteOrigin = new Vector2(width * column + (width / 2), height * row + (height/2));

            Vector2 spriteOrigin = new Vector2(0, 0);

            sourceRectangle = new Rectangle(width * column, height * row, drawnWidth, height);
            destinationRectangle = new Rectangle((int)location.X , (int)location.Y , blockDimensionX, blockDimensionY);

            spriteBatch.Begin();
            spriteBatch.Draw(texture, location, sourceRectangle, Color.White, angle, spriteOrigin, 1.0f, SpriteEffects.None, 1);
            spriteBatch.End();

        }

        private int AdjustWidth(int width, int frame)
        {
            if (frame == 6 || frame == 14 || frame == 22 || frame == 30 || frame == 38)
            {
                return width * 2;
            }
            return width;
        }

        public Rectangle getDestination()
        {
            return destinationRectangle;
        }

        public Rectangle getDestination(Vector2 location)
        {
            return destinationRectangle;
        }

    }
}
