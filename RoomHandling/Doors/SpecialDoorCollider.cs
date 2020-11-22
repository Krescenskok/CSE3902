using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class SpecialDoorCollider : IDoorCollider
    {

        public string name;
        public Rectangle bounds;
        public Door door;
        public string Name { get => "LockedDoor"; }
        public Layer layer { get; set; }


        public SpecialDoorCollider(Door door, Point location, Point size)
        {
            bounds.Location = location;
            bounds.Size = size;
            this.door = door;
           

            CollisionHandler.Instance.AddCollider(this, Layers.Door);


        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Doorway" || tag == "Door";
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
            if (col.CompareTag("Projectile"))
            {
                col.SendMessage("Impact", 0);
            }
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
