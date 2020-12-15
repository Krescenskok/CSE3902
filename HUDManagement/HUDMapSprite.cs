using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.HUDManagement
{
    public class HUDMapSprite : ISprite
    {
        private Texture2D texture;
        private Point size;
        private int row;
        private int column;
        private const int width = 47;
        private const int height = 23;

        public HUDMapSprite(Texture2D texture, int row, int column, Point size)
        {
            this.texture = texture;
            this.size = size;
            this.row = row;
            this.column = column;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int) location.X, (int) location.Y, size.X, size.Y);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White); 

        }

    }
}
