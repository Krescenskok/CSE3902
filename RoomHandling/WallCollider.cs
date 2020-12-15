using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class WallCollider : ICollider
    {
       
        public Rectangle bounds;

        public string Name { get => "Wall"; }
        public Layer layer { get; set; }


        public WallCollider(Point location, Point size)
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
            return tag == "Wall" || tag == "wall";
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

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
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
