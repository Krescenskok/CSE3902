﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5;

namespace Sprint5.Link
{
    public class LinkSprite : ISprite
    {

        Texture2D texture;
        int TOTAL_ROWS = 7;
        int TOTAL_COLS = 20;
        public Rectangle hitbox;

        public LinkSprite(Texture2D texture)
        {
            this.texture = texture;


            int width = texture.Width / TOTAL_COLS;

            int height = texture.Height / TOTAL_ROWS;

            hitbox = new Rectangle(0, 0, width, height);
        }

        public void Draw(SpriteBatch spriteBatch,   Vector2 location, int currentFrame, Color color)
        {
            
            int width = texture.Width / TOTAL_COLS;
            int height = texture.Height / TOTAL_ROWS;
            int row = (int)((float)currentFrame / (float)TOTAL_COLS);
            int column = currentFrame % TOTAL_COLS;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X - width / 2, (int)location.Y - height / 2, 2*width, 2*height);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color);
        }


    }
}
