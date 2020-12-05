using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class BladeTrapSprite : EnemySprite
    {
        private Texture2D texture;
        private static int[] spriteSheetSize = EnemySpriteFactory.SheetSize();
        private int rows = spriteSheetSize[0];
        private int columns = spriteSheetSize[1];

        private int row = EnemySpriteFactory.GetRow("Trap");
        private int column = EnemySpriteFactory.GetColumn("Trap");

        private Point spriteSize;
        private Point drawSize;

        public BladeTrapSprite(Texture2D texture)
        {
            this.texture = texture;

            spriteSize.X = texture.Width / columns;
            spriteSize.Y = texture.Height / rows;
            drawSize.X = spriteSize.X * 2;
            drawSize.Y = spriteSize.Y * 2;
        }

        public void Draw(SpriteBatch batch, Vector2 location, int currentFrame, Color color)
        {
           
            Rectangle sourceRectangle = new Rectangle(spriteSize.X * column, spriteSize.Y * row, spriteSize.X, spriteSize.Y);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);

            
        }

        public void Update()
        {

        }


        public Rectangle GetRectangle()
        {
            return new Rectangle(new Point(), drawSize);
        }
    }
}
