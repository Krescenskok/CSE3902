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
        private Dictionary<int,DoorSprite> doorWaySprites;
        public List<ICollider> Doors => doors.ConvertAll(x => x as ICollider);

        private Point doorSizeMiddle;
        private Point doorSizeSide;
        private Point middleDoorSize;
        private Point sideDoorHalfSize;
        private Point middleDoorHalf;

        private List<Point> locations;
        private GridGenerator generator;
        

        private Game game;
        private Camera cam = Camera.Instance;

        private int curRoom;

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

            doorWaySprites = new Dictionary<int, DoorSprite>();
            
        }

        private void LoadSpecialDoors()
        {
            doorWaySprites.Add(1, DoorSpriteFactory.Instance.CreateDoor(1));
            doorWaySprites.Add(6, DoorSpriteFactory.Instance.CreateDoor(6));
            doorWaySprites.Add(8, DoorSpriteFactory.Instance.CreateDoor(8));
            doorWaySprites.Add(9, DoorSpriteFactory.Instance.CreateDoor(9));
            doorWaySprites.Add(13, DoorSpriteFactory.Instance.CreateDoor(13));
            doorWaySprites.Add(14, DoorSpriteFactory.Instance.CreateDoor(14));
            doorWaySprites.Add(16, DoorSpriteFactory.Instance.CreateDoor(16));

            foreach(KeyValuePair<int,DoorSprite> pair in doorWaySprites)
            {
                pair.Value.shouldDraw = false;
            }
            doorWaySprites[6].shouldDraw = true;
            
        }

        public void OpenDoor(int doorNum)
        {
            doorWaySprites[doorNum].shouldDraw = true;
        }
        public void CloseDoor(int doorNum)
        {
            doorWaySprites[doorNum].shouldDraw = false;
        }


        public void LoadRoom(Game game, XElement room)
        {
            doors = new List<Door>();
      
            this.game = game;
            curRoom = int.Parse(room.Attribute("id").Value);
            generator = GridGenerator.Instance;

            if (locations.Count == 0) CalculateDoorDrawLocations();
            if (doorWaySprites.Count == 0) LoadSpecialDoors();

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                XElement nameTag = item.Element("ObjectName");
                XElement lockTag = item.Element("Locked");
                

                string objType = typeTag.Value;
                string objName = nameTag.Value;
                

                bool unlocked = lockTag == null || lockTag.Value.Equals("false");




                if (objType.Equals("Door") && unlocked)
                {

                    XElement roomTag = item.Element("RoomNum");
                    int roomNumber = int.Parse(roomTag.Value);

                    Point camOffset = cam.Location.ToPoint();

                    if (objName.Equals("Left"))
                    {
                        doors.Add(new Door(game, locations[0] - camOffset, doorSizeSide, roomNumber, 'L', false, item,locations[4] - camOffset, curRoom));
                    }
                    else if (objName.Equals("Right"))
                    {
                        doors.Add(new Door(game, locations[1] - camOffset, doorSizeSide, roomNumber, 'R', false, item, locations[5] - camOffset, curRoom));
                    }
                    else if (objName.Equals("Up"))
                    {
                        doors.Add(new Door(game, locations[2] - camOffset, doorSizeMiddle, roomNumber, 'T', false, item, locations[6] - camOffset, curRoom));
                    }
                    else if (objName.Equals("Down"))
                    {
                        doors.Add(new Door(game, locations[3] - camOffset, doorSizeMiddle, roomNumber, 'B', false, item, locations[7] - camOffset, curRoom));
                    }
                    

                } else if (objType.Equals("Door") && !unlocked)
                {
                    XElement roomTag = item.Element("RoomNum");
                    int nextRoom = int.Parse(roomTag.Value);
                    

                    Point camOffset = cam.Location.ToPoint();

                    if (objName.Equals("Left"))
                    {
                        doors.Add(new Door(game, locations[0] - camOffset, doorSizeMiddle, nextRoom, 'L', true, item, locations[4] - camOffset,curRoom));
                    }
                    else if (objName.Equals("Right"))
                    {
                        doors.Add(new Door(game, locations[1] - camOffset, doorSizeMiddle, nextRoom, 'R', true, item, locations[5] - camOffset, curRoom));
                    }
                    else if (objName.Equals("Up"))
                    {
                        doors.Add(new Door(game, locations[2] - camOffset, doorSizeMiddle, nextRoom, 'T', true, item, locations[6] - camOffset, curRoom));
                    }
                    else if (objName.Equals("Down"))
                    {
                        doors.Add(new Door(game, locations[3] - camOffset, doorSizeMiddle, nextRoom, 'B', true, item, locations[7] - camOffset, curRoom));
                    }

                }
            }

        }


        public void Draw(SpriteBatch batch)
        {
            foreach(Door door in doors)
            {
                door.Draw(batch);
            }
            foreach(KeyValuePair<int,DoorSprite> pair in doorWaySprites)
            {
                pair.Value.Draw(batch);
            }
        }



        private void CalculateDoorDrawLocations()
        {
            int tileWidth = generator.GetTileSize().X;
            int tileHeight = generator.GetTileSize().Y;

            //unlocked door points
            locations.Add(new Point(0, tileHeight*5));
            locations.Add(new Point(tileWidth * 15, tileHeight * 5));
            locations.Add(new Point(tileWidth * 7,0));
            locations.Add(new Point(tileWidth * 7, tileHeight * 10));

            //locked door points
            locations.Add(new Point(tileWidth * 2, tileHeight * 5));
            locations.Add(new Point(tileWidth * 14, tileHeight * 5));
            locations.Add(new Point(tileWidth * 7, tileHeight*2));
            locations.Add(new Point(tileWidth * 7, tileHeight * 9));


            doorSizeMiddle = new Point(tileWidth*2, 1);
            doorSizeSide = new Point(1, tileHeight);

            
        }



        public void Destroy(Door door)
        {
            
            CollisionHandler.Instance.RemoveCollider(door.innerCollider);
            doors.Remove(door);

        }



        public void Update()
        {
          foreach(Door door in doors)
            {
                door.Update();
            }


          if(curRoom == 6 && RoomEnemies.Instance.EnemyCount == 0 && doorWaySprites[6].shouldDraw == false)
            {
                foreach (Door door in doors)
                {
                    door.Unlock();
                }
            }
        }




    }
}
