using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class DoorEntranceCollider : IDoorCollider
    {

        public string name;
        public Rectangle bounds;
        public char orientation;
        public Door door;

        public string Name { get => "DoorEntrance"; }
        public Layer layer { get; set; }

        private bool trigger;
        private int doorTrigger;

        public DoorEntranceCollider(Door door, Point location, Point size, char orient, bool trigger)
        {
            bounds.Location = location;
            bounds.Size = size;
            orientation = orient;
            this.door = door;
            this.trigger = trigger;

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
            if (col.CompareTag("Player") && trigger && RoomEnemies.Instance.EnemyCount > 0)
            {
                RoomDoors.Instance.CloseDoor(6);
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Projectile"))
            {
                col.SendMessage("Impact", 0);
            }
        }

        public void SendMessage(string msg, object value)
        {
            
        }

        public void Update()
        {
            
            
        }
    }
}
