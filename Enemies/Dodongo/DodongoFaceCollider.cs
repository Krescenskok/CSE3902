using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.Enemies.Dodongo
{
    class DodongoFaceCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemyState dodongo;

        public string Name { get => "dodongo"; }
        public Layer layer { get; set; }

        public DodongoFaceCollider(Rectangle rect, IEnemyState dodongo)
        {
            bounds = rect;
            this.dodongo = dodongo;
            CollisionHandler.Instance.AddCollider(this);

        }

        

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "DodongoFace";
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
            if(col.CompareTag("Bomb") || col.CompareTag("bomb"))
            {
                col.SendMessage("Eaten", 0);
            }
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "Bomb")
            {
                dodongo.TakeDamage(-5);
            }
            
        }

        public void Update(Point point)
        {
            bounds.Location = point;
        }
    }
}
