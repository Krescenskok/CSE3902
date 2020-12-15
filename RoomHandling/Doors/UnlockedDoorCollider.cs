using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class UnlockedDoorCollider : IDoorCollider
    {

        public string name = "Unlocked";
        public Rectangle bounds;
        public Direction entrySide;
        public Door door;

        public string Name { get => name; }
        public Layer layer { get; set; }

        private bool trigger = false;

        public UnlockedDoorCollider(Door door, Point location, Point size, Direction dir)
        {
            bounds.Location = location;
            bounds.Size = size;
            entrySide = dir;

            if (dir.Equals(Direction.top)) entrySide = Direction.up;
            else if (dir.Equals(Direction.bottom)) entrySide = Direction.down;
            
            this.door = door;

            CollisionHandler.Instance.AddCollider(this, Layers.Door);
        }

        public UnlockedDoorCollider(Door door, Point location, Point size, char orient)
        {
            bounds.Location = location;
            bounds.Size = size;

            if (orient == 'C') entrySide = Direction.right;
            else if (orient == 'S') entrySide = Direction.up;


            this.door = door;

            CollisionHandler.Instance.AddCollider(this, Layers.Door);
        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "UnlockedDoor" || tag == entrySide.ToString();
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") && collision.From.Equals(entrySide) && trigger == true)
            {
                door.ChangeRoom();
                trigger = false;
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") && !collision.From.Equals(entrySide))
            {
                trigger = true;
               
            }
        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {


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
