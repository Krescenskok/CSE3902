using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    class StalfosWalkingState : IEnemyState
    {

        private Stalfos skeleton;
        private Vector2 location;
        private Vector2 moveDirection;


        List<Direction> possibleDirections;
        Direction currentDirection;


        private float randomness = 0.1f;

        private const int normalMoveSpeed = 1;
        private int currentMoveSpeed = normalMoveSpeed;
        private const int stunTime = 120;
        private int stunClock = 0;

        public bool permaStun = false;

        public StalfosWalkingState(Stalfos stalfos, Vector2 location)
        {
            skeleton = stalfos; 
            this.location = location;

            stalfos.SetSprite(EnemySpriteFactory.Instance.CreateStalfosWalkingSprite());

            currentDirection = Direction.right;
            possibleDirections = Directions.Default();
            moveDirection.X = 1;
            moveDirection.Y = 0;
        }

        

        public void ChangeDirection()
        {

            currentDirection = Directions.RandomDirection(possibleDirections);

            

            moveDirection.Y = Directions.CheckDirection(currentDirection, Direction.down, Direction.up);
            moveDirection.X = Directions.CheckDirection(currentDirection, Direction.right, Direction.left);

            possibleDirections = Directions.Default();
            
        }



      

        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = Directions.Default();
            possibleDirections.Remove(collision.From);

            if (!possibleDirections.Contains(currentDirection)) ChangeDirection();
        }

        

        public void Update()
        {
            

            if (Directions.Chance(randomness)) ChangeDirection();
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

        public void Stun(bool permanent)
        {
            currentMoveSpeed = 0;
            stunClock = permanent ? int.MaxValue : stunTime;
            permaStun = permanent ? true : permaStun;
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

        public void TakeDamage(int amount)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
