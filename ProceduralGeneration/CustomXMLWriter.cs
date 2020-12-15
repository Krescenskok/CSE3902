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

        private XDocument document;
        private XElement root;

        private string output;


        public CustomXMLWriter(string outputFile)
        {
          
            document = new XDocument();
            root = new XElement("Root");
            document.Add(root);
 

            output = outputFile;
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
    }
}
