using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class EnemyCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemyState enemy;
        private float damageAmount;

        public EnemyCollider(Rectangle rect, IEnemyState enemy, float strength)
        {
            bounds = rect;

            this.enemy = enemy;

            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this);
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
            if (col.CompareTag("Player"))
            {
                col.SendMessage("TakeDamage", collision);
            }
            else if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall"))
            {
                enemy.MoveAwayFromCollision(collision);
                Debug.WriteLine("hit wall");
            }
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage") enemy.TakeDamage();
        }

        public void Update(Point point)
        {
            bounds.Location = point;
        }
    }
}
