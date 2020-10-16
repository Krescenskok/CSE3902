using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class DodongoMovingState : IEnemyState
    {
        Dodongo dodongoMoving;
        private Vector2 dodongoPos;
        private string direction;
        private float speedPerSec = 25;
        private float updatePerSec = 30;
        private float speed;
        public DodongoMovingState(Dodongo dodongo, Vector2 initialPos)
        {
            dodongoMoving = dodongo;
            dodongoPos = initialPos;
            direction = "Right";
            speed = speedPerSec / updatePerSec;
        }

        public void Attack()
        {
        }
        
        public void ChangeDirection()
        {
            Random rand = new Random();
            Boolean findNewDirection = false;
            string[] directionArray = { "ForWard", "Backward", "Left", "Right" };
            int directionIndex = rand.Next(0, 4);
            while (!findNewDirection)
            {
                if (directionArray[directionIndex].Equals(direction))
                {
                    directionIndex = rand.Next(0, 4);
                }
                else
                {
                    direction = directionArray[directionIndex];
                    findNewDirection = true;
                    dodongoMoving.SetSprite(direction);
                }
            }
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            //do nothing
        }

        public void TakeDamage()
        {
            //do nothing for now
        }

        public void Update()
        {
            if (direction.Equals("ForWard"))
            {
                dodongoPos.Y -= speed;
            }
            else if (direction.Equals("BackWard"))
            {
                dodongoPos.Y += speed;
            }
            else if (direction.Equals("Left"))
            {
                dodongoPos.X -= speed;
            }
            else if (direction.Equals("Right"))
            {
                dodongoPos.X += speed;
            }

            dodongoMoving.UpdatePos(dodongoPos);
        }
    }
}
