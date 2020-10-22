using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    class StalfosWalkingState : IEnemyState
    {

        private Stalfos skeleton;
        private Vector2 location;
        private Vector2 walkingDirection;

        private List<Stalfos.direction> possibleDirections;

        private const float moveSpeed = 2;

        public StalfosWalkingState(Stalfos stalfos, Vector2 location)
        {
            skeleton = stalfos; 
            this.location = location;

            stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosWalkingSprite());

            walkingDirection.X = 1;
            walkingDirection.Y = 0;
 
        }

        public void Attack()
        {
            //do nothing
        }

        public void ChangeDirection()
        {
            possibleDirections = skeleton.GetDirections();
            
            Random rand = new Random();
            Stalfos.direction nextDirection = possibleDirections[rand.Next(0, possibleDirections.Count)];

            skeleton.UpdateDirection(nextDirection);

            walkingDirection.X = CheckDirection(nextDirection, Stalfos.direction.right, Stalfos.direction.left);
            walkingDirection.Y = CheckDirection(nextDirection, Stalfos.direction.up, Stalfos.direction.down);
            
        }

        public float CheckDirection(Stalfos.direction dir, Stalfos.direction pos, Stalfos.direction negative)
        {
            if (dir == pos) return 1;
            if (dir == negative) return -1;
            return 0;
        }

        public void Die()
        {
            //change to dying state
        }

        public Vector2 GetLocation()
        {
            return location;
        }

        public void TakeDamage()
        {
            //subtract from health
            //call Die() if health < 0
        }

        public void Update()
        {          
            MoveOneUnit();
        }

        public void MoveOneUnit()
        {
            location.X += walkingDirection.X * moveSpeed;
            location.Y +=  walkingDirection.Y * moveSpeed;
            skeleton.UpdateLocation(location);
        }

    }
}
