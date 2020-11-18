using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ExplosionSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 sheetSize;
        private const int buffer = 8;
        private Rectangle hitbox;

        public Rectangle Hitbox { get => hitbox; }

        public ExplosionSprite(Texture2D texture)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetExplosionSheetSize();
            
            int width = texture.Width / (int)sheetSize.Y;

            int height = texture.Height;

            hitbox = new Rectangle(0, 0, width * 2, height * 2);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = (int)texture.Width / (int)sheetSize.Y;
            int height = (int)texture.Height;
            int xLoc = currentFrame * width;
            int yLoc = 0;

            sourceRectangle = new Rectangle(xLoc, yLoc, width, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height, 2 * width, 2 * height);
            destinationRectangle = new Rectangle((int)location.X - width + buffer, (int)location.Y - height / 2, 2 * width, 2 * height);


            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
