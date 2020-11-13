using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class GoriyaDamagedState : IEnemyState
    {

        

        
        Direction currentDirection;

        Vector2 moveDirection;
        Vector2 location;

        private int moveSpeed;
        private const int normalSpeedMultiplier = 4;
        private const int totalDamagedTime = 30;
        private int damagedTime = 0;


        private Goriya goriya;

        public GoriyaDamagedState(Direction dir, Goriya goriya, Vector2 location, int moveSpeed)
        {
            currentDirection = dir;

            moveDirection.Y = Directions.CalculateY(currentDirection);
            moveDirection.X = Directions.CalculateX(currentDirection);

            this.goriya = goriya;
            this.location = location;
            this.moveSpeed = moveSpeed * normalSpeedMultiplier;

            
            goriya.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaDamagedSprite(dir.ToString()));
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
            Direction collisionDirection = collision.From;
            if (collisionDirection.Equals(currentDirection))
            {
                moveSpeed = 0;
            }
        }

        public void TakeDamage(int amount)
        {
            //do nothing
        }

        public void Update()
        {
            MoveOneUnit();
            damagedTime++;

            if (damagedTime >= totalDamagedTime) goriya.state = new GoriyaMoveState(goriya, location);
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            goriya.UpdateLocation(location);
        }

        public void Stun()
        {
            //do nothing
        }
    }
}
