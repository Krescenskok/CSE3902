using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class StalfosDamagedState : IEnemyState
    {

        private enum Direction { left = -1, right = 1, up = -2, down = 2 };
        
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;

        Vector2 moveDirection;
        Vector2 location;

        private float moveSpeed = 4;
        
        

        private Stalfos stalfos;

        public StalfosDamagedState(string dir, Stalfos stalfos, Vector2 location)
        {
            string direction = dir.ToLower();
            if (direction.Equals(right.ToString())) currentDirection = right;
            if (direction.Equals(left.ToString())) currentDirection = left;
            if (direction.Equals(down.ToString())) currentDirection = down;
            if (direction.Equals(up.ToString())) currentDirection = up;

            moveDirection.Y = CheckDirection(currentDirection, down, up);
            moveDirection.X = CheckDirection(currentDirection, right, left);

            this.stalfos = stalfos;
            this.location = location;

            this.stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosDamagedSprite());
        }

        private int CheckDirection(Direction dir, Direction pos, Direction neg)
        {
            if (dir.Equals(pos)) return 1;
            if (dir.Equals(neg)) return -1;
            return 0;
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
            //do nothing
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
