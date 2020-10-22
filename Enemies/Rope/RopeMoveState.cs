using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class RopeMoveState : IEnemyState
    {
        private Rope rope;

        private Vector2 location;
        private Vector2 moveDirection;
        

        private const int originalMoveSpeed = 1;
        private int moveSpeed = originalMoveSpeed;

        Random RandomNumber;

        private enum Direction { left = -1, right = 1, up = -2, down = 2 };
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
            moveSpeed = originalMoveSpeed * 2;
        }

        public void DontAttack()
        {
            moveSpeed = originalMoveSpeed;
        }

        //choose new tile on current row or column and change movedirection
        public void ChangeDirection()
        {
            

            currentDirection = RandomDirection(possibleDirections);

            moveDirection.Y = CheckDirection(currentDirection, down, up);
            moveDirection.X = CheckDirection(currentDirection,right,left);



            possibleDirections = new List<Direction> { left, right, up, down };

            if (currentDirection.Equals(left)) rope.SetSprite(EnemySpriteFactory.Instance.CreateRopeMoveSprite("left"));
            if (currentDirection.Equals(right)) rope.SetSprite(EnemySpriteFactory.Instance.CreateRopeMoveSprite("right"));
            
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



        private int CheckDirection(Direction dir, Direction pos, Direction neg)
        {
            if (dir.Equals(pos)) return 1;
            if (dir.Equals(neg)) return -1;
            return 0;
        }

        public void Update()
        {

            if (RandomNumber.Next(0,100) == 0) ChangeDirection();
         
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
            rope.SubtractHP(amount);
        }
    }
}
