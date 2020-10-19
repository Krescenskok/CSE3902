using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Small gelatinous creature that moves randomly between square spaces on the map.</para>
    /// </summary>
    public class Gel : IEnemy
    {

        private Vector2 location;
        public IEnemyState state;
        private ISprite sprite;

        public Gel(Game game, Vector2 location)
        {
            this.location = location;

            state = new GelMoveState(this,location,game);

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

        public void TakeDamage(int amount)
        {
            state.TakeDamage(amount);
        }

        public void Die()
        {
            state.Die();
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }

        public void Spawn()
        {
            throw new NotImplementedException();
        }
    }
}
