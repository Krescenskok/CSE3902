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

        private bool stunned;

        public StalfosDamagedState(Direction dir, Stalfos stalfos, Vector2 location, bool stunned)
        {
            currentDirection = dir;

            moveDirection.Y = Directions.CalculateY(currentDirection);
            moveDirection.X = Directions.CalculateX(currentDirection);

            this.stalfos = stalfos;
            this.location = location;

            this.stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosDamagedSprite());
            this.stunned = stunned;
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
            if (collision.From.Equals(currentDirection)) BackToNormal();
        }

        public void TakeDamage(int amount)
        {
            //do nothing
        }

        public void Update()
        {
            MoveOneUnit();
            moveSpeed -= 0.1f;
            
            if (moveSpeed <= 0) {
                BackToNormal();
            }
                


        }

        private void BackToNormal()
        {
            stalfos.state = new StalfosWalkingState(stalfos, location);
            if (stunned) stalfos.state.Stun(true);
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            stalfos.UpdateLocation(location);
        }

        public void Stun(bool permanent)
        {
            if (permanent) stunned = true;
            
            
        }
    }
}
