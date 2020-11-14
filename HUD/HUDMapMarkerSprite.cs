using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class HUDMapMarkerSprite : ISprite
    {
        private Texture2D texture;
        private Point location;

        public HUDMapMarkerSprite(Texture2D texture, Point location)
        {
            this.texture = texture;
            this.location = location;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Rectangle destinationRectangle = new Rectangle(this.location.X, this.location.Y, texture.Width * 2, texture.Width * 2);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }

    }
}
