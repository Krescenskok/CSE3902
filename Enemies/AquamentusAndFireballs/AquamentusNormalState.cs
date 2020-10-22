using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class AquamentusNormalState : IEnemyState
    {
        private Aquamentus aquamentus;
        private Vector2 aquamentusPos;
        private int attackPosYValueAdjust = 6;
        private Vector2 targetPos = new Vector2(100, 100);


        public AquamentusNormalState(Aquamentus aquamentus, Vector2 initialPos)
        {
            this.aquamentus = aquamentus;
            aquamentusPos = initialPos;
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDragonSprite());
        }

        public void Attack()
        {
            Vector2 attackPos = new Vector2(aquamentusPos.X, aquamentusPos.Y + attackPosYValueAdjust);
            aquamentus.SpawnFireBall(attackPos, targetPos);
        }

        public void ChangeDirection()
        {
            //wont use
        }

        public void Die()
        {
            //enemy die in damaged state
        }

        public void TakeDamage(int amount)
        {
            aquamentus.LostHP(amount);
            aquamentus.state = new AquamentusDamagedState(aquamentus, aquamentusPos);
        }

        public void Update()
        {
            //do nothing
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            //aquamentus move in its own area
        }

    }
}
