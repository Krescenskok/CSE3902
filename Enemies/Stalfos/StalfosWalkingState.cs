using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Vector2 moveDirection;

        

        private enum Direction { left = -1, right = 1, up = -2, down = 2 };
        List<Direction> possibleDirections;
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;
        

        Random RandomNumber;

        private const int normalMoveSpeed = 1;
        private int currentMoveSpeed = normalMoveSpeed;
        private const int stunTime = 120;
        private int stunClock = 0;

        public StalfosWalkingState(Stalfos stalfos, Vector2 location)
        {
            skeleton = stalfos; 
            this.location = location;

            stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosWalkingSprite());

            currentDirection = right;
            moveDirection.X = 1;
            moveDirection.Y = 0;

            RandomNumber = new Random();
            possibleDirections = new List<Direction> { left, right, up, down };
        }

        

        public void ChangeDirection()
        {


            currentDirection = RandomDirection(possibleDirections);

            moveDirection.Y = CheckDirection(currentDirection, down, up);
            moveDirection.X = CheckDirection(currentDirection, right, left);



            possibleDirections = new List<Direction> { left, right, up, down };


        }

        private int CheckDirection(Direction dir, Direction pos, Direction neg)
        {
            if (dir.Equals(pos)) return 1;
            if (dir.Equals(neg)) return -1;
            return 0;
        }

 

        private Direction RandomDirection(List<Direction> directions)
        {
            int rand = RandomNumber.Next(0, directions.Count);
            return directions[rand];
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = new List<Direction> { left, right, up, down };

            if (collision.Left()) possibleDirections.Remove(Direction.left);
            if (collision.Right()) possibleDirections.Remove(Direction.right);
            if (collision.Up()) possibleDirections.Remove(Direction.up);
            if (collision.Down()) possibleDirections.Remove(Direction.down);

            

            if (!possibleDirections.Contains(currentDirection)) ChangeDirection();
        }

        

        public void Update()
        {
            if (RandomNumber.Next(0, 100) == 0) ChangeDirection();
            MoveOneUnit();

            if (stunClock > 0) stunClock--;
            else if (stunClock <= 0 && currentMoveSpeed == 0) currentMoveSpeed = normalMoveSpeed;
           
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * currentMoveSpeed;
            location.Y +=  moveDirection.Y * currentMoveSpeed;
            skeleton.UpdateLocation(location);
        }

        public void Stun()
        {
            currentMoveSpeed = 0;
            stunClock = stunTime;
           
        }


        public void TakeDamage(int amount)
        {
            bool stillAlive = skeleton.SubtractHP(amount);
            if (stillAlive) skeleton.state = new StalfosDamagedState(currentDirection.ToString(), skeleton, location);
        }

        public void TakeDamage(string dir, int amount)
        {
            bool stillAlive = skeleton.SubtractHP(amount);
            if (stillAlive) skeleton.state = new StalfosDamagedState(dir, skeleton, location);
            
        }

        #region //unused methods
        public void Attack()
        {
            //do nothing
        }

        public void Die()
        {
            //change to dying state
        }
        #endregion

    }
}
