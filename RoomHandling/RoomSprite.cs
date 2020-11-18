using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Sprint5
{
    public class RoomSprite : ISprite
    {
        private Texture2D texture;
     
        private Point spriteSize;
        private Point drawSize;
       

        private int row;
        private int col;

        public RoomSprite(Texture2D texture, int row, int col, Point size)
        {

            this.texture = texture;
            this.row = row;
            this.col = col;


            spriteSize.X = texture.Bounds.Width / 6;
            spriteSize.Y = texture.Bounds.Height / 6;

            drawSize = size;
        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {


            Rectangle sourceRectangle = new Rectangle(spriteSize.X * col, spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle(0, 0, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }


        public void Load(Game game)
        {

            texture = game.Content.Load<Texture2D>("Rooms");
        }
        
    }
}
