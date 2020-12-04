using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public class RoomDoors
    {

        private static readonly RoomDoors instance = new RoomDoors();

        private List<Door> doors;
        
        

        private Dictionary<int,DoorSprite> lockedDoorSprites;
        private Dictionary<int, Door> lockedDoors;
        public List<ICollider> Doors => doors.ConvertAll(x => x as ICollider);

        private Point doorSizeMiddle;
        private Point doorSizeSide;


        private List<Point> locations;
        private GridGenerator generator;
        

        private Game game;
        private Camera cam = Camera.Instance;

        private int curRoom;

        public Vector2 LinkStartLocation { get; private set; }

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

            lockedDoorSprites = new Dictionary<int, DoorSprite>();
            lockedDoors = new Dictionary<int, Door>();
            
        }

        private void LoadSpecialDoors()
        {
            lockedDoorSprites.Add(1, DoorSpriteFactory.Instance.CreateDoor(1));
            lockedDoorSprites.Add(6, DoorSpriteFactory.Instance.CreateDoor(6));
            lockedDoorSprites.Add(8, DoorSpriteFactory.Instance.CreateDoor(8));
            lockedDoorSprites.Add(9, DoorSpriteFactory.Instance.CreateDoor(9));
            lockedDoorSprites.Add(13, DoorSpriteFactory.Instance.CreateDoor(13));
            lockedDoorSprites.Add(14, DoorSpriteFactory.Instance.CreateDoor(14));
            lockedDoorSprites.Add(16, DoorSpriteFactory.Instance.CreateDoor(16));

            foreach(KeyValuePair<int,DoorSprite> pair in lockedDoorSprites)
            {
                pair.Value.shouldDraw = false;
            }
            lockedDoorSprites[6].shouldDraw = true;
            
        }

        public void OpenDoor(int doorNum)
        {
            if (lockedDoorSprites.ContainsKey(doorNum)) lockedDoorSprites[doorNum].shouldDraw = true;
            if(lockedDoors.ContainsKey(doorNum)) lockedDoors[doorNum].Open();
        }
        public void ShowDoorSprite(int doorNum)
        {
            if(lockedDoorSprites.ContainsKey(doorNum)) lockedDoorSprites[doorNum].shouldDraw = true;
        }
        public void CloseDoor(int doorNum)
        {
            if (lockedDoorSprites.ContainsKey(doorNum)) lockedDoorSprites[doorNum].shouldDraw = false;
            if(lockedDoors.ContainsKey(doorNum)) lockedDoors[doorNum].Close();
        }


        public void LoadRoom(Game game, XElement room)
        {
            doors = new List<Door>();
      
            this.game = game;
            curRoom = int.Parse(room.Attribute("id").Value);
            generator = GridGenerator.Instance;

            CalculateDoorDrawLocations();
            if (lockedDoorSprites.Count == 0) LoadSpecialDoors();
            lockedDoors.Clear();

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                XElement nameTag = item.Element("ObjectName");
                

                string objType = typeTag.Value;
                string objName = nameTag.Value;
                

                if (objType.Equals("Door"))
                {

                    XElement roomTag = item.Element("RoomNum");
                    int nextRoom = int.Parse(roomTag.Value);

                    XElement doorTypeTag = item.Element("Type");

                    DoorType thisType = DoorTypes.Parse(doorTypeTag.Value);

                    

                    if (objName.Equals("Left"))
                    {
                        doors.Add(new Door(game, locations[0], doorSizeSide, nextRoom, 'L', thisType, item,locations[4], curRoom));
                    }
                    else if (objName.Equals("Right"))
                    {
                        doors.Add(new Door(game, locations[1], doorSizeSide, nextRoom, 'R', thisType, item, locations[5], curRoom));
                        
                        
                    }
                    else if (objName.Equals("Up"))
                    {
                        doors.Add(new Door(game, locations[2], doorSizeMiddle, nextRoom, 'T', thisType, item, locations[6], curRoom));
                    } 
                    else if (objName.Equals("Center"))
                    {
                        doors.Add(new Door(game, locations[9], doorSizeSide, nextRoom, 'C', thisType, item, locations[9], curRoom));
                    }
                    else if (objName.Equals("secret"))
                    {
                        doors.Add(new Door(game, locations[10], doorSizeMiddle, nextRoom, 'S', thisType, item, locations[6], curRoom));
                    }
                    else if (objName.Equals("Down"))
                    {
                        doors.Add(new Door(game, locations[3], doorSizeMiddle, nextRoom, 'B', thisType, item, locations[7], curRoom));
                    }

                    if (!thisType.Equals(DoorType.normal)) lockedDoors[curRoom] = doors[doors.Count - 1];

                }
            }

            if (curRoom == 6 && !RoomEnemies.Instance.allDead && lockedDoors[6].open)
            {
                CloseDoor(6);
            }

        }


        public void Draw(SpriteBatch batch)
        {
          
            foreach(KeyValuePair<int,DoorSprite> pair in lockedDoorSprites)
            {
                pair.Value.Draw(batch);
            }
        }

        public void Reset()
        {
            lockedDoorSprites.Clear();
            LoadSpecialDoors();
        }

        private void CalculateDoorDrawLocations()
        {

            locations.Clear();

            int tileWidth = generator.GetTileSize().X;
            int tileHeight = generator.GetTileSize().Y;

            Point camOffset = cam.Location.ToPoint();

            //inner door points
            locations.Add(new Point(0, tileHeight*5) - camOffset) ;
            locations.Add(new Point(tileWidth * 15, tileHeight * 5) - camOffset);
            locations.Add(new Point(tileWidth * 7,0) - camOffset);
            locations.Add(new Point(tileWidth * 7, tileHeight * 10) - camOffset);


            //outer door points
            locations.Add(new Point(tileWidth * 2, tileHeight * 5) - camOffset);
            locations.Add(new Point(tileWidth * 14, tileHeight * 5) - camOffset);
            locations.Add(new Point(tileWidth * 7, tileHeight*2) - camOffset);
            locations.Add(new Point(tileWidth * 7, tileHeight * 9) - camOffset);
            locations.Add(new Point(tileWidth * 9, tileHeight * 5) - camOffset);

            //stair point
            locations.Add(new Point((int)(tileWidth * 9.5), tileHeight * 5) - camOffset);

            //secret door point
            locations.Add(new Point(tileWidth *2, (int)(tileHeight*-.5f)) - camOffset);


            doorSizeMiddle = new Point(tileWidth*2, 2);
            doorSizeSide = new Point(2, tileHeight);

            //find location to send link to when wallmaster brings him to room 1
            LinkStartLocation = new Rectangle(locations[3],new Point(tileWidth,tileHeight)).Center.ToVector2();
         

        }

      



        public void Update()
        {
            foreach(Door door in doors)
            {
                door.Update();
            }


            if (curRoom == 6 && RoomEnemies.Instance.allDead && lockedDoors.ContainsKey(6) && !lockedDoors[6].open)
            {
                OpenDoor(6);
            }


            
        }




    }
}
