using Microsoft.Xna.Framework;
using Sprint5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Sprint5.Menus;

namespace Sprint5
{
    public class EnemyCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemy enemy;
        private int damageAmount;
        public string name;
        private Point hitboxOffset;

        public Vector2 Center => bounds.Center.ToVector2();

        public string Name { get => name; }
        public Layer layer { get; set; }

        public EnemyCollider(Rectangle rect, IEnemy enemy, int strength)
        {
            bounds = rect;

            hitboxOffset = (rect.Location - enemy.Location.ToPoint());

            this.enemy = enemy;

            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this,Layers.Enemy);
            name = "Enemy";


        }

        public EnemyCollider(Rectangle rect, IEnemy enemy, int strength, string name)
        {
            bounds = rect;
            hitboxOffset = (rect.Location - enemy.Location.ToPoint());
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
            
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") ||col.CompareTag("Trap") || col.CompareTag("Doorway"))
            {
                enemy.ObstacleCollision(collision);
                
            }

            WallMaster master = enemy as WallMaster;
            if (name.Equals("WallMaster") && col.CompareTag("Player")) { 
                master.Attack(); col.SendMessage("Hand", master.Location);
            }

        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string direction = collision.From.ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);


            WallMaster master = enemy as WallMaster;
            if (col.CompareTag("Player")) col.SendMessage("TakeDamage" + direction, damageAmount);
            if (name.Equals("WallMaster") && col.CompareTag("Player")) { master.Attack(); col.SendMessage("Hand", master.Location); }
            else if (col.CompareTag("Item") || col.CompareTag("Projectile")) col.SendMessage("Impact", 0);
        }

        public void SendMessage(string msg, object value)
        {


            if (msg == "Stun") { enemy.Stun(); Sounds.Instance.Play("EnemyHit"); }
            else if (msg.Contains("EnemyTakeDamage"))
            {
                Direction dir = Directions.Parse(msg.Substring(15));

                StatsScreen.Instance.DamageGiven += (int)value;

                enemy.TakeDamage(dir, (int)value);

                Sounds.Instance.Play("EnemyHit");


            }

          



        }


        public void Update()
        {
            if (name == "gel")
            {
                bounds.Location = (enemy.Location.ToPoint() + hitboxOffset+ (enemy as Gel).gSprite.centerOffset);
            } else
            {
                bounds.Location = (enemy.Location.ToPoint() + hitboxOffset);
            }

           
        }
    }
}
