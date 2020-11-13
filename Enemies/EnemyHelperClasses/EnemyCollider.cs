using Microsoft.Xna.Framework;
using Sprint4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    public class EnemyCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemy enemy;
        private int damageAmount;
        public string name;

        public string Name { get => name; }
        public Layer layer { get; set; }

        public EnemyCollider(Rectangle rect, IEnemy enemy, int strength)
        {
            bounds = rect;

            this.enemy = enemy;

            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this,Layers.Enemy);

            name = "Enemy";
        }

        public EnemyCollider(Rectangle rect, IEnemy enemy, int strength, string name)
        {
            bounds = rect;

            this.enemy = enemy;
            this.name = name;
            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this, Layers.Enemy);
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
            return tag == name || tag == "Enemy";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") ||col.CompareTag("Trap"))
            {
                enemy.ObstacleCollision(collision);
                
            }

            WallMaster master = enemy.State as WallMaster;
            if (name.Equals("WallMaster") && col.CompareTag("Player")) { master.Attack(); col.SendMessage("Hand", master.Location); }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string direction = collision.From.ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);


            WallMaster master = enemy.State as WallMaster;
            if (col.CompareTag("Player")) col.SendMessage("TakeDamage" + direction, damageAmount);
            if (name.Equals("WallMaster") && col.CompareTag("Player")) { master.Attack(); col.SendMessage("Hand", master.Location); }
            else if (col.CompareTag("Item")) col.SendMessage("Impact", 0);
        }

        public void SendMessage(string msg, object value)
        {


            if (msg == "Stun") enemy.Stun();
            else if (msg.Contains("EnemyTakeDamage"))
            {
                Direction dir = Directions.Parse(msg.Substring(15));

                enemy.TakeDamage(dir, (int)value);

                Sounds.Instance.PlayEnemyHit();
                Sounds.Instance.StartLowHealthLoop();
            }

          



        }


        public void Update()
        {
            bounds.Location = enemy.Location.ToPoint();
        }
    }
}
