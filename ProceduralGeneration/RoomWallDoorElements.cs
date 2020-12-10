using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public static class RoomWallDoorElements
    {

        public static XElement Room(int id, Point location)
        {
            return new XElement("Room", new XAttribute("id", id), new XAttribute("row",location.Y),new XAttribute("col",location.X));
        }


        public static XElement NormalDoor(int connectedRoom, string direction)
        {
            return new XElement("Door",
                new XAttribute("Type", "normal" + direction),
                new XAttribute("Room", connectedRoom));
        }

        public static XElement TrapDoor(int connectedRoom, string direction)
        {
            return new XElement("Door",
                new XAttribute("Type", "open" + direction),
                new XAttribute("Room", connectedRoom));
        }

        public static XElement TrapDoorSprite(string direction,Point loc)
        {
            int row = loc.Y, col = loc.X;
            return new XElement("LockedDoor",
                new XAttribute("Drawn", "false"),
                new XAttribute("Type", "open" + direction),
                new XAttribute("Row",row),
                new XAttribute("Col",col),
                new XAttribute("Trap","true"));
        }

        public static XElement LeftWall()
        {
            return new XElement("Wall",
                new XAttribute("Type", "Left"));
        }

        public static XElement RightWall()
        {
            return new XElement("Wall",
                new XAttribute("Type", "Right"));
        }

        public static XElement TopWall()
        {
            return new XElement("Wall",
                new XAttribute("Type", "Top"));
        }

        public static XElement BottomWall()
        {
            return new XElement("Wall",
                new XAttribute("Type", "Bottom"));
        }

        public static List<XElement> LeftSplitWall()
        {
            List<XElement> list = new List<XElement>();
            list.Add(new XElement("Wall",
                new XAttribute("Type", "LeftTop")));
            list.Add(new XElement("Wall",
                new XAttribute("Type","LeftBottom")));
            return list;
        }

        public static List<XElement> RightSplitWall()
        {
            List<XElement> list = new List<XElement>();
            list.Add(new XElement("Wall",
                new XAttribute("Type","RightTop")));
            list.Add(new XElement("Wall",
                new XAttribute("Type","RightBottom")));
            return list;
        }

        public static List<XElement> TopSplitWall()
        {
            List<XElement> list = new List<XElement>();
            list.Add(new XElement("Wall",
                new XAttribute("Type","TopLeft")));
            list.Add(new XElement("Wall",
                new XAttribute("Type","TopRight")));
            return list;
        }
        public static List<XElement> BottomSplitWall()
        {
            List<XElement> list = new List<XElement>();
            list.Add(new XElement("Wall",
                new XAttribute("Type","BottomLeft")));
            list.Add(new XElement("Wall",
                new XAttribute("Type","BottomRight")));
            return list;
        }


    }
}
