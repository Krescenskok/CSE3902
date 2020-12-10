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
     

        private static List<List<Room>> rooms = new List<List<Room>>();

        private static int size;

        public static void GenerateRooms(List<List<int>> inputRows, int bottomRowNum)
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


        public static void FinishGeneration(CustomXMLWriter document)
        {
            int index = 0;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    Random rand = new Random(); int num = rand.Next(0, size / 2);

                    if(num == 0) document.AddTrapRoom(index++, rooms[i][j], size, rooms[i][j].location);
                    else document.AddRoom(index++,rooms[i][j], size, rooms[i][j].location);
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
                    string type = door.Attribute("Type").Value;
                    if (type.Contains("left")) RoomSpriteFactory.instance.AddLeftDoor(ref newRoom);
                    else if(type.Contains("right")) RoomSpriteFactory.instance.AddRightDoor(ref newRoom);
                    else if (type.Contains("top")) RoomSpriteFactory.instance.AddTopDoor(ref newRoom);
                    else if (type.Contains("bottom")) RoomSpriteFactory.instance.AddBottomDoor(ref newRoom);

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
                    string type = door.Attribute("Type").Value;
                    if (type.Contains("left")) RoomSpriteFactory.instance.AddLeftDoorTop(ref newRoom);
                    else if (type.Contains("right")) RoomSpriteFactory.instance.AddRightDoorTop(ref newRoom);
                    else if (type.Contains("top")) RoomSpriteFactory.instance.AddTopDoorTop(ref newRoom);
                    else if (type.Contains("bottom")) RoomSpriteFactory.instance.AddBottomDoorTop(ref newRoom);

                }

                sprites.Add(newRoom);
            }

            return sprites;
        }
    }
}
