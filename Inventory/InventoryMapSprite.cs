using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Inventory
{
    public class InventoryMapSprite : ISprite
    {
        private Texture2D texture;
        private Point size;
        private int column;
        private const int width = 48;

        public InventoryMapSprite(Texture2D texture, int column, Point size)
        {
            this.texture = texture;
            this.size = size;
            this.column = column;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(width * column, 0, width, texture.Height);
            Rectangle destinationRectangle = new Rectangle((int) location.X, (int) location.Y, size.X, size.Y);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White); 

        }

    }
}
