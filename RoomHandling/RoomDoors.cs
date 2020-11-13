using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
{
    public class RoomDoors
    {

        private static readonly RoomDoors instance = new RoomDoors();

        private List<Door> doors;
        public List<ICollider> Doors => doors.ConvertAll(x => x as ICollider);

        private Point sideDoorSize;
        private Point middleDoorSize;
        private Point sideDoorHalfSize;
        private Point middleDoorHalf;

        private List<Point> locations;
        private GridGenerator generator;
        

        private Game game;
        private Camera cam = Camera.Instance;


        public static RoomDoors Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomDoors()
        {
            doors = new List<Door>();

            locations = new List<Point>();
        }


        public void LoadRoom(Game game, XElement room, int roomNumber)
        {
            doors = new List<Door>();
      
            this.game = game;
            generator = GridGenerator.Instance;

            if (locations.Count == 0) CalculateDoorDrawLocations();

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                XElement nameTag = item.Element("ObjectName");
                XElement aliveTag = item.Element("Alive");

                string objType = typeTag.Value;
                string objName = nameTag.Value;

                bool alive = aliveTag == null || aliveTag.Value.Equals("true");




                if (objType.Equals("Door") && alive)
                {
                    Point camOffset = cam.Location.ToPoint();

                    if (objName.Equals("Left"))
                    {
                        doors.Add(new Door(game, locations[0] - camOffset, sideDoorSize, roomNumber, 'L', false, item));
                    }
                    else if (objName.Equals("Right"))
                    {
                        doors.Add(new Door(game, locations[2] - camOffset, sideDoorSize, roomNumber, 'R', false, item));
                    }
                    else if (objName.Equals("Top"))
                    {
                        doors.Add(new Door(game, locations[0] - camOffset, middleDoorSize, roomNumber, 'T', false, item));
                    }
                    else if (objName.Equals("Bottom"))
                    {
                        doors.Add(new Door(game, locations[5] - camOffset, middleDoorSize, roomNumber, 'B', false, item));
                    }


                } else if (objType.Equals("Door") && !alive)
                {
                    Point camOffset = cam.Location.ToPoint();

                    if (objName.Equals("Left"))
                    {
                        doors.Add(new Door(game, locations[0] - camOffset, sideDoorSize, roomNumber, 'L', true, item));
                    }
                    else if (objName.Equals("Right"))
                    {
                        doors.Add(new Door(game, locations[2] - camOffset, sideDoorSize, roomNumber, 'R', true, item));
                    }
                    else if (objName.Equals("Top"))
                    {
                        doors.Add(new Door(game, locations[0] - camOffset, middleDoorSize, roomNumber, 'T', true, item));
                    }
                    else if (objName.Equals("Bottom"))
                    {
                        doors.Add(new Door(game, locations[5] - camOffset, middleDoorSize, roomNumber, 'B', true, item));
                    }

                }
            }

        }






        private void CalculateDoorDrawLocations()
        {
            int tileWidth = generator.GetTileSize().X;
            int tileHeight = generator.GetTileSize().Y;
            locations.Add(new Point(0, 0));
            locations.Add(new Point(tileWidth * 9, 0));
            locations.Add(new Point(tileWidth * 14, 0));
            locations.Add(new Point(tileWidth * 14, tileHeight * 6));
            locations.Add(new Point(tileWidth * 9, tileHeight * 9));
            locations.Add(new Point(0, tileHeight * 9));
            locations.Add(new Point(0, tileHeight * 6));


            sideDoorSize = new Point(generator.Offset.X, game.Window.ClientBounds.Height);
            middleDoorSize = new Point(game.Window.ClientBounds.Width, generator.Offset.Y);

            sideDoorHalfSize = new Point(generator.Offset.X, tileHeight * 5);
            middleDoorHalf = new Point(tileWidth * 7, generator.Offset.Y);
        }



        public void Destroy(Door door)
        {
            
            CollisionHandler.Instance.RemoveCollider(door.collider);
            doors.Remove(door);

        }



        public void Update()
        {
          

        }




    }
}
