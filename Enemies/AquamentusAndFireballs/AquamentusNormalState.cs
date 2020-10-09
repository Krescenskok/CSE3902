using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Sprint2
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class AquamentusNormalState : IEnemyState
    {

        private Aquamentus AquamentusNormal;
        private Vector2 aquamentusPos;
        private int attackPosYValueAdjust = 6;
        private Vector2 targetPos = new Vector2(100, 100);

        public AquamentusNormalState(Aquamentus aquamentus, Vector2 initialPos)
        {
            AquamentusNormal = aquamentus;
            aquamentusPos = initialPos;
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDragonSprite());
        }

        public void Attack()
        {
            Vector2 attackPos = new Vector2(aquamentusPos.X, aquamentusPos.Y + attackPosYValueAdjust);
            AquamentusNormal.SpawnFireBall(attackPos, targetPos);
        }

        public void ChangeDirection()
        {
            //Do nothing
        }

        public void Die()
        {
            //enemy die in damaged state
        }



        public void TakeDamage()
        {
            AquamentusNormal.LostHP();
            AquamentusNormal.state = new AquamentusDamagedState(AquamentusNormal, aquamentusPos);
        }

        public void MoveLeft()
        {
            aquamentusPos.X-=2;
        }

        public void Update()
        {
            MoveLeft();
            AquamentusNormal.ChangePos(aquamentusPos);
            if (AquamentusNormal.TryAttack())
            {
                Attack();
            }
        }
    }
}
