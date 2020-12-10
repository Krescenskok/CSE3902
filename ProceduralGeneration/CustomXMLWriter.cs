using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Sprint5
{
    /// <summary>
    /// Opens an XDocument to write and save to
    /// </summary>
    public class CustomXMLWriter
    {

        XDocument document;
        XElement root;

        string output;


        public CustomXMLWriter(string outputFile)
        {
          
            document = new XDocument();
            root = new XElement("Root");
            document.Add(root);
 

            output = outputFile;
        }

        public CustomXMLWriter(string fixFile, string fixName)
        {
            
            document = XDocument.Load("../../../XMLLoading/" + fixFile + ".xml");
            output = fixFile;
        }


        public void Save()
        {

            document.Save("../../../XMLLoading/" + output + ".xml");
          
        }

        public void AddRoom(int roomId,Room room,int gridSize, Point loc)
        {

            XElement roomElement = RoomWallDoorElements.Room(roomId, loc);

            if (room.hasLeftDoor) 
            { 
                roomElement.Add(RoomWallDoorElements.NormalDoor(roomId - 1, "left"));
                roomElement.Add(RoomWallDoorElements.LeftSplitWall()); 
            }
            else roomElement.Add(RoomWallDoorElements.LeftWall());

            if (room.hasRightDoor)
            {
                roomElement.Add(RoomWallDoorElements.NormalDoor(roomId + 1, "right"));
                roomElement.Add(RoomWallDoorElements.RightSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.RightWall());

            if (room.hasTopDoor)
            {
                roomElement.Add(RoomWallDoorElements.NormalDoor(roomId + gridSize, "top"));
                roomElement.Add(RoomWallDoorElements.TopSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.TopWall());

            if (room.hasBottomDoor)
            {
                roomElement.Add(RoomWallDoorElements.NormalDoor(roomId - gridSize, "bottom"));
                roomElement.Add(RoomWallDoorElements.BottomSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.BottomWall());

            roomElement.Add(EnemyElements.RandomEnemy(new Point(1, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(2, 4)));

            roomElement.Add(EnemyElements.RandomEnemy(new Point(3, 4)));

            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));



            root.Add(roomElement);


            
        }

        public void AddTrapRoom(int roomId, Room room, int gridSize, Point loc)
        {

            XElement roomElement = RoomWallDoorElements.Room(roomId, loc);

            if (room.hasLeftDoor)
            {
                roomElement.Add(RoomWallDoorElements.TrapDoor(roomId - 1, "left"));
                root.Add(RoomWallDoorElements.TrapDoorSprite("left", loc));
                roomElement.Add(RoomWallDoorElements.LeftSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.LeftWall());

            if (room.hasRightDoor)
            {
                roomElement.Add(RoomWallDoorElements.TrapDoor(roomId + 1, "right"));
                root.Add(RoomWallDoorElements.TrapDoorSprite("right", loc));
                roomElement.Add(RoomWallDoorElements.RightSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.RightWall());

            if (room.hasTopDoor)
            {
                roomElement.Add(RoomWallDoorElements.TrapDoor(roomId + gridSize, "top"));
                root.Add(RoomWallDoorElements.TrapDoorSprite("top", loc));
                roomElement.Add(RoomWallDoorElements.TopSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.TopWall());

            if (room.hasBottomDoor)
            {
                roomElement.Add(RoomWallDoorElements.TrapDoor(roomId - gridSize, "bottom"));
                root.Add(RoomWallDoorElements.TrapDoorSprite("bottom", loc));
                roomElement.Add(RoomWallDoorElements.BottomSplitWall());
            }
            else roomElement.Add(RoomWallDoorElements.BottomWall());

            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));
            roomElement.Add(EnemyElements.RandomEnemy(new Point(4, 4)));




            root.Add(roomElement);



        }



        #region method used to modify finallevelone.xml
        public void Fix(string element)
        {
            XElement asset = document.Element("XnaContent").Element("Asset");
            List<XElement> rooms = asset.Elements("Room").ToList();


            if (element.Equals("Room"))
            {
                foreach (XElement room in rooms.ToList())
                {
                    int roomNum = int.Parse(room.Attribute("id").Value);

                    if (roomNum == 1) { room.Add(new XAttribute("row", 5)); room.Add(new XAttribute("col", 2)); }
                    else if (roomNum == 2) { room.Add(new XAttribute("row", 5)); room.Add(new XAttribute("col", 1)); }
                    else if (roomNum == 3) { room.Add(new XAttribute("row", 5)); room.Add(new XAttribute("col", 3)); }
                    else if (roomNum == 4) { room.Add(new XAttribute("row", 4)); room.Add(new XAttribute("col", 2)); }
                    else if (roomNum == 5) { room.Add(new XAttribute("row", 3)); room.Add(new XAttribute("col", 2)); }
                    else if (roomNum == 6) { room.Add(new XAttribute("row", 3)); room.Add(new XAttribute("col", 1)); }
                    else if (roomNum == 7) { room.Add(new XAttribute("row", 3)); room.Add(new XAttribute("col", 3)); }
                    else if (roomNum == 8) { room.Add(new XAttribute("row", 2)); room.Add(new XAttribute("col", 2)); }
                    else if (roomNum == 9) { room.Add(new XAttribute("row", 2)); room.Add(new XAttribute("col", 1)); }
                    else if (roomNum == 10) { room.Add(new XAttribute("row", 2)); room.Add(new XAttribute("col", 0)); }
                    else if (roomNum == 11) { room.Add(new XAttribute("row", 2)); room.Add(new XAttribute("col", 3)); }
                    else if (roomNum == 12) { room.Add(new XAttribute("row", 2)); room.Add(new XAttribute("col", 4)); }
                    else if (roomNum == 13) { room.Add(new XAttribute("row", 1)); room.Add(new XAttribute("col", 2)); }
                    else if (roomNum == 14) { room.Add(new XAttribute("row", 1)); room.Add(new XAttribute("col", 4)); }
                    else if (roomNum == 15) { room.Add(new XAttribute("row", 1)); room.Add(new XAttribute("col", 5)); }
                    else if (roomNum == 16) { room.Add(new XAttribute("row", 0)); room.Add(new XAttribute("col", 2)); }
                    else if (roomNum == 17) { room.Add(new XAttribute("row", 0)); room.Add(new XAttribute("col", 1)); }
                    else if (roomNum == 18) { room.Add(new XAttribute("row", 1)); room.Add(new XAttribute("col", 1)); }

                }
            }

            #endregion



        }
    }
}
