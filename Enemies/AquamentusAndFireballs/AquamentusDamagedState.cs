using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class AquamentusDamagedState : IEnemyState
    {
        private Aquamentus aquamentus;
        private Vector2 aquamentusPos;
        private int attackPosYValueAdjust = 6;
        private Vector2 targetPos = new Vector2(100, 100);
        private int recoverCountDown = 20;

        public AquamentusDamagedState(Aquamentus aquamentus, Vector2 initalPos)
        {
            this.aquamentus = aquamentus;
            aquamentusPos = initalPos;
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDragonSprite());
        }


        public void Attack()
        {
            Vector2 attackPos = new Vector2(aquamentusPos.X, aquamentusPos.Y + attackPosYValueAdjust);
            aquamentus.SpawnFireBall(attackPos, targetPos);
        }


        public void MoveAwayFromCollision(Collision collision)
        {
            
        }

        public void Die()
        {
            aquamentus.Die();
        }


        public void Update()
        {
            if(recoverCountDown < 0)
            {
                if (aquamentus.checkAlive())
                {
                    aquamentus.state = new AquamentusNormalState(aquamentus, aquamentusPos);
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

        public void TakeDamage(int amount)
        {
            //wont take damage in DamagedState
        }

        public void ChangeDirection()
        {
            //wont use
        }
    }
}
