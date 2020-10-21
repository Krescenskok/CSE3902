using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.EnemyAndNPC.Merchant
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class MerchantSprite : ISprite
    {
        private Texture2D texture;
        private static int[] SpriteSize = { 16, 16 };
        private Vector2 sourcePos;
        private int spriteSizeIndex = 2;
        private Point drawSize;

        public MerchantSprite(Texture2D texture)
        {
            this.texture = texture;
            sourcePos.X = EnemySpriteFactory.GetColumn("Merchant") * SpriteSize[0];
            sourcePos.Y = EnemySpriteFactory.GetRow("Merchant") * SpriteSize[1];
            drawSize.X = SpriteSize[0] * spriteSizeIndex;
            drawSize.Y = SpriteSize[1] * spriteSizeIndex;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color)
        {
            Rectangle sourceRectangle = new Rectangle((int)sourcePos.X, (int)sourcePos.Y, SpriteSize[0], SpriteSize[1]); ;
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public Rectangle getRectangle(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, drawSize.X, drawSize.Y);
        }
    }
}
