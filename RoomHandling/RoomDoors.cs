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
        private List<Door> doors;
        
        

        private Dictionary<string,DoorSprite> lockedDoorSprites;
        private Dictionary<string, Door> lockedDoors;
        private List<string> trap_doors;

        string room6Closed = "3,1,closedright", room6Open = "3,1,openright";
        public List<ICollider> Doors => doors.ConvertAll(x => x as ICollider);

        private Point doorSizeMiddle;
        private Point doorSizeSide;


        private List<Point> locations;
        private GridGenerator generator;
        
        private Camera cam = Camera.Instance;

        private int curRoom;

        public Vector2 LinkStartLocation { get; private set; }

        public static RoomDoors Instance { get; } = new RoomDoors();

        private RoomDoors()
        {
            doors = new List<Door>();

            locations = new List<Point>();

            lockedDoorSprites = new Dictionary<string, DoorSprite>();
            lockedDoors = new Dictionary<string, Door>();
            trap_doors = new List<string>();
        }

        private void FillLockedDoorSprites(XElement file)
        {
            List<XElement> lockedSprites = file.Elements("LockedDoor").ToList();

            foreach(XElement item in lockedSprites)
            {
                bool drawn = item.Attribute("Drawn").Value.Equals("true");
                bool trapDoor = item.Attribute("Trap").Value.Equals("true");

                int row = int.Parse(item.Attribute("Row").Value), col = int.Parse(item.Attribute("Col").Value);
                string type = item.Attribute("Type").Value;
                string key = row + "," + col + "," + type;
                lockedDoorSprites.Add(key, DoorSpriteFactory.Instance.CreateDoor(row,col,type));
                lockedDoorSprites[key].shouldDraw = drawn;

                if (trapDoor) trap_doors.Add(key);
            }
            
        }

        public void OpenDoor(string key)
        {
            if (lockedDoors.ContainsKey(key) && lockedDoorSprites.ContainsKey(key))
            {
                Door doorBeingOpened = lockedDoors[key]; DoorSprite sprite = lockedDoorSprites[key];
                string newKey = key.Substring(0, key.IndexOf("closed")) + "open" + key.Substring(key.IndexOf("closed") + 5);
                lockedDoors.Remove(key); lockedDoors.Add(newKey, doorBeingOpened);
                lockedDoorSprites.Remove(key); lockedDoorSprites.Add(newKey, sprite);
                
                doorBeingOpened.Open();

                if (trap_doors.Contains(key)) trap_doors[trap_doors.IndexOf(key)] = newKey;
            }
        }
        public void CloseDoor(string key)
        {
            if (lockedDoors.ContainsKey(key) && lockedDoorSprites.ContainsKey(key))
            {
                Door doorBeingClosed = lockedDoors[key]; DoorSprite sprite = lockedDoorSprites[key];
                string newKey = key.Substring(0,key.IndexOf("open")) + "closed" + key.Substring(key.IndexOf("open") + 4);
                lockedDoors.Remove(key); lockedDoors.Add(newKey, doorBeingClosed);
                lockedDoorSprites.Remove(key); lockedDoorSprites.Add(newKey, sprite);
                doorBeingClosed.Close();

                if (trap_doors.Contains(key)) trap_doors[trap_doors.IndexOf(key)] = newKey;
            }
        }


        public void LoadRoom(Game game, XElement room)
        {
            doors = new List<Door>();
      
            curRoom = int.Parse(room.Attribute("id").Value);

            int row = int.Parse(room.Attribute("row").Value), col = int.Parse(room.Attribute("col").Value);

            generator = GridGenerator.Instance;

            CalculateDoorDrawLocations();
            if (lockedDoorSprites.Count == 0) FillLockedDoorSprites(room.Parent);
            lockedDoors.Clear();

            List<XElement> doorItems = room.Elements("Door").ToList();
            foreach (XElement item in doorItems)
            {

                int nextRoom = int.Parse(item.Attribute("Room").Value);

                string type = item.Attribute("Type").Value;
                DoorType thisType = DoorTypes.Parse(type);

                string key = (row + "," + col + "," + type);

                DoorSprite sprite = lockedDoorSprites.ContainsKey(key) ? lockedDoorSprites[key] : null;

                if (type.Contains("left"))
                {
                    doors.Add(new Door(locations[0], doorSizeSide, nextRoom, thisType, item,locations[4], key, sprite));
                }
                else if (type.Contains("right"))
                {
                    doors.Add(new Door(locations[1], doorSizeSide, nextRoom, thisType, item, locations[5], key, sprite));
                        
                        
                }
                else if (type.Contains("top"))
                {
                    doors.Add(new Door(locations[2], doorSizeMiddle, nextRoom, thisType, item, locations[6], key, sprite));
                } 
                else if (type.Contains("center"))
                {
                    doors.Add(new Door(locations[9], doorSizeSide, nextRoom, thisType, item, locations[9], key, sprite));
                }
                else if (type.Contains("secret"))
                {
                    doors.Add(new Door(locations[10], doorSizeMiddle, nextRoom, thisType, item, locations[6], key, sprite));
                }
                else if (type.Contains("bottom"))
                {
                    doors.Add(new Door(locations[3], doorSizeMiddle, nextRoom, thisType, item, locations[7], key, sprite));
                }

                if (!thisType.Equals(DoorType.normal)) lockedDoors[key] = doors[doors.Count - 1];


                if (trap_doors.Contains(key)) 
                    CloseDoor(key);
            }

            

        }


        public void Draw(SpriteBatch batch)
        {
            foreach(KeyValuePair<string,DoorSprite> pair in lockedDoorSprites)
            {
                pair.Value.Draw(batch);
            }
        }

        public void Reset()
        {
            lockedDoorSprites.Clear();
            //LoadSpecialDoors();
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

            bool enemiesGone = RoomEnemies.Instance.allDead;
            foreach(string key in trap_doors.ToList())
            {
                if (DoorLocked(key) && enemiesGone) OpenDoor(key);
            }
 
        }


        private bool DoorLocked(string key) { return lockedDoors.ContainsKey(key) && !lockedDoors[key].open; }

    }
}
