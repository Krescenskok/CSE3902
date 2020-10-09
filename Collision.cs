using Sprint2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public class Collision
    {
        public enum Direction { left, right, up, down };
        private Direction dir;

        private Direction left = Direction.left;
        private Direction right = Direction.left;
        private Direction up = Direction.left;
        private Direction down = Direction.left;

        public Collision(Direction dir)
        {
            this.dir = dir;
        }

        public bool Left() { return dir.Equals(left); }
        public bool Right() { return dir.Equals(right); }
        public bool Up() { return dir.Equals(up); }
        public bool Down() { return dir.Equals(down);}
    }
}
