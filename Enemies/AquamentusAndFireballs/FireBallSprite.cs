using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Sprint5.EnemyAndNPC.AquamentusAndFireballs
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class FireBallSprite : ISprite
    {
        private static int[] spriteSize = { 8, 10 };
        Texture2D texture;
        Rectangle sourceRectangle;
        private Vector2 sourcePos;
        private Point drawSize;
        private int spriteSizeIndex = 2;

        public FireBallSprite(Texture2D texture)
        {
            this.texture = texture;
            sourcePos.X = EnemySpriteFactory.GetColumn("FireBall");
            sourcePos.Y = EnemySpriteFactory.GetRow("FireBall");
            sourceRectangle = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, spriteSize[0], spriteSize[1]);
            drawSize.X = spriteSize[0] * spriteSizeIndex;
            drawSize.Y = spriteSize[1] * spriteSizeIndex;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Load(Game game)
        {
            //do nothing
        }

        public Rectangle GetRectangle(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
        }
    }
}
