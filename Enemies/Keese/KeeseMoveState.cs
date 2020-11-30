using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class KeeseMoveState : IEnemyState
    {

        private Keese bat;
        private Vector2 location;

        private Vector2 moveDirection;

        public enum Direction { left, right, up, down };
        
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        List<Direction> possibleDirections;
        List<Direction> currentDirection;

        private const int moveSpeed = 1;
        private int currentMoveSpeed = moveSpeed;

        private const int stunTime = 120;
        private int stunClock = 0;
        private Random RandomNumber = new Random();

        public KeeseMoveState(Keese keese, Vector2 location)
        {
            bat = keese;
            this.location = location;
            moveDirection = new Vector2(1, 0);

            keese.SetSprite(EnemySpriteFactory.Instance.CreateKeeseMoveSprite());

            possibleDirections = new List<Direction> { left, right, up, down };
            ChangeDirection();
        }

        public void Attack()
        {
            //do nothing
        }

        public void ChangeDirection()
        {
            
            currentDirection = new List<Direction>();
            
            Direction directionOne = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];
            Direction directionTwo = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];

            //avoid conflicting movement input ex. moving left and right simultaneously
            while(OppositeDirections(directionOne,directionTwo))
            {
                directionOne = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];
                directionTwo = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];
            }

            currentDirection.Add(directionOne);
            currentDirection.Add(directionTwo);

           

            moveDirection.X = CheckDirection(currentDirection, right, left);
            moveDirection.Y = CheckDirection(currentDirection,down, up);

            possibleDirections = new List<Direction> { left, right, up, down };
        }


        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = new List<Direction> { left, right, up, down };

            Direction dir = (Direction)collision.From;
            possibleDirections.Remove(dir);

           
            if (!possibleDirections.Contains(currentDirection[0]) || !possibleDirections.Contains(currentDirection[1])) ChangeDirection();
            
        }


        private int CheckDirection(List<Direction> newDir, Direction pos, Direction neg)
        {
            if (newDir.Contains(pos)) return 1;
            if (newDir.Contains(neg)) return -1;
            return 0;
        }

        private bool OppositeDirections(Direction dir1,Direction dir2)
        {
            return dir1.Equals(left) && dir2.Equals(right)
                    || dir1.Equals(right) && dir2.Equals(left)
                    || dir1.Equals(up) && dir2.Equals(down)
                    || dir1.Equals(down) && dir2.Equals(up);
        }


        public void Die()
        {
            bat.Die();
            
        }

  
        public void Update()
        {
            if (RandomNumber.Next(0, 100) == 0) ChangeDirection();
            MoveOneUnit();

            if (stunClock > 0) stunClock--;
            else if (stunClock <= 0 && currentMoveSpeed == 0) currentMoveSpeed = moveSpeed;
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * currentMoveSpeed;
            location.Y += moveDirection.Y * currentMoveSpeed;
            bat.UpdateLocation(location);
        }

        

        public void TakeDamage(int amount)
        {
            
        }

        public void Stun(bool permanent)
        {
            currentMoveSpeed = 0;
            stunClock = permanent ? int.MaxValue : stunTime;

        }
    }
}
