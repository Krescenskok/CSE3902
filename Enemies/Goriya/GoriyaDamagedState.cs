using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class GoriyaDamagedState : IEnemyState
    {

        private enum Direction { left , right , up , down  };

        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;

        Vector2 moveDirection;
        Vector2 location;

        private int moveSpeed;
        private const int normalSpeedMultiplier = 4;
        private const int totalDamagedTime = 30;
        private int damagedTime = 0;


        private Goriya goriya;

        public GoriyaDamagedState(string dir, Goriya goriya, Vector2 location, int moveSpeed)
        {
            if (dir.Equals(left.ToString())) currentDirection = right;
            if (dir.Equals(right.ToString())) currentDirection = left;
            if (dir.Equals(up.ToString())) currentDirection = down;
            if (dir.Equals(down.ToString())) currentDirection = up;

            moveDirection.Y = CheckDirection(currentDirection, down, up);
            moveDirection.X = CheckDirection(currentDirection, right, left);

            this.goriya = goriya;
            this.location = location;
            this.moveSpeed = moveSpeed * normalSpeedMultiplier;

            goriya.Collider().ChangeState(this);
            goriya.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaDamagedSprite(dir));
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
            Direction collisionDirection = (Direction)collision.From();
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
