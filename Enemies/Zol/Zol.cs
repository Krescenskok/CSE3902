using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Big gelatinous creature that moves randomly between square spaces on the map.</para>
    /// </summary>
    public class Zol : IEnemy
    {
        public IEnemyState state;
        private ISprite sprite;
        private Vector2 location;

        public Vector2 Location => throw new NotImplementedException();

        public IEnemyState State => throw new NotImplementedException();
        public List<ICollider> Colliders { get => null; }

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
            sprite.Draw(batch, location, 0 , Color.White);
        }

        public void Spawn()
        {
            //
        }

 

        public void TakeDamage(Direction dir, int amount)
        {
            state.TakeDamage(amount);
            
        }

        public void ObstacleCollision(Collision collision)
        {
            state.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            state.Stun(false);
        }

    }
}
