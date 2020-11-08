using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    class FireBallState : IEnemyState
    {
        private Aquamentus aquamentus;
        private FireBall fireBall;
        private Vector2 fireBallPos;
        private Vector2 targetPos;
        private double distanceX, distanceY;
        private double deltaX, deltaY;
        private double speedPerSec = 20;
        private int updatePerSec = 30;

        public FireBallState(FireBall fb, Aquamentus aquamentus, Vector2 initialPos, Vector2 targetPos)
        {
            this.aquamentus = aquamentus;
            fireBall = fb;
            fireBallPos = initialPos;
            this.targetPos = targetPos;
            distanceX = targetPos.X - initialPos.X;
            distanceY = targetPos.Y - initialPos.Y;
            double overallDistance = Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
            double ratioToSpeed = speedPerSec / overallDistance;
            deltaX = ratioToSpeed * distanceX / updatePerSec;
            deltaY = ratioToSpeed * distanceY / updatePerSec;
        }

        public void Attack()
        {
            //wont use
        }

        public void ChangeDirection()
        {
            //wont use
        }

        public void Die()
        {
            CollisionHandler.Instance.RemoveCollider(fireBall.Colliders);
            aquamentus.RemoveFireBall(fireBall);
        }

        public void MoveAwayFromCollision(Collision collision)
        {
            Die();
        }

        public void Stun()
        {
            //im guessing does nothing
        }

        public void TakeDamage(int amount)
        {
            //wont use
        }

        public void Update()
        {
            fireBallPos.X += (float)deltaX;
            fireBallPos.Y += (float)deltaY;
            fireBall.ChangePos(fireBallPos);
        }
    }
}
