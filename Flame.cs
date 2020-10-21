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
    class Flame : INPC
    {
        private Vector2 flamePos;
        private FlameSprite flameSprite;
        private NPCCollider flameCollider;
        public Flame(Vector2 initialPos, Texture2D texture)
        {
            flamePos = initialPos;
            flameSprite =(FlameSprite) EnemySpriteFactory.Instance.CreateFlameSprite();
            flameCollider =new NPCCollider(flameSprite.getRectangle(initialPos));
        }

        public void Update()
        {
            //do nothing
        }
        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            flameSprite.Draw(spriteBatch, flamePos, 0, Color.White);
        }

        public NPCCollider GetCollider()
        {
            return flameCollider;
        }
    }
}
