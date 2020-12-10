using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public static class Directions
    {
        private static Random random = new Random();

        private static Direction Left { get => Direction.left; }
        private static Direction Right { get => Direction.right; }
        private static Direction Up { get => Direction.up; }
        private static Direction Down { get => Direction.down; }

        private static Direction Top { get => Direction.top; }
        private static Direction Bottom { get => Direction.bottom; }
        private static Direction None { get => Direction.none; }


        private static List<Direction> all = new List<Direction> { Left, Right, Up, Down };
        public static List<Direction> Default() { return new List<Direction>(all); }


        public static int CheckDirection(Direction dir, Direction pos, Direction neg)
        {
            if (dir.Equals(pos)) return 1;
            if (dir.Equals(neg)) return -1;
            
            return 0;
        }

        public static int CheckDirection(List<Direction> newDir, Direction pos, Direction neg)
        {
            if (newDir.Contains(pos)) return 1;
            if (newDir.Contains(neg)) return -1;
            return 0;
        }

        public static int CalculateX(Direction dir)
        {
            if (dir.Equals(Right)) return 1;
            if (dir.Equals(Left)) return -1;
            return 0;
        }

        public static int CalculateY(Direction dir)
        {
            if (dir.Equals(Down)) return 1;
            if (dir.Equals(Up)) return -1;
            return 0;
        }

        public static Direction RandomDirection(List<Direction> directions)
        {
            int rand = random.Next(0, directions.Count);
            return directions[rand];
        }

       
        public static Direction Parse(string str)
        {
            if (str.Contains("left") || str.Contains("Left")) return Left;
            else if (str.Contains("right") || str.Contains("Right")) return Right;
            else if (str.Contains("up") || str.Contains("Up")) return Up;
            else if (str.Contains("top") || str.Contains("Top")) return Top;
            else if (str.Contains("bottom") || str.Contains("Bottom")) return Bottom;
            else if (str.Contains("down") || str.Contains("Down")) return Down;

            return None;
        }

        public static Vector3 ParseVector(string str)
        {
            if (str == "left" || str == "Left") return Vector3.Right;
            else if (str == "right" || str == "Right") return Vector3.Left;
            else if (str == "up" || str == "Up" || str == "top" || str == "Top") return Vector3.Up;
            else if (str == "down" || str == "Down" || str == "bottom" || str == "Bottom") return Vector3.Down;

            return Vector3.Zero;
        }

        /// <summary>
        /// likelihood on scale from 0 to 1, least to most likely respectively
        /// </summary>
        /// <param name="likelihood"></param>
        /// <returns></returns>
        public static bool Chance(float likelihood)
        {
            float chanceInHundred = (likelihood * 10);
            int randomCap = (int)(100 / chanceInHundred);
            return random.Next(0, randomCap) == 0;
        }

        public static Direction Opposite(Direction dir)
        {
            if (dir.Equals(Left)) return Right;
            else if (dir.Equals(Right)) return Left;
            else if (dir.Equals(Up)) return Down;
            else return Up;
        }

        public static bool Opposites(Direction dir1, Direction dir2)
        {
            return Opposite(dir1).Equals(dir2);
        }
    }
}
