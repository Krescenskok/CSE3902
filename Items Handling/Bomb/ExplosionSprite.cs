﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.Items
{
    public class ExplosionSprite : ISprite
    {
        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private Vector2 sheetSize;
        private const int buffer = 8;

        public ExplosionSprite(Texture2D texture)
        {
            this.texture = texture;
            sheetSize = ItemsFactory.Instance.GetExplosionSheetSize();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            int width = (int) sheetSize.X;
            int height = (int) sheetSize.Y / 2;
            int xLoc = 0;
            int yLoc = currentFrame * (int)(sheetSize.Y / 2 + 2);

            sourceRectangle = new Rectangle(xLoc, yLoc, width, height);
            destinationRectangle = new Rectangle((int)location.X - width + buffer, (int)location.Y - height / 2, 2 * width, 2 * height);


            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
