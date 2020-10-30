using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    public class GelBlockCollider : ICollider
    {
        private Rectangle bounds;
        private GelMoveState gel;

        public string Name {get =>  "GelBlock";}
        public Layer layer { get; set; }

        public GelBlockCollider(Rectangle rect, GelMoveState gel)
        {
            bounds = rect;

            this.gel = gel;

            CollisionHandler.Instance.AddCollider(this);
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
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("block") || col.CompareTag("wall") || col.CompareTag("PlayerWeapon"))
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

        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage") gel.TakeDamage((int)value);

            

        }

        public void Update(Point point)
        {
            bounds.Location = point;
        }
    }
}
