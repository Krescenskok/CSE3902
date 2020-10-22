using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final.Enemies.Dodongo
{
    class DodongoFaceCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemyState dodongo;
        private int damageAmount;

        public string Tag { get => "dodongo"; }

        public DodongoFaceCollider(Rectangle rect, IEnemyState dodongo, int strength)
        {
            bounds = rect;
            this.dodongo = dodongo;
            damageAmount = strength;
            CollisionHandler.Instance.AddCollider(this);

        }

        

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "dodongo" || tag == "Dodongo";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if(col.CompareTag("Bomb") || col.CompareTag("bomb"))
            {
                col.SendMessage("Eaten", 0);
                dodongo.TakeDamage(-1);
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if(col.CompareTag("Bomb") || col.CompareTag("bomb"))
            {
                col.SendMessage("Eaten", 0);
                dodongo.TakeDamage(-1);
            }
        }

        public void SendMessage(string msg, object value)
        {
        }

        public void Update(Point point)
        {
            bounds.Location = point;
        }
    }
}
