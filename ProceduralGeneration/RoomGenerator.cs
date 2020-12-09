using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public static class RoomGenerator
    {
        private struct Room
        {
            public bool hasRightDoor { get; set; }
            public bool hasLeftDoor { get; set; }
            public bool hasTopDoor { get; set; }
            public bool hasBottomDoor;

            public Point location;
        }

        private static List<List<Room>> rooms = new List<List<Room>>();

        private static int size;

        public static void GenerateRooms(RoomWriter document, List<List<int>> inputRows, int bottomRowNum)
        {
            size = inputRows[0].Count;


            if(inputRows.Count == 2)
            {
                List<int> row1 = inputRows[0];
                List<int> row2 = inputRows[1];
                rooms.Add(new List<Room>());

                for (int i = 0; i < size; i++)
                {
                    Room room = new Room();
                    room.location = new Point(i, size - bottomRowNum - 1);
                    int set = row1[i];

                    room.hasLeftDoor = i > 0 && set == row1[i - 1];
                    room.hasRightDoor = i < size - 1 && set == row1[i + 1];
                    room.hasBottomDoor = bottomRowNum > 0 && rooms[bottomRowNum - 1][i].hasTopDoor;
                    room.hasTopDoor = set == row2[i];

                    
                    rooms[bottomRowNum].Add(room);
                }
            }
            else
            {
                List<int> row = inputRows[0];
                rooms.Add(new List<Room>());

                for (int i = 0; i < size; i++)
                {
                    Room room = new Room();
                    room.location = new Point(i, size - bottomRowNum - 1);
                    int set = row[i];

                    room.hasLeftDoor = i > 0 && set == row[i - 1];
                    room.hasRightDoor = i < size - 1 && set == row[i + 1];
                    room.hasBottomDoor = rooms[bottomRowNum - 1][i].hasTopDoor;
                    room.hasTopDoor = false;

                    rooms[bottomRowNum].Add(room);
                }
            }
         




          
        }


        public static void FinishGeneration(RoomWriter document)
        {
            int index = 0;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    document.AddRoom(index++, rooms[i][j].hasLeftDoor, rooms[i][j].hasRightDoor, rooms[i][j].hasTopDoor, rooms[i][j].hasBottomDoor, size, rooms[i][j].location);
                }
                
            }

            document.Save();
        }
       


        public static List<RoomSprite> RoomSprites(XElement xml)
        {
            List<RoomSprite> sprites = new List<RoomSprite>();
            List<XElement> rooms = xml.Elements("Room").ToList();

            foreach(XElement room in rooms)
            {
                int row = int.Parse(room.Attribute("row").Value);
                int col = int.Parse(room.Attribute("col").Value);


                RoomSprite newRoom = RoomSpriteFactory.instance.NewRoom(row, col);
                List<XElement> doors = room.Elements("Door").ToList();

                int num = 0;
                foreach(XElement door in doors)
                {
                    string direction = door.Element("Direction").Value;
                    if (direction.Equals("Left")) RoomSpriteFactory.instance.AddLeftDoor(ref newRoom);
                    else if(direction.Equals("Right")) RoomSpriteFactory.instance.AddRightDoor(ref newRoom);
                    else if (direction.Equals("Up")) RoomSpriteFactory.instance.AddTopDoor(ref newRoom);
                    else if (direction.Equals("Down")) RoomSpriteFactory.instance.AddBottomDoor(ref newRoom);

                    num++;
                }

                if(num > 0) sprites.Add(newRoom);
            }

            return sprites;
        }

        public static List<RoomSprite> RoomSpritesTop(XElement xml)
        {
            List<RoomSprite> sprites = new List<RoomSprite>();
            List<XElement> rooms = xml.Elements("Room").ToList();

            foreach (XElement room in rooms)
            {
                int row = int.Parse(room.Attribute("row").Value);
                int col = int.Parse(room.Attribute("col").Value);


                RoomSprite newRoom = RoomSpriteFactory.instance.NewRoomTop(row, col);
                List<XElement> doors = room.Elements("Door").ToList();

                foreach (XElement door in doors)
                {
                    string direction = door.Element("Direction").Value;
                    if (direction.Equals("Left")) RoomSpriteFactory.instance.AddLeftDoorTop(ref newRoom);
                    else if (direction.Equals("Right")) RoomSpriteFactory.instance.AddRightDoorTop(ref newRoom);
                    else if (direction.Equals("Up")) RoomSpriteFactory.instance.AddTopDoorTop(ref newRoom);
                    else if (direction.Equals("Down")) RoomSpriteFactory.instance.AddBottomDoorTop(ref newRoom);

                }

                sprites.Add(newRoom);
            }

            return sprites;
        }
    }
}
