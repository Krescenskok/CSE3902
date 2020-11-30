using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    class DodongoDamagedState : IEnemyState
    {
        Dodongo dodongo;
        private Vector2 dodongoPos;
        private string direction;
        private int recoverCountDown = 30;

        public DodongoDamagedState(Dodongo dodongo, Vector2 initialPos)
        {
            this.dodongo = dodongo;
            dodongoPos = initialPos;
            direction = dodongo.GetDirection();
            dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDodongoSprite(direction));
        }

        public void Attack()
        {
            //wont attack
        }

        public void ChangeDirection()
        {
            //wont change direction
        }

        public void Die()
        {
            dodongo.Die();
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            //wont move in DamagedState
        }

        public void Stun(bool permanent)
        {
           //not stunned
        }

        public void TakeDamage(int amount)
        {
            //wont take damage in DamagedState
        }

        public void Update()
        {
            if(recoverCountDown < 0)
            {
                if (dodongo.checkAlive())
                {
                    dodongo.dodongoState = new DodongoMovingState(dodongo, dodongoPos);
                    dodongo.SetSprite(EnemySpriteFactory.Instance.CreateDodongoSprite(direction));
                }
                else
                {
                    Die();
                }
            }
            else
            {
                recoverCountDown--;
            }
        }
    }
}
