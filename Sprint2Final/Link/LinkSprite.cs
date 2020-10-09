using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;

namespace Sprint2.Link
{
    public class LinkSprite : ISprite
    {

        Texture2D texture;
        int TOTAL_ROWS = 7;
        int TOTAL_COLS = 20;

        public LinkSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch,   Vector2 location, int currentFrame, Color color)
        {
            
            int width = texture.Width / TOTAL_COLS;
            int height = texture.Height / TOTAL_ROWS;
            int row = (int)((float)currentFrame / (float)TOTAL_COLS);
            int column = currentFrame % TOTAL_COLS;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2, 2*width, 2*height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }


    }
}
