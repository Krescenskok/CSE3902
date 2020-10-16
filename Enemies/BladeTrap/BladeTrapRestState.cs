using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>State of Blade Trap when resting(not moving)</para>
    /// </summary>
    public class BladeTrapRestState : IEnemyState
    {

        private Vector2 location;
        private Vector2 target;
        private BladeTrap trap;

        public BladeTrapRestState(Vector2 location, Vector2 target, BladeTrap trap)
        {
            this.location = location;
            this.target = target;
            this.trap = trap;
        }
        public void Attack()
        {
            trap.state = new BladeTrapAttackState(location,target,trap);
        }

        public void ChangeDirection()
        {
            //do nothing
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            //do nothing
        }

        public void TakeDamage()
        {
            //do nothing
        }

        public void Update()
        {
            Attack();
        }
    }
}
