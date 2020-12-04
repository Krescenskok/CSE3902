using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
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
        private int recoverCountDown = 120;
        private int directionIndex;
        private float speed;

        public AquamentusDamagedState(Aquamentus aquamentus, Vector2 initalPos, LinkPlayer link, int directionIndex, float speed)
        {
            this.link = link;
            this.aquamentus = aquamentus;
            aquamentusPos = initalPos;
            this.directionIndex = directionIndex;
            this.speed = speed;
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDragonSprite());
        }


        public void Attack()
        {
            Vector2 targetPos = link.CurrentLocation;
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
            if (!aquamentus.checkAlive())
            {
                Die();
            }

            if(recoverCountDown < 0)
            {
               aquamentus.state = new AquamentusNormalState(aquamentus, aquamentusPos, link, directionIndex, speed);
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
            directionIndex *= -1;
        }

        public void Stun()
        {
            //not affected
        }

        public void Stun(bool permanent)
        {
            //not affected
        }
    }
}
