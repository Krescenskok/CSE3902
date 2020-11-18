using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>State of Blade Trap when it's attacking. Moves to a target Vector2 and returns.</para>
    /// </summary>
    public class BladeTrapAttackState : IEnemyState
    {

        private Vector2 location;
      
        private Vector2 origin;
        private Vector2 direction;
        private int attackLength;
        private BladeTrap trap;

        private bool returningToRest;
        private bool endOfReach;
        private bool endOfReturn;

        private int moveSpeed;


        public BladeTrapAttackState(Vector2 location, Vector2 direction, BladeTrap trap, int attackLength)
        {
            this.location = location;
            origin = location;
            this.direction = direction;
            this.trap = trap;

            this.attackLength = attackLength;

          

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

        public void MoveAwayFromCollision(Collision collision)
        {
           if(collision.other.Name.Equals("Trap")) endOfReach = true;

        }

        public void Die()
        {
            trap.state = new BladeTrapRestState(origin,trap);
        }

        public void TakeDamage()
        {
            //do nothing
        }

        public void Update()
        {


            if (!endOfReach) endOfReach = Vector2.Distance(location, origin) >= attackLength;

           
            //endOfReturn = location.X <= origin.X && location.Y <= origin.Y;
            endOfReturn = Vector2.Distance(location, origin) < 1;

            if(!returningToRest && !endOfReach || returningToRest && !endOfReturn)
            {
                location = Vector2.Add(location, direction * moveSpeed);
                trap.UpdateLocation(location);
            }
            else if(!returningToRest && endOfReach)
            {
                direction = Vector2.Negate(direction);
                returningToRest = true;
                moveSpeed = 1;

                
            }else if(returningToRest && endOfReturn)
            {
                Die();
            }

            
        }

        public void TakeDamage(int amount)
        {
            //does not take damage
        }

        public void Stun()
        {
           //can't be stunned
        }
    }
}
