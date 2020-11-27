using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class StalfosDamagedState : IEnemyState
    {

        
        
        
        Direction currentDirection;

        Vector2 moveDirection;
        Vector2 location;

        private float moveSpeed = 4;
        
        

        private Stalfos stalfos;

        public StalfosDamagedState(Direction dir, Stalfos stalfos, Vector2 location)
        {
            currentDirection = dir;

            moveDirection.Y = Directions.CalculateY(currentDirection);
            moveDirection.X = Directions.CalculateX(currentDirection);

            this.stalfos = stalfos;
            this.location = location;

            this.stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosDamagedSprite());
        }

      
        public void Attack()
        {
            //do nothing
        }

        public void ChangeDirection()
        {
            //do nothing
            
        }

        public void Die()
        {
            //do nothing
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            if(collision.From.Equals(currentDirection)) stalfos.state = new StalfosWalkingState(stalfos, location);
        }

        public void TakeDamage(int amount)
        {
            //do nothing
        }

        public void Update()
        {
            MoveOneUnit();
            moveSpeed -= 0.1f;
            
            if (moveSpeed <= 0) stalfos.state = new StalfosWalkingState(stalfos, location);


        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            stalfos.UpdateLocation(location);
        }

        public void Stun()
        {
           //can't be stunned while taking damage
        }
    }
}
