using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class AquamentusDamagedState : IEnemyState
    {
        private Aquamentus aquamentus;
        private Vector2 aquamentusPos;
        private LinkPlayer link;
        private int attackPosYValueAdjust = 6;
        private int recoverCountDown = 20;

        public AquamentusDamagedState(Aquamentus aquamentus, Vector2 initalPos, LinkPlayer link)
        {
            this.link = link;
            this.aquamentus = aquamentus;
            aquamentusPos = initalPos;
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDragonSprite());
        }


        public void Attack()
        {
            Sounds.Instance.PlayAquamentusRoar();
            Vector2 targetPos = link.CurrentLocation;
            Vector2 attackPos = new Vector2(aquamentus.Location.X, aquamentus.Location.Y + attackPosYValueAdjust);
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
                    aquamentus.state = new AquamentusNormalState(aquamentus, aquamentusPos, link);
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

        public void Stun()
        {
            //not affected
        }
    }
}
