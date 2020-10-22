using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public class NPCCollider : ICollider
    {
        private Rectangle bounds;

        public NPCCollider(Rectangle rectangle)
        {
            bounds = rectangle;
            CollisionHandler.Instance.AddCollider(this);
        }

        public string Tag { get => "NPC"; }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "NPC";
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
