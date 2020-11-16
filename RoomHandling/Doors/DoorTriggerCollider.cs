using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    class DoorTriggerCollider : ICollider
    {

        public string name;
        public Rectangle bounds;
        public char orientation;

        public string Name { get => "DoorTrigger"; }
        public Layer layer { get; set; }

        private int doorTrigger;
        private Func<bool> condition;

        public DoorTriggerCollider(Point location, Point size, int doorNum, Func<bool> condition)
        {
            bounds.Location = location;
            bounds.Size = size;
            
            doorTrigger = doorNum;
            this.condition = condition;

            CollisionHandler.Instance.AddCollider(this, Layers.Door);
        }


        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Doorway";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            //no action
        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") && condition())
            {
                RoomDoors.Instance.CloseDoor(doorTrigger);
                CollisionHandler.Instance.RemoveCollider(this);
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

        }

        public void SendMessage(string msg, object value)
        {

        }

        public void Update()
        {
            Debug.WriteLine(condition());

        }
    }
}
