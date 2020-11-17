using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Enemies.EnemyHelperClasses
{
    class FireballCollider : ICollider
    {
        private Rectangle bounds;
        private int damageAmount;
        private IEnemy enemy;
        private LinkPlayer link;
        public string name;
        public string Name { get => name; }
        public Layer layer { get; set; }

        public FireballCollider(Rectangle rect, IEnemy enemy, int strength, string name, LinkPlayer link)
        {
            damageAmount = strength;
            bounds = rect;
            enemy = this.enemy;
            name = this.name;
            link = this.link;
            CollisionHandler.Instance.AddCollider(this, Layers.Projectile);
        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == name;
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") || col.CompareTag("Trap") || col.CompareTag("Doorway"))
            {
                enemy.ObstacleCollision(collision);
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
            string direction = collision.From.ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);

            if (col.CompareTag("Player"))
            {
                enemy.ObstacleCollision(collision);
                if(link.LinkDirection != direction || !link.LargeShield || link.IsAttacking)
                {
                    col.SendMessage("TakeDamage" + direction, damageAmount);
                }
            }
        }

        public void SendMessage(string msg, object value)
        {
        }

        public void Update()
        {
            bounds.Location = enemy.Location.ToPoint();
        }
    }
}
