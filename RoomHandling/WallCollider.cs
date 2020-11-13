﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class WallCollider : ICollider
    {
        public string name;
        public Rectangle bounds;

        public string Name { get => name; }
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
            //no action
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            //no action
        }

        public void SendMessage(string msg, object value)
        {
            if(msg == "Bomb") { 
                //open hole in wall?
            }
        }

        public void Update()
        {
            //doesn't update
        }
    }
}
