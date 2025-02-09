﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class DungeonSprite
    {
        private Texture2D texture;

        private Point spriteSize;
        private Point drawSize;


  

        public DungeonSprite(Texture2D texture)
        {

            this.texture = texture;

            Point size = Camera.Instance.playArea.Size;

            spriteSize.X = texture.Bounds.Width;
            spriteSize.Y = texture.Bounds.Height;

            drawSize.X = size.X * 6;
            drawSize.Y = size.Y * 7;
        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {


          //not implemented

        }

        public void Draw(SpriteBatch batch)
        {


            Rectangle sourceRectangle = new Rectangle(0, 0, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle(0, 0, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }


        public void Load(Game game)
        {

            texture = game.Content.Load<Texture2D>("Rooms");
        }

    }
}
