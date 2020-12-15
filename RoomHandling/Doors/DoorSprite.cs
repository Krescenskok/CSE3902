using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class DoorSprite : ISprite
    {
        private Texture2D texture;

   

        public bool shouldDraw = true;
        private Point spriteSize;
        private Point drawSize;

        int row, col;


        public DoorSprite(Texture2D texture, int row, int col)
        {

            this.texture = texture;
            this.row = row;
            this.col = col;


            spriteSize.X = texture.Bounds.Width;
            spriteSize.Y = texture.Bounds.Height;


            drawSize = Camera.Instance.playArea.Size;
        }





        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {


            //not implemented

        }

        public void Draw(SpriteBatch batch)
        {

            if(shouldDraw)
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, spriteSize.X, spriteSize.Y);
                Rectangle destinationRectangle = new Rectangle(drawSize.X * col, drawSize.Y * row, drawSize.X, drawSize.Y);

                batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            }
            

        }


    }
}
