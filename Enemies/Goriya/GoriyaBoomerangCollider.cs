using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Enemies
{
    public class GoriyaBoomerangCollider : ICollider
    {

        private Rectangle bounds;
        
        private int damageAmount;

        GoriyaBoomerang boomerang;

        public string Name { get => "Enemy"; }
        public Layer layer { get; set; }

        public GoriyaBoomerangCollider(GoriyaBoomerang boomerang, Rectangle rect, int strength)
        {
            bounds = rect;

            this.boomerang = boomerang;

            damageAmount = strength;

            CollisionHandler.Instance.AddCollider(this, Layers.Enemy);
        }

       

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "enemy" || tag == "Enemy" || tag == "EnemyBoomerang";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string direction = collision.From.ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);

            if (col.CompareTag("Player"))
            {
                    col.SendMessage("TakeDamage" + direction, damageAmount);
                }
            else if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") || col.CompareTag("PlayerWeapon"))
            {
                boomerang.BounceOff(collision);

            }


        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void SendMessage(string msg, object value)
        {
           //nothing
        }

        public void Update()
        {
            bounds.Location = boomerang.Location;
        }
    }
}
