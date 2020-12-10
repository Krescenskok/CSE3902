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

        public Door door;

        public string Name { get => name; }
        public Layer layer { get; set; }

        



        public DoorEntranceCollider(Door door, Point location, Point size)
        {
            bounds.Location = location;
            bounds.Size = size;

            this.door = door;
            name = "DoorEntrance";

            CollisionHandler.Instance.AddCollider(this, Layers.Door);

            RoomEnemies.Instance.AddTestCollider(bounds, this);
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

        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

        }

        public void SendMessage(string msg, object value)
        {
            
        }

        public void Update()
        {
            
            
        }
    }
}
