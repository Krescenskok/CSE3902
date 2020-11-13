using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
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
        private int attackLength;

        public BladeTrapRestState(Vector2 location,BladeTrap trap)
        {
            this.location = location;
            trap.SetSprite(EnemySpriteFactory.Instance.CreateBladeTrapSprite());
            this.trap = trap;
        }

        public BladeTrapRestState()
        {
        }

        public void SetDirection(Vector2 newDirection, int length)
        {
            target = newDirection;
            attackLength = length;

        }

        #region //unused methods
        public void Attack()
        {
            trap.state = new BladeTrapAttackState(location,target,trap,attackLength);
        }

        public void ChangeDirection()
        {
            //do nothing
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            //do nothing
        }

        public void Die()
        {
            //do nothing
        }

        public void Update()
        {
            //wait
        }

        public void TakeDamage(int amount)
        {
           //cannot be damaged
        }

        public void Stun()
        {
            //can't be stunned
        }

        #endregion
    }
}
