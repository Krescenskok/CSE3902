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
        private int row;

        public BeamImpactSprite(Texture2D texture, int row)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetSwordBeamSheetSize();
            this.row = row;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = (int)texture.Width / 2;
            int height = (int)texture.Height / 3;
            int xLoc = currentFrame * width;
            int yLoc = row * currentFrame * (int)(sheetSize.Y / 3);

            sourceRectangle = new Rectangle(xLoc, yLoc, width, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height, 2 * width, 2 * height);


            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


        }
    }
}
