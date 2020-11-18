using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Inventory
{
    public class CursorSprite : ISprite
    {
        private Texture2D texture;
        private Point location;
        private Point size;

        public CursorSprite(Texture2D texture, Point size, Point location)
        {
            this.texture = texture;
            this.location = location;
            this.size = size;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Rectangle destinationRectangle = new Rectangle(this.location.X, this.location.Y, size.X, size.Y);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

    }
}
