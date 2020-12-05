using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Sprint5
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
        private float updatePerSec = 60;
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
            string[] directionArray = { "up", "down", "left", "right" };
            
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
            dodongo.UpdateDirection(direction);
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            if(collision.From.ToString().Equals(direction) )
            {
                if(direction.Equals("up"))
                { 
                    direction = "down"; 
                }
                else if(direction.Equals("down")) 
                {
                    direction = "up"; 
                }
                else if(direction.Equals( "left"))
                { 
                    direction = "right"; 
                }
                else if(direction.Equals("right")){ 
                    direction = "left"; 
                }
                dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDodongoSprite(direction));
                dodongo.UpdateDirection(direction);
            }
                
        }

        public void Die()
        {
            //die in DamagedState
        }


        public void Update()
        {
            if (direction.Equals("up"))
            {
                dodongoPos.Y -= speed;
            }
            else if (direction.Equals("down"))
            {
                dodongoPos.Y += speed;
            }
            else if (direction.Equals("left"))
            {
                dodongoPos.X -= speed;
            }
            else if (direction.Equals("right"))
            {
                dodongoPos.X += speed;
            }
            dodongo.UpdatePos(dodongoPos);
        }

        public void TakeDamage(int amount)
        {
            if (amount == -1)
            {
                dodongo.dodongoState = new DodongoDamagedState(dodongo, dodongoPos);
                dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDodongoSprite(direction));
                dodongo.LostHP();
            }
        }

        public void Stun(bool permanent)
        {
            //wont get stuned
        }
    }
}
