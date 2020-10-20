using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class GoriyaMoveState : IEnemyState
    {
        private Goriya gorilla;
        
        private Vector2 location;
        private Vector2 boomerangLocation;

        private Vector2 moveDirection;
        

        private enum Direction { left, right, up, down };
        List<Direction> possibleDirections;
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;


        private const int moveSpeed = 1;

        private const int boomerangThrowTime = 50;
        private int timeSinceThrown;
        private bool currentlyThrowing;
        private bool returning;

        private Random RandomNumber;

        public GoriyaMoveState(Goriya goriya, Vector2 location)
        {
            gorilla = goriya;

            this.location = location;

            boomerangLocation = location;


            goriya.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaWalkingSprite("right"));
            currentDirection = right;
            moveDirection.X = 1;
            moveDirection.Y = 0;

            timeSinceThrown = 0;

            RandomNumber = new Random();
            possibleDirections = new List<Direction> { left, right, up, down };
        }

        public void Attack()
        {
            if (!currentlyThrowing)
            {
                //gorilla.SetBoomerang(true);
                currentlyThrowing = true;
                returning = false;
                timeSinceThrown = 0;
                gorilla.SetBoomerang(new GoriyaBoomerang(location, currentDirection.ToString(),moveSpeed));
            }
            
        }

        public void ChangeDirection()
        {


            currentDirection = RandomDirection(possibleDirections);

            moveDirection.Y = CheckDirection(currentDirection, down, up);
            moveDirection.X = CheckDirection(currentDirection, right, left);

            possibleDirections = new List<Direction> { left, right, up, down };

            string dir = currentDirection.ToString();

            gorilla.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaWalkingSprite(dir));


        }

        private Direction RandomDirection(List<Direction> directions)
        {
            int rand = RandomNumber.Next(0, directions.Count);
            return directions[rand];
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = new List<Direction> { left, right, up, down };

            possibleDirections.Remove((Direction)collision.From());
            Direction dir = (Direction)collision.From();
          
            if (!possibleDirections.Contains(currentDirection)) ChangeDirection();
        }


        private int CheckDirection(Direction dir, Direction pos, Direction neg)
        {
            if (dir.Equals(pos)) return 1;
            if (dir.Equals(neg)) return -1;
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

       

            
            if(gorilla.GetBoomerang() == null || gorilla.GetBoomerang().Finished())
            {

                currentlyThrowing = false;
                gorilla.SetBoomerang(null);

                int rand = RandomNumber.Next(0, 1000);
                if (rand < 10) ChangeDirection();
                
                MoveOneUnit();

                if (rand > 10 && rand < 30) Attack();

                
            }
            

        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            boomerangLocation = location;
            gorilla.UpdateLocation(location);
            gorilla.UpdateBoomerangLocation(location);
        }


        public void TakeDamage(int amount)
        {
            //wait
        }
    }
}
