using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BombSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 sheetSize;
        private int sheetLocation = 36;

        public BombSprite(Texture2D texture)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetSheetSize();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = texture.Width / (int)sheetSize.Y;
            int drawnWidth = width;

            int height = texture.Height / (int)sheetSize.X;
            int row = (int)((float)(sheetLocation + currentFrame) / (float)(int)sheetSize.Y);
            int column = (sheetLocation + currentFrame) % (int)sheetSize.Y;

            sourceRectangle = new Rectangle(width * column, height * row, drawnWidth, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2, 2 * drawnWidth, 2 * height);


            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
