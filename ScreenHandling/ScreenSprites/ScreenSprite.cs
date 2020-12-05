using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.ScreenHandling.ScreenSprites
{
    public class ScreenSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        private int width;
        private int height;

        public ScreenSprite(Texture2D texture)
        {
            this.Texture = texture;

            width = Camera.Instance.playArea.Size.X;
            height = Camera.Instance.playArea.Size.Y;


        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Rectangle destinationRectangle = new Rectangle(0, 0, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, color);
        }
    }
}
