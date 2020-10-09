using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class BladeTrapSprite : ISprite
    {
        private Texture2D texture;
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row = EnemySpriteFactory.GetRow("Trap");
        private int column = EnemySpriteFactory.GetColumn("Trap");

        private Vector2 spriteSize;
        

        public BladeTrapSprite(Texture2D texture)
        {
            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;
        }

        public void Draw(SpriteBatch batch, Vector2 location, int currentFrame, Color color)
        {
            int width = (int)spriteSize.X;
            int height = (int)spriteSize.Y;


            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width * 2, height * 2);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);


        }

        public void Load(Game game)
        {
            //do nothing
        }
    }
}
