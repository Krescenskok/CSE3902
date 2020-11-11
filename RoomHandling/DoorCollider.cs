using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class DoorCollider : ICollider
    {

        public string name;
        public Rectangle bounds;

        public string Name { get => name; }
        public Layer layer { get; set; }


        public DoorCollider(Point location, Point size)
        {
            bounds.Location = location;
            bounds.Size = size;

            CollisionHandler.Instance.AddCollider(this, Layers.Wall);
        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Door" || tag == "door";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            //no action
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player"))
            {
               
            }
        }

        public void SendMessage(string msg, object value)
        {
           
        }

        public void Update()
        {
            //doesn't update
        }
    }
}
