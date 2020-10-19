using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public interface IEnemyState
    {
        public void TakeDamage(int amount);
        public void ChangeDirection();
        public void MoveAwayFromCollision(Collision collision);
        public void Die();
        public void Attack();

        public void Update();

    }
}
