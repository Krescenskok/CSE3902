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
        Dodongo dodongo;
        private Vector2 dodongoPos;
        private string direction = "Right";
        private float speedPerSec = 25;
        private float updatePerSec = 30;
        private float speed;
        public DodongoMovingState(Dodongo dodongo, Vector2 initialPos)
        {
            this.dodongo = dodongo;
            dodongoPos = initialPos;
            direction = dodongo.GetDirection();
            speed = speedPerSec / updatePerSec;
            dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDodongoSprite(direction)) ;
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
                    dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDodongoSprite(direction));
                }
            }
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            ChangeDirection();
        }

        public void Die()
        {
            //die in DamagedState
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
            dodongo.UpdatePos(dodongoPos);
        }

        public void TakeDamage(int amount)
        {
            if (amount == -1)
            {
                dodongo.LostHP();
                dodongo.dodongoState = new DodongoDamagedState(dodongo, dodongoPos);
                dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDodongoSprite(direction));
            }
        }
    }
}
