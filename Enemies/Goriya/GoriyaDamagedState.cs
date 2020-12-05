using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class GoriyaDamagedState : IEnemyState
    {

        private Direction currentDirection;

        private Vector2 moveDirection;
        private Vector2 location;

        private int moveSpeed;
        private const int normalSpeedMultiplier = 4;
        private const int totalDamagedTime = 30;
        private int damagedTime = 0;


        private Goriya goriya;
        private bool stunned;
        public GoriyaDamagedState(Direction dir, Goriya goriya, Vector2 location, int moveSpeed, bool stunned)
        {
            currentDirection = dir;

            moveDirection.Y = Directions.CalculateY(currentDirection);
            moveDirection.X = Directions.CalculateX(currentDirection);

            this.goriya = goriya;
            this.location = location;
            this.moveSpeed = moveSpeed * normalSpeedMultiplier;

            
            goriya.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaDamagedSprite(dir.ToString()));
            this.stunned = stunned;
        }


        public void MoveAwayFromCollision(Collision collision)
        {
            moveSpeed = collision.From.Equals(currentDirection) ? 0 : moveSpeed;
        }


        public void Update()
        {
            MoveOneUnit();
            damagedTime++;

            if (damagedTime >= totalDamagedTime)
            {
                goriya.state = new GoriyaMoveState(goriya, location);
                if (stunned) goriya.state.Stun(true);
            }
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            goriya.Location = location;
        }

        public void Stun(bool permanent)
        {
            if (permanent) stunned = true;
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

        public void TakeDamage(int amount)
        {
            //do nothing
        }
    }
}
