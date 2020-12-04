using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class WinSprite : ISprite
    {

        public Texture2D Texture { get; set; }
        private int width;
        private int height;

        public WinSprite(Texture2D texture)
        {
            this.Texture = texture;


            width = texture.Width;

            height = texture.Height;

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {

            width = Texture.Width;
            height = Texture.Height;


            Rectangle sourceRectangle = new Rectangle(width, height, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, 0,0);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
        }
    }
}
