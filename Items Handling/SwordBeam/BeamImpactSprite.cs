using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class BeamImpactSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 sheetSize;
        private int sheetLocationTop = 76;
        private int sheetLocationBottom = 84;

        public BeamImpactSprite(Texture2D texture)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetSheetSize();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = texture.Width / (int)sheetSize.Y;
            int drawnWidth = width;

            int height = texture.Height / (int)sheetSize.X;
            int row = (int)((float)(sheetLocationTop + currentFrame) / (float)(int)sheetSize.Y);
            int column = (sheetLocationTop + currentFrame) % (int)sheetSize.Y;

            sourceRectangle = new Rectangle(width * column, height * row, drawnWidth, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2 + 20, 2 * drawnWidth, 2 * height + 20);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


            row = (int)((float)(sheetLocationBottom + currentFrame) / (float)(int)sheetSize.Y);
            column = (sheetLocationBottom + currentFrame) % (int)sheetSize.Y;
            sourceRectangle = new Rectangle(width * column, height * row, drawnWidth, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2 - 20, 2 * drawnWidth, 2 * height - 20);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


        }
    }
}
