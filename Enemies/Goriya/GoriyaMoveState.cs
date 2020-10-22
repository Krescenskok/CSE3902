using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
        private int[] directionModifier;

        private List<Goriya.direction> possibleDirections;

        private const float moveSpeed = 1;

        private const int boomerangThrowTime = 50;
        private int timeSinceThrown;
        private bool currentlyThrowing;
        private bool returning;

        public GoriyaMoveState(Goriya goriya, Vector2 location)
        {
            gorilla = goriya;

            this.location = location;

            boomerangLocation = location;


            goriya.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaWalkingSprite("right"));

            moveDirection.X = 1;
            moveDirection.Y = 0;

            directionModifier = new int[2];
            directionModifier[0] = -1;
            directionModifier[1] = 1;

            timeSinceThrown = 0;
        }

        public void Attack()
        {
            if (!currentlyThrowing)
            {
                gorilla.SetBoomerang(true);
                currentlyThrowing = true;
                returning = false;
                timeSinceThrown = 0;
            }
            
        }

        public void ChangeDirection()
        {
            possibleDirections = gorilla.GetDirections();

            Random rand = new Random();
            Goriya.direction newDirection = possibleDirections[rand.Next(0, possibleDirections.Count)];

            moveDirection.X = CheckDirection(newDirection, Goriya.direction.right, Goriya.direction.left);
            moveDirection.Y = CheckDirection(newDirection, Goriya.direction.down, Goriya.direction.up);

            string direction = moveDirection.X > 0 ? "right" : "left";
            direction = moveDirection.Y > 0 ? "down" : direction;
            direction = moveDirection.Y < 0 ? "up" : direction;

            gorilla.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaWalkingSprite(direction));

            gorilla.UpdateDirection(newDirection);

        }

        public float CheckDirection(Goriya.direction dir, Goriya.direction pos, Goriya.direction neg)
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

            bool boomerangIsLeaving = currentlyThrowing && timeSinceThrown < boomerangThrowTime && !returning;
            bool boomerangComingBack = currentlyThrowing && timeSinceThrown > 0 && returning;
            bool boomerangAtEndOfThrow = currentlyThrowing && timeSinceThrown == boomerangThrowTime && !returning;
            bool boomerangDone = currentlyThrowing && timeSinceThrown == 0 && returning;

            if (boomerangIsLeaving)
            {
                boomerangLocation.X += moveDirection.X * moveSpeed * 5;
                boomerangLocation.Y += moveDirection.Y * moveSpeed * 5;               
                gorilla.UpdateBoomerangLocation(boomerangLocation);
                timeSinceThrown++;
            }
            else if (boomerangAtEndOfThrow)
            {
                returning = true;
            }
            else if(boomerangComingBack)
            {
                boomerangLocation.X -= moveDirection.X * moveSpeed * 5;
                boomerangLocation.Y -= moveDirection.Y * moveSpeed* 5;
                gorilla.UpdateBoomerangLocation(boomerangLocation);
                timeSinceThrown--;
            }
            else if(boomerangDone)
            {
                currentlyThrowing = false;
                gorilla.SetBoomerang(false);
                returning = false;
                
            }
            else
            {
                MoveOneUnit();
                
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

    }
}
