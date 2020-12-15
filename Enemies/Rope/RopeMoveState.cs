using Microsoft.Xna.Framework;
using Sprint5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class RopeMoveState : IEnemyState
    {
        private Rope rope;

        private Vector2 location;
        private Vector2 moveDirection;
        

        private const int normalMoveSpeed = 1;
        private int moveSpeed = normalMoveSpeed;


        private const int attackTime = 60, stunTime = 120;
        private int attackClock = 0, stunClock = 0;
        private bool stunned = false, attacking = false;

        Random RandomNumber;

        List<Direction> possibleDirections;
        Direction left = Direction.left, right = Direction.right, up = Direction.up, down = Direction.down;
        Direction currentDirection;

        

        public RopeMoveState(Rope rope, Vector2 location, Game game)
        {
            this.location = location;
            this.rope = rope;

            this.rope.SetSprite(EnemySpriteFactory.Instance.CreateRopeMoveSprite("left"));


            RandomNumber = new Random();

            possibleDirections = new List<Direction> { left, right, up, down };

            currentDirection = down;
            moveDirection.Y = 1;
            moveDirection.X = 0;
        }

        public void Attack()
        {
            if (!stunned)
            {
                moveSpeed = normalMoveSpeed * 2;
                attackClock = attackTime;
            }
            
        }

        public void DontAttack()
        {
            moveSpeed = normalMoveSpeed;
        }

        //choose new tile on current row or column and change movedirection
        public void ChangeDirection()
        {
            

            currentDirection = Directions.RandomDirection(possibleDirections);

            moveDirection.Y = Directions.CheckDirection(currentDirection, down, up);
            moveDirection.X = Directions.CheckDirection(currentDirection,right,left);



            possibleDirections = new List<Direction> { left, right, up, down };

            if (currentDirection.Equals(left)) rope.SetSprite(EnemySpriteFactory.Instance.CreateRopeMoveSprite("left"));
            if (currentDirection.Equals(right)) rope.SetSprite(EnemySpriteFactory.Instance.CreateRopeMoveSprite("right"));

            rope.UpdateDirection(currentDirection);
        }

        private Direction RandomDirection(List<Direction> directions)
        {
            int rand = RandomNumber.Next(0, directions.Count);
            return directions[rand];
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            possibleDirections = Directions.Default();

            possibleDirections.Remove(collision.From);

            if (!possibleDirections.Contains(currentDirection)) ChangeDirection();

            attackClock = 0;
        }


        public void Update()
        {
           stunned = stunClock > 0;
            attacking = attackClock > 0;

            if (attacking) attackClock--;
            if (stunned) stunClock--;
            else if (!attacking && !stunned && moveSpeed != normalMoveSpeed) moveSpeed = normalMoveSpeed;


            if (RandomNumber.Next(0,100) == 0 && attackClock <= 0) ChangeDirection();
         
            MoveOneUnit(); 
        }


        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            
            rope.UpdateLocation(location);
        }


        public void Die()
        {
            //change to dying sprite
        }

        public void TakeDamage(int amount)
        {
            //
        }

        public void Stun(bool permanent)
        {
            moveSpeed = 0;
            stunClock = permanent ? int.MaxValue : stunTime;

        }

        
    }
}
