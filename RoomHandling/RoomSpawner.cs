using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sprint5.Items;
using Sprint5.Blocks;
using System.Linq.Expressions;
using System.Collections;


namespace Sprint5
{

    /// <summary>
    /// Author: JT Thrash & Andrew Sanchez
    /// <para>Singleton class for spawning enemies, blocks, and items in each room. Used in main Game class</para>
    /// </summary>
    public class RoomSpawner
    {

        private static readonly RoomSpawner instance = new RoomSpawner();

        
        Dictionary<int, XElement> roomXMLs;

        List<RoomSprite> roomSprites;
        List<RoomSprite> roomSpritesTopLayer;
        RoomSprite currentRoomSprite;
        RoomSprite currentRoomSpriteTopLayer;
        Texture2D roomSpriteSheet;

        DungeonSprite allRooms;
        DungeonSprite topLayer;
        Texture2D dungeonSheet;
        Texture2D dungeonSheetOuter;
        static Hashtable roomConnections;

        XElement xml;

        int currentRoom = 1;
        public int CurrentRoom
        {
            get { return currentRoom; }
            set { currentRoom = value; }
        }


        public static RoomSpawner Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomSpawner()
        {
            xml = XElement.Load("../../../FinalLevelOne.xml").Element("Asset");
            xml.Attribute("Type").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("id").Value));
            InitRoomConnections();

           
        }

        public void Reset()
        {

            xml = XElement.Load("../../../FinalLevelOne.xml").Element("Asset");
            xml.Attribute("Type").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("id").Value));
        }

    
        public void LoadRoom(Game game, int roomNumber)
        {            
            XElement room = roomXMLs[roomNumber];
            //currentRoomSprite = roomSprites[roomNumber - 1];
            //currentRoomSpriteTopLayer = roomSpritesTopLayer[roomNumber - 1];
            RoomEnemies.Instance.LoadRoom(game, room);
            RoomItems.Instance.LoadRoom(game, room);
            RoomBlocks.Instance.LoadRoom(game, room);
            RoomWalls.Instance.LoadRoom(game, room);
            RoomDoors.Instance.LoadRoom(game, room);
            
            CurrentRoom = roomNumber;
        }
        public void Update()
        {
            RoomEnemies.Instance.Update();
            RoomItems.Instance.Update();
            RoomBlocks.Instance.Update();
            RoomWalls.Instance.Update();
            RoomDoors.Instance.Update();
        }

        public void RoomChange(Game game, int roomNumber, char heading)
        {
            CurrentRoom = roomNumber;
            Hashtable numConnect = (Hashtable)roomConnections[roomNumber];
            int destinationRoom = (int)numConnect[heading];
            CollisionHandler.Instance.RoomChange();
            
        }

        public void RoomChange(Game game, int roomNumber)
        {
            GridGenerator.Instance.GetGrid(game, 12, 7);
            CollisionHandler.Instance.RoomChange();
            LoadRoom(game, roomNumber);
        }

        public void Draw(SpriteBatch batch)
        {
            allRooms.Draw(batch);
            RoomDoors.Instance.Draw(batch);
            //currentRoomSprite.Draw(batch, Vector2.Zero, 0, Color.White);
            RoomBlocks.Instance.Draw(batch);
            RoomEnemies.Instance.Draw(batch);
            RoomItems.Instance.Draw(batch);
        }

        public void DrawTopLayer(SpriteBatch batch)
        {
            topLayer.Draw(batch);
            //currentRoomSpriteTopLayer.Draw(batch, Vector2.Zero, 0, Color.White);
        }

        public void LoadAllRooms(Game game)
        {
            roomSprites = new List<RoomSprite>();
            roomSpritesTopLayer = new List<RoomSprite>();
            roomSpriteSheet = game.Content.Load<Texture2D>("RoomMapFixed");
            dungeonSheet = roomSpriteSheet;
            dungeonSheetOuter = game.Content.Load<Texture2D>("RoomMapOuter");
            Point drawSize = new Point(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);

            allRooms = new DungeonSprite(dungeonSheet);
            topLayer = new DungeonSprite(dungeonSheetOuter);

            roomSprites.Add(new RoomSprite(roomSpriteSheet, 5, 2, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 5, 1, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 5, 3, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 4, 2, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 3, 2, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 3, 1, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 3, 3, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 2, 2, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 2, 1, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 2, 0, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 2, 3, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 2, 4, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 1, 2, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 1, 4, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 1, 5, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 0, 2, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 0, 1, drawSize));
            roomSprites.Add(new RoomSprite(roomSpriteSheet, 1, 1, drawSize));

            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 5, 2, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 5, 1, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 5, 3, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 4, 2, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 3, 2, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 3, 1, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 3, 3, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 2, 2, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 2, 1, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 2, 0, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 2, 3, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 2, 4, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 1, 2, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 1, 4, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 1, 5, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 0, 2, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 0, 1, drawSize));
            roomSpritesTopLayer.Add(new RoomSprite(dungeonSheetOuter, 1, 1, drawSize));
        }

        public static void InitRoomConnections()
        {
             roomConnections = new Hashtable();

            Hashtable roomOne = new Hashtable();

            roomOne.Add('E', 3);
            roomOne.Add('W', 2);
            roomOne.Add('N', 4);

            roomConnections.Add(1, roomOne);

            Hashtable roomTwo = new Hashtable();

            roomTwo.Add('E', 1);

            roomConnections.Add(2, roomTwo);

            Hashtable roomThree = new Hashtable();

            roomThree.Add('W', 1);

            roomConnections.Add(3, roomThree);

            Hashtable roomFour = new Hashtable();

            roomFour.Add('N', 5);
            roomFour.Add('S', 1);

            roomConnections.Add(4, roomFour);

            Hashtable roomFive = new Hashtable();

            roomFive.Add('E', 7);
            roomFive.Add('W', 6);
            roomFive.Add('N', 8);
            roomFive.Add('S', 4);

            roomConnections.Add(5, roomFive);

            Hashtable roomSix = new Hashtable();

            roomSix.Add('E', 5);
            roomSix.Add('N', 9);

            roomConnections.Add(6, roomSix);

            Hashtable roomSeven = new Hashtable();

            roomSeven.Add('W', 5);
            roomSeven.Add('N', 11);

            roomConnections.Add(7, roomSeven);

            Hashtable roomEight = new Hashtable();

            roomEight.Add('E', 11);
            roomEight.Add('W', 9);
            roomEight.Add('N', 13);
            roomEight.Add('S', 5);

            roomConnections.Add(8, roomEight);

            Hashtable roomNine = new Hashtable();

            roomNine.Add('E', 8);
            roomNine.Add('W', 10);
            roomNine.Add('S', 6);

            roomConnections.Add(9, roomNine);

            Hashtable roomTen = new Hashtable();

            roomTen.Add('E', 9);

            roomConnections.Add(10, roomTen);

            Hashtable roomEleven = new Hashtable();

            roomEleven.Add('E', 12);
            roomEleven.Add('W', 8);
            roomEleven.Add('S', 7);

            roomConnections.Add(11, roomEleven);

            Hashtable roomTwelve = new Hashtable();

            roomTwelve.Add('W', 11);
            roomTwelve.Add('N', 14);

            roomConnections.Add(12, roomTwelve);

            Hashtable roomThirteen = new Hashtable();

            roomThirteen.Add('N', 16);
            roomThirteen.Add('S', 8);

            roomConnections.Add(13, roomThirteen);

            Hashtable roomFourteen = new Hashtable();

            roomFourteen.Add('E', 15);
            roomFourteen.Add('S', 12);

            roomConnections.Add(14, roomFourteen);

            Hashtable roomFifteen = new Hashtable();

            roomFifteen.Add('W', 14);

            roomConnections.Add(15, roomFifteen);

            Hashtable roomSixteen = new Hashtable();

            roomSixteen.Add('S', 13);
            roomSixteen.Add('W', 17);

            roomConnections.Add(16, roomSixteen);

            Hashtable roomSeventeen = new Hashtable();

            roomSixteen.Add('E', 16);
            roomSixteen.Add('?', 18); //PLACEHOLDER

            roomConnections.Add(17, roomSeventeen);

            Hashtable roomEighteen = new Hashtable();

            roomEighteen.Add('?', 17); //PLACEHOLDER

            roomConnections.Add(18, roomEighteen);

        }

    }
}
    
