using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class GelBlockCollider : ICollider
    {
        private Rectangle bounds;
        private GelMoveState gel;
        private IMoveable obj;

        public string Name {get =>  "GelBlock";}
        public Layer layer { get; set; }

        public GelBlockCollider(Rectangle rect, GelMoveState gel, IMoveable gelly)
        {
            bounds = rect;

            this.gel = gel;
            obj = gelly;

            CollisionHandler.Instance.AddCollider(this, Layers.Enemy);
        }
        public GelBlockCollider()
        {

        }
        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "GelBlock";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall"))
            {
                
                gel.CheckIfBlockingPath(col, collision);

                
            }
            else
            {
                gel.FreeToMove();
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

            
        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage") gel.TakeDamage((int)value);

            

        }

        public void Update()
        {
            Point point = obj.Location.ToPoint();
            point.X -= 5;
            point.Y -= 5;
            bounds.Location = point;
        }
    }
}
