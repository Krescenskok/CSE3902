using Microsoft.Xna.Framework;
using Sprint5;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint5
{
    public class Collision
    {
        
        private Direction dir;

     

        private Vector2 location;

        public ICollider other;

        public Collision(Direction dir, Vector2 location, ICollider other)
        {
            this.dir = dir;
            this.location = location;
            this.other = other;
        }

        public Collision()
        {

        }

        

        public bool Left { get => dir.Equals(Direction.left); }
        public bool Right { get => dir.Equals(Direction.right); }
        public bool Up { get => dir.Equals(Direction.up); }
        public bool Down { get => dir.Equals(Direction.down);}
        public Vector2 Location { get => location; }
        public Direction From { get => dir; }
    }
}
