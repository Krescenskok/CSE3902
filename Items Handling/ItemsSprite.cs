using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3;

namespace Sprint3.Items
{

    public class ItemsSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private int columns = 8;
        private int rows = 7;

        public ItemsSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = texture.Width / columns;
            int drawnWidth = AdjustWidth(width, currentFrame);

            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            sourceRectangle = new Rectangle(width * column, height * row, drawnWidth, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2, 2 * drawnWidth, 2 * height);

           
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
    

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 spriteLocation, int drawnFrame)
        {
            int width = texture.Width / columns;
            int drawnWidth = AdjustWidth(width, drawnFrame);

            int height = texture.Height / rows;
            int row = (int)((float)drawnFrame / (float)columns);
            int column = drawnFrame % columns;

            sourceRectangle = new Rectangle(width * column, height * row, drawnWidth, height);
            destinationRectangle = new Rectangle((int)spriteLocation.X - width / 2, (int)spriteLocation.Y - height / 2, 2 * drawnWidth, 2 * height);

            
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
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2, 2 * drawnWidth, 2 * height);

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

    }
}
