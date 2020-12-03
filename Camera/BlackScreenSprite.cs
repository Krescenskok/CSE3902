using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class BlackScreenSprite : ISprite
    {
        private Texture2D texture;
        public BlackScreenSprite(Texture2D texture)
        {
            this.texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle destination = new Rectangle(location.ToPoint(), new Point(texture.Width, texture.Height));
            spriteBatch.Draw(texture,destination,color);
        }
    }
}
