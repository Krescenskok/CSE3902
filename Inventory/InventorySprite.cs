using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Sprint5
{
    public class InventorySprite : ISprite
    {
        private Texture2D texture;
        private Point size;

        public Point InventorySize
        {
            get { return size; }
        }

        public InventorySprite(Texture2D texture, Point size)
        {
            this.texture = texture;
            this.size = Camera.Instance.HUDArea.Size;
        }


        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Rectangle destinationRectangle = new Rectangle(0, 0, size.X, size.Y);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
