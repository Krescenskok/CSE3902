using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class Flame : INPCState
    {
        private Vector2 flamePos;
        private ISprite flameSprite;

        public Flame(Vector2 initialPos, Texture2D texture)
        {
            flamePos = initialPos;
            flameSprite = EnemySpriteFactory.Instance.CreateFlameSprite();
        }

        public void Die()
        {
            //Falme wont die
        }

        public void Update()
        {
            //do nothing
        }
        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            flameSprite.Draw(spriteBatch, flamePos, 0, Color.White);
        }
    }
}
