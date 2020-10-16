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
    class FireBall
    {
        private ISprite sprite;
        private Vector2 fireBallPos;
        private Vector2 targetPos;
        private double distanceX, distanceY;
        private double deltaX, deltaY;
        private double speedPerSec = 20;
        private int updatePerSec = 30;

        public FireBall(Vector2 initialPos, Vector2 targetPos)
        {
            fireBallPos = initialPos;
            this.targetPos = targetPos;
            distanceX = targetPos.X - initialPos.X;
            distanceY = targetPos.Y - initialPos.Y;
            double overallDistance = Math.Sqrt(Math.Pow(distanceX, 2) + Math.Pow(distanceY, 2));
            double ratioToSpeed = speedPerSec / overallDistance;
            deltaX = ratioToSpeed * distanceX / updatePerSec;
            deltaY = ratioToSpeed * distanceY / updatePerSec;
            sprite = EnemySpriteFactory.Instance.CreateFireBall();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, fireBallPos, 0, Color.White);
        }


        public void TakeDamage()
        {
            //FireBall wont TakeDamage
        }

        public void Update()
        {
            fireBallPos.X += (float)deltaX;
            fireBallPos.Y += (float)deltaY;
        }
    }
}
