﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Security;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class GoriyaMoveState : IEnemyState
    {
        private Goriya goriya;
        
        private Vector2 location;
        private Vector2 moveDirection;
        
        List<Direction> possibleDirections;
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;


        private const int moveSpeed = 1;
        private int currentMoveSpeed = moveSpeed;
        private bool currentlyThrowing;
        

        private Random RandomNumber;
        private const float directionChangeLikelihood = 0.1f;
        private const float attackLikelihood = 0.05f;


        private const int stunTime = 120;
        private int stunClock = 0;
        public bool permaStun = false;
        public GoriyaMoveState(Goriya goriya, Vector2 location)
        {
            this.goriya = goriya;
            this.location = location;



            RandomNumber = new Random();
            possibleDirections = new List<Direction> { left, right, up, down };
            ChangeDirection();

        }

        public void Attack()
        {
            if (!currentlyThrowing)
            {
                
                currentlyThrowing = true;
                goriya.SetBoomerang(new GoriyaBoomerang(location, currentDirection.ToString(),moveSpeed));
            }
            
        }

        public void ChangeDirection()
        {


            currentDirection = RandomDirection(possibleDirections);

            moveDirection.Y = CheckDirection(currentDirection, down, up);
            moveDirection.X = CheckDirection(currentDirection, right, left);

            possibleDirections = new List<Direction> { left, right, up, down };

            string dir = currentDirection.ToString();

            goriya.SetSprite(EnemySpriteFactory.Instance.CreateGoriyaWalkingSprite(dir));


        }

        private Direction RandomDirection(List<Direction> directions)
        {
            int rand = RandomNumber.Next(0, directions.Count);
            return directions[rand];
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = new List<Direction> { left, right, up, down };

            possibleDirections.Remove((Direction)collision.From);
            
          
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

        public void Update()
        {


            

            if ((goriya.GetBoomerang() == null || goriya.GetBoomerang().Finished()) && NotStunned())
            {
               
               
                currentlyThrowing = false;
                goriya.SetBoomerang(null);

                
                
                if (Directions.Chance(directionChangeLikelihood)) ChangeDirection();
                MoveOneUnit();
                if (Directions.Chance(attackLikelihood)) Attack();
            }
            

        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * currentMoveSpeed;
            location.Y += moveDirection.Y * currentMoveSpeed;
           
            goriya.UpdateLocation(location);
           
        }


        public void TakeDamage(int amount)
        {
            
            goriya.state = new GoriyaDamagedState(currentDirection, goriya, location, moveSpeed, permaStun);
            
        }



        public void Stun(bool permanent)
        {
            currentMoveSpeed = 0;
            stunClock = permanent || permaStun ? int.MaxValue : stunTime;
            permaStun = permanent ? true : permaStun;
        }

        public bool NotStunned()
        {
            if (stunClock > 0) stunClock--;
            else if (stunClock <= 0 && currentMoveSpeed == 0) currentMoveSpeed = moveSpeed;

            return stunClock <= 0;
        }
    }
}
