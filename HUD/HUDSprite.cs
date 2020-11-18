using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sprint5
{
    public class HUDSprite : ISprite
    {
        private Texture2D texture;
        private Point size;
        private const int YCOOR = 159;

        public HUDSprite(Texture2D texture, Point size)
        {
            this.texture = texture;
            this.size = size;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, YCOOR, texture.Bounds.Width, texture.Bounds.Height - YCOOR);
            Rectangle destinationRectangle = new Rectangle(0, 0, size.X, size.Y);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }

    }
}
