using Microsoft.Xna.Framework;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public class RopeMoveState : IEnemyState
    {

        private Rope rope;
        private Vector2 location;
        private Vector2 moveDirection;


        private List<Keese.direction> possibleDirections;

        private const float moveSpeed = 1;

        public RopeMoveState(Rope rope, Vector2 location)
        {
            this.rope = rope;
            this.location = location;
            moveDirection = new Vector2(1, 0);

            rope.SetSprite(EnemySpriteFactory.Instance.CreateKeeseMoveSprite());
        }

        public void Attack()
        {
            //do nothing
        }

        public void ChangeDirection()
        {
            possibleDirections = rope.GetDirections();

            Random rand = new Random();
            List<Keese.direction> newDirection = new List<Keese.direction>();

            Keese.direction directionOne = possibleDirections[rand.Next(0, possibleDirections.Count)];
            Keese.direction directionTwo = possibleDirections[rand.Next(0, possibleDirections.Count)];

            //avoid conflicting movement input ex. moving left and right simultaneously
            while ((int)directionOne == (int)directionTwo * -1)
            {
                directionOne = possibleDirections[rand.Next(0, possibleDirections.Count)];
                directionTwo = possibleDirections[rand.Next(0, possibleDirections.Count)];
            }

            newDirection.Add(directionOne);
            newDirection.Add(directionTwo);

            rope.UpdateDirection(newDirection);

            moveDirection.X = CheckDirection(newDirection, Keese.direction.right, Keese.direction.left);
            moveDirection.Y = CheckDirection(newDirection, Keese.direction.up, Keese.direction.down);

        }

        public float CheckDirection(List<Keese.direction> newDir, Keese.direction pos, Keese.direction neg)
        {
            if (newDir.Contains(pos)) return 1;
            if (newDir.Contains(neg)) return -1;
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
            MoveOneUnit();
        }

        public void MoveOneUnit()
        {
            location.X += moveDirection.X * moveSpeed;
            location.Y += moveDirection.Y * moveSpeed;
            rope.UpdateLocation(location);
        }


    }
}
