using Microsoft.Xna.Framework;
using Sprint2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public class EnemyCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemyState enemy;
        private float damageAmount;

        public EnemyCollider(Rectangle rect, IEnemyState enemy)
        {
            bounds = rect;
            this.enemy = enemy;
        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "enemy" || tag == "Enemy";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") && collision.Right())
            {
                col.SendMessage("TakeDamage", damageAmount);
            }
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage") enemy.TakeDamage();
        }
    }
}
