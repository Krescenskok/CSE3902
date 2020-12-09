using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Sprint5
{
    public class RoomWriter
    {

        XDocument document;
        XElement root;

        string output;


        public RoomWriter(string outputFile)
        {
          
            document = new XDocument();
            root = new XElement("Root");
            document.Add(root);
 

            output = outputFile;
        }

        public RoomWriter(string fixFile, string fixName)
        {
            
            document = XDocument.Load("../../../XMLLoading/" + fixFile + ".xml");
            output = fixFile;
        }


        public void Save()
        {

            document.Save("../../../XMLLoading/" + output + ".xml");
          
        }

        public void AddRoom(int roomId, bool left, bool right, bool up, bool down, int gridSize, Point loc)
        {

            XElement room = RoomElements.Room(roomId, loc);

            if (left) 
            { 
                room.Add(RoomElements.NormalDoor(roomId - 1, "Left"));
                room.Add(RoomElements.LeftSplitWall()); 
            }
            else room.Add(RoomElements.LeftWall());

            if (right)
            {
                room.Add(RoomElements.NormalDoor(roomId + 1, "Right"));
                room.Add(RoomElements.RightSplitWall());
            }
            else room.Add(RoomElements.RightWall());

            if (up)
            {
                room.Add(RoomElements.NormalDoor(roomId + gridSize, "Up"));
                room.Add(RoomElements.TopSplitWall());
            }
            else room.Add(RoomElements.TopWall());

            if (down)
            {
                room.Add(RoomElements.NormalDoor(roomId - gridSize, "Down"));
                room.Add(RoomElements.BottomSplitWall());
            }
            else room.Add(RoomElements.BottomWall());

            room.Add(RoomElements.Enemy("Stalfos", new Point(4, 4)));


            root.Add(room);

            Save();
            
        }

        public void Fix()
        {
            XElement asset = document.Element("XnaContent").Element("Asset");
            List<XElement> rooms = asset.Elements("Room").ToList();

            foreach(XElement room in rooms.ToList())
            {
                List<XElement> doors = room.Elements("Item").ToList();

                foreach (XElement door in doors.ToList())
                {
                    if (door.Element("ObjectType").Value.Equals("Door"))
                    {
                        door.Name = "Door";
                        door.Element("ObjectType").Remove();
                        door.Element("ObjectName").Name = "Direction";
                    }
                }
            }
            
        }
    }
}
