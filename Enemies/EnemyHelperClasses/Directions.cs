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
        private static Direction None { get => Direction.none; }



        private static List<Direction> all = new List<Direction> { Left, Right, Up, Down };
        public static List<Direction> Default() { return new List<Direction>(all); }
        public static List<Direction> LeftUp { get; } =  new List<Direction> { Left, Up };
        public static List<Direction> RightUp { get; } = new List<Direction> { Right, Up };
        public static List<Direction> DownRight { get; } = new List<Direction> { Right, Down };
        public static List<Direction> DownLeft { get; } = new List<Direction> { Left, Down };

        public static int CheckDirection(Direction dir, Direction pos, Direction neg)
        {
            if (dir.Equals(pos)) return 1;
            if (dir.Equals(neg)) return -1;
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
            if (str == "left" || str == "Left") return Left;
            else if (str == "right" || str == "Right") return Right;
            else if (str == "up" || str == "Up" || str == "top" || str == "Top") return Up;
            else if (str == "down" || str == "Down" || str == "bottom" || str == "Bottom") return Down;

            return None;
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
    }
}
