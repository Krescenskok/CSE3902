using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public struct Room
    {
        public bool hasRightDoor { get; set; }
        public bool hasLeftDoor { get; set; }
        public bool hasTopDoor { get; set; }
        public bool hasBottomDoor { get; set; }

        public Point location { get; set; }
    }
}
