using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Big gelatinous creature that moves randomly between square spaces on the map.</para>
    /// </summary>
    public class Zol : IEnemyNPC
    {
        public IEnemyState state;
        private ISprite sprite;
        private Vector2 location;

        public Zol(Game game, Vector2 location)
        {
            this.location = location;

            state = new ZolMoveState(this, location, game);

        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }


        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }


        public void Update()
        {
            state.Update();
        }

        public void TakeDamage()
        {
            state.TakeDamage();
        }

        public void Die()
        {
            state.Die();
        }

        public void Draw(SpriteBatch batch, GameTime time)
        {
            sprite.Draw(batch, location, 0 , Color.White);
        }


    }
}
