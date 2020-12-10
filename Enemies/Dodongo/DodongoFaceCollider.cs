using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    class DodongoFaceCollider : ICollider
    {
        private Rectangle bounds;
        private IEnemyState dodongo;
        private Dodongo dodongoBase;
        private string name;

        public string Name { get => name; }
        public Layer layer { get; set; }

        public DodongoFaceCollider(Rectangle rect, IEnemyState dodongo, Dodongo dongo)
        {
            bounds = rect;
            this.dodongo = dodongo;
            this.name = "DodongoFace";
            dodongoBase = dongo;
            CollisionHandler.Instance.AddCollider(this, Layers.Enemy);

        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag.Equals(name);
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

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }

        public void SendMessage(string msg, object value)
        {
            if (msg.Equals("Bomb"))
            {
                dodongo.TakeDamage(-1);
            }
            
        }

        public void Update(Point point)
        {
            bounds.Location = point;
        }

        public void Update()
        {
            bounds.Location = dodongoBase.UpdateFacePos();
        }
    }
}
