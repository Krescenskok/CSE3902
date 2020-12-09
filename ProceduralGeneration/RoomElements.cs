using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public static class RoomElements
    {

        public static XElement Room(int id, Point location)
        {
            return new XElement("Room", new XAttribute("id", id), new XAttribute("row",location.Y),new XAttribute("col",location.X));
        }

        public static XElement Enemy(string name, Point location)
        {
            return new XElement("Item",
                new XElement("ObjectType", "Enemy"),
                new XElement("ObjectName", name),
                new XElement("Location", location.Y + " " + location.X),
                new XElement("Alive", "true"));
        }
        public static XElement NormalDoor(int connectedRoom, string direction)
        {
            return new XElement("Door",
                new XElement("Direction", direction),
                new XElement("Type", "normal"),
                new XElement("RoomNum", connectedRoom));
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
