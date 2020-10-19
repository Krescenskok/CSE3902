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
        private int damageAmount;

        public EnemyCollider(Rectangle rect, IEnemyState enemy, int strength)
        {
            bounds = rect;

            this.enemy = enemy;

            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this);
        }

        public EnemyCollider()
        {

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
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall"))
            {
                enemy.MoveAwayFromCollision(collision);
                
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player"))
            {
                col.SendMessage("TakeDamage", damageAmount);
            }
            else if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") || col.CompareTag("PlayerWeapon"))
            {
                enemy.MoveAwayFromCollision(collision);
                
            }

            
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage") enemy.TakeDamage((int)value);
        }

        public void Update(Point point)
        {
            bounds.Location = point;
        }
    }
}
