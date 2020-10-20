using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint3
{
    public class Collision
    {
        public enum Direction { left, right, up, down };
        private Direction dir;

     

        private Vector2 location;

        public Collision(Direction dir, Vector2 location)
        {
            this.dir = dir;
            this.location = location;
        }

        public Collision()
        {

        }
        

        public bool Left() { return dir.Equals(Direction.left); }
        public bool Right() { return dir.Equals(Direction.right); }
        public bool Up() { return dir.Equals(Direction.up); }
        public bool Down() { return dir.Equals(Direction.down);}
        public Vector2 Location() { return location; }
        public Direction From() { return dir; }
    }
}
