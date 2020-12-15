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

        
        private Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        private List<Direction> possibleDirections;
        private List<Direction> currentDirection;

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

            possibleDirections = Directions.Default();
            ChangeDirection();
        }

     

        public void ChangeDirection()
        {
            
            currentDirection = new List<Direction>();
            
            Direction directionOne = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];
            Direction directionTwo = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];

            //avoid conflicting movement input ex. moving left and right simultaneously
            while(Directions.Opposites(directionOne,directionTwo))
            {
                directionOne = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];
                directionTwo = possibleDirections[RandomNumber.Next(0, possibleDirections.Count)];
            }

            currentDirection.Add(directionOne);
            currentDirection.Add(directionTwo);

            moveDirection.X = Directions.CheckDirection(currentDirection, right, left);
            moveDirection.Y = Directions.CheckDirection(currentDirection,down, up);

            possibleDirections = Directions.Default();
        }


        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = Directions.Default();
            possibleDirections.Remove(collision.From);

            if (!possibleDirections.Contains(currentDirection[0]) || !possibleDirections.Contains(currentDirection[1])) ChangeDirection();
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
            bat.Location = location;
        }

        

        public void TakeDamage(int amount)
        {
            
        }

        public void Stun(bool permanent)
        {
            currentMoveSpeed = 0;
            stunClock = permanent ? int.MaxValue : stunTime;

        }

        public void Attack()
        {
            //do nothing
        }
    }
}
