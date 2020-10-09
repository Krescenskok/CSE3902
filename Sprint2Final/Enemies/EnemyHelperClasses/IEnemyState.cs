using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public interface IEnemyState
    {
        public void TakeDamage();
        public void ChangeDirection();
        public void Die();
        public void Attack();

        public void Update();

    }
}
