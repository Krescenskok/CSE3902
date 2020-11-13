﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class LockedDoorCollider : IDoorCollider
    {

        public string name;
        public Rectangle bounds;
        public char orientation;
        public Door door;

        public string Name { get => name; }
        public Layer layer { get; set; }


        public LockedDoorCollider(Door door, Point location, Point size, char orient)
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
            return tag == "LockedDoor" || tag == "lockeddoor";
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
                col.SendMessage("LockedDoor", null);
            }
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "Unlock")
            {
                this.door.Unlock();
            }
        }

        public void Update()
        {
            //doesn't update
        }
    }
}
