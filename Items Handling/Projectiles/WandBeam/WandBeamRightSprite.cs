using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class WandBeamRightSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 sheetSize;
        private const int sheetLocation = 6;

        private Rectangle hitbox;

        public Rectangle Hitbox { get => hitbox; }

        public WandBeamRightSprite(Texture2D texture)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetWandBeamSheetSize();

            int width = texture.Width / (int)sheetSize.Y;

            int height = texture.Height / (int)sheetSize.X;

            hitbox = new Rectangle(0, 0, width * 2, height * 2);
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
