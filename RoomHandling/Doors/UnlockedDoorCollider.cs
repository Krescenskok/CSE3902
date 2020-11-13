using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class UnlockedDoorCollider : IDoorCollider
    {

        public string name;
        public Rectangle bounds;
        public char orientation;
        public Door door;

        public string Name { get => name; }
        public Layer layer { get; set; }


        public UnlockedDoorCollider(Door door, Point location, Point size, char orient)
        {
            bounds.Location = location;
            bounds.Size = size;
            orientation = orient;
            this.door = door;

            CollisionHandler.Instance.AddCollider(this, Layers.Door);
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
                col.SendMessage("Door", null);
            }
        }

        public void SendMessage(string msg, object value)
        {
           if (msg == "Enter")
            {
                this.door.ChangeRoom();
            }
        }

        public void Update()
        {
            //doesn't update
        }
    }
}
