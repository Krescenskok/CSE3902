using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class DungeonSprite
    {
        private Texture2D texture;

        private Point spriteSize;
        


  

        public DungeonSprite(Texture2D texture)
        {

            this.texture = texture;
           

            spriteSize.X = texture.Bounds.Width;
            spriteSize.Y = texture.Bounds.Height;

        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {


          //not implemented

        }

        public void Draw(SpriteBatch batch)
        {


            Rectangle sourceRectangle = new Rectangle(0, 0, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle(0, 0, spriteSize.X, spriteSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

        }


        public void Load(Game game)
        {

            texture = game.Content.Load<Texture2D>("Rooms");
        }

    }
}
