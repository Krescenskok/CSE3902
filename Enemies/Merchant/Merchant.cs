using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies;

using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.EnemyAndNPC.Merchant
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Merchant : IEnemyNPC
    {
        private Vector2 MerchantePos;
        private ISprite MerchantSprite;
        

        public Merchant(Vector2 initialPos)
        {
            MerchantePos = initialPos;
            MerchantSprite = EnemySpriteFactory.Instance.CreateMerchantSprite();

            
        }

        public void Die()
        {
            //Merchant wont die
        }


        public void Update()
        {
            //Merchant dont need to update
        }

        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            MerchantSprite.Draw(spriteBatch, MerchantePos, 0, Color.White);
            
        }

    }
}
