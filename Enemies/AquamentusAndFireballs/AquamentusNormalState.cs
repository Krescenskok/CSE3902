using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class AquamentusNormalState : IEnemyState
    {
        private Aquamentus aquamentus;
        private Vector2 aquamentusPos;
        private int attackPosYValueAdjust = 6;
        private LinkPlayer link;
        private int directionIndex;
        private float speed;
        private const double AttackAngle = 35;
        private double AttackRadian;

        public AquamentusNormalState(Aquamentus aquamentus, Vector2 initialPos, LinkPlayer link, int directionIndex, float speed)
        {
            this.aquamentus = aquamentus;
            this.link = link;
            this.directionIndex = directionIndex;
            this.speed = speed;
            aquamentusPos = initialPos;
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDragonSprite());
            AttackRadian = 2 * Math.PI * AttackAngle / 360;
        }

        public void Attack()
        {
            Vector2 targetPos = link.CurrentLocation;
            Vector2 attackPos = new Vector2(aquamentusPos.X, aquamentusPos.Y + attackPosYValueAdjust);
            aquamentus.SpawnFireBall(attackPos, targetPos);

            float deltaX = -(targetPos.X - attackPos.X);
            float deltaY = targetPos.Y - attackPos.Y;
            double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            double angle = Math.Atan(deltaY / deltaX);
            double upFireAngle = angle - AttackRadian;
            targetPos = new Vector2((float)(attackPos.X - Math.Cos(upFireAngle) * distance), (float)(attackPos.Y + Math.Sin(upFireAngle) * distance));
            aquamentus.SpawnFireBall(attackPos, targetPos);
            double downFireAngle = angle + AttackRadian;
            targetPos = new Vector2((float)(attackPos.X - Math.Cos(downFireAngle) * distance), (float)(attackPos.Y + Math.Sin(downFireAngle) * distance));
            aquamentus.SpawnFireBall(attackPos, targetPos);           
        }

        public void ChangeDirection()
        {
            directionIndex *= -1;
        }

        public void Die()
        {
            //enemy die in damaged state
        }

        public void TakeDamage(int amount)
        {
            aquamentus.LostHP(amount);
            aquamentus.state = new AquamentusDamagedState(aquamentus, aquamentusPos, link, directionIndex, speed);
            aquamentus.SetSprite(EnemySpriteFactory.Instance.CreateDamagedDragonSprite());
        }

        public void Update()
        {
            aquamentusPos.X += directionIndex * speed;
            aquamentus.UpdatePos(aquamentusPos);
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            //aquamentus move in its own area
        }

        public void Stun()
        {
            //stun implement
        }

        public void Stun(bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}
