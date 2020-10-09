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
    public class StalfosSpawnSprite : ISprite
    {
        private Texture2D texture;

        private int rows = EnemySpriteFactory.SheetSize()[0];
        private int columns = EnemySpriteFactory.SheetSize()[1];

        private readonly int row = EnemySpriteFactory.GetRow("Stalfos");
        private readonly int startColumn = EnemySpriteFactory.GetColumn("Stalfos");
        private int currentColumn;

        private int CurrentFrame;
        private int TotalFrames;

        private Vector2 location;

        private StalfosSpawnState returnState;

      

        public StalfosSpawnSprite(StalfosSpawnState state, Texture2D texture)
        {
            returnState = state;
            
            this.texture = texture;

           
            currentColumn = startColumn;
            TotalFrames = 60;
        }


        public void Draw(SpriteBatch batch, Vector2 location, int curFrame, Color color)
        {
            if (this.location.Equals(new Vector2(0, 0)))
            {
                this.location = location;
            }


            if (CurrentFrame == TotalFrames)
            {
                returnState.FinishSpawning(this.location);
            }

            int width = texture.Width / columns;
            int height = texture.Height / rows;



            Rectangle sourceRectangle = new Rectangle(width * currentColumn, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)this.location.X, (int)this.location.Y, width / 2, height / 2);

            batch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);



            CurrentFrame++;
            currentColumn = CurrentFrame / 10;
        }

        public void Load(Game game)
        {
           //do nothing
        }
    }
}
