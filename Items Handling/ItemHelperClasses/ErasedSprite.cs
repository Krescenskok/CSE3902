﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final.Items_Handling.ItemHelperClasses
{
    public class ErasedSprite : ISprite
    {

        Texture2D texture;
        private Rectangle sourceRectangle;
        private Rectangle destinationRectangle;
        private const int zero = 0;

        public ErasedSprite(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            sourceRectangle = new Rectangle(zero, zero, zero, zero);
            destinationRectangle = new Rectangle((int) location.X, (int) location.Y, zero, zero);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

    }
}
