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

        public BeamImpactSprite(Texture2D texture)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetSwordBeamSheetSize();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = (int)texture.Width / 2;
            int height = (int)texture.Height / 3;
            int xLoc = currentFrame * width;
            int yLoc = (currentFrame) % (int)sheetSize.Y;

            sourceRectangle = new Rectangle(xLoc, yLoc, width, height);
            destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height, 2 * width, 2 * height);


            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


        }
    }
}
