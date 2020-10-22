using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>State of Blade Trap when it's attacking. Moves to a target Vector2 and returns.</para>
    /// </summary>
    public class BladeTrapAttackState : IEnemyState
    {

        private Vector2 location;
        private Vector2 target;
        private Vector2 origin;
        private Vector2 direction;
        private BladeTrap trap;

        private bool returningToRest;

        private int moveSpeed;


        public BladeTrapAttackState(Vector2 location, Vector2 target, BladeTrap trap)
        {
            this.location = location;
            origin = location;
            this.target = target;
            this.trap = trap;

            direction = target - location;
            direction.Normalize();

            returningToRest = false;

            moveSpeed = 10;
        }

        public void Attack()
        {
            //already attacking do nothing
        }

        public void ChangeDirection()
        {
            //does nothing
        }

        public void Die()
        {
            trap.state = new BladeTrapRestState(origin,target,trap);
        }

        public void TakeDamage()
        {
            //do nothing
        }

        public void Update()
        {

            
            bool endOfReach = location.X >= target.X && location.Y >= target.Y;
            bool endOfReturn = location.X <= origin.X && location.Y <= origin.Y;

            if(!returningToRest && !endOfReach || returningToRest && !endOfReturn)
            {
                location = Vector2.Add(location, direction * moveSpeed);
                trap.UpdateLocation(location);
            }
            else if(!returningToRest && endOfReach)
            {
                direction = Vector2.Negate(direction);
                returningToRest = true;
                moveSpeed = 2;
            }else if(returningToRest && endOfReturn)
            {
                Die();
            }

            
        }
    }
}
