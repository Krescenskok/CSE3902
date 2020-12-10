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
        private List<Texture2D> textures;
     
        private Point textureSize;
        private Point drawSize;
       

        private int row;
        private int col;

        public RoomSprite(List<Texture2D> textures, int row, int col)
        {

            this.textures = textures;
            this.row = row;
            this.col = col;

            drawSize = Camera.Instance.playArea.Size;

            textureSize.X = textures[0].Bounds.Width;
            textureSize.Y = textures[0].Bounds.Height;

           
        }




        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {


            Rectangle sourceRectangle = new Rectangle(0, 0, textureSize.X, textureSize.Y);
            Rectangle destinationRectangle = new Rectangle(drawSize.X * col, drawSize.Y * row, drawSize.X, drawSize.Y);

            foreach(Texture2D texture in textures)
            {
                batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            }
            

        }

        public void Draw(SpriteBatch batch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, textureSize.X, textureSize.Y);
            Rectangle destinationRectangle = new Rectangle(drawSize.X * col, drawSize.Y * row, drawSize.X, drawSize.Y);

            for(int i = 0; i < textures.Count; i++)
            {
                batch.Draw(textures[i], destinationRectangle, sourceRectangle, Color.White);
            }

        }

        public void AddTexture(Texture2D texture)
        {
            textures.Add(texture);
        }


        public void Load(Game game)
        {

          
        }
        
    }
}
