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
            xml = XElement.Load("../../../XMLLoading/FinalLevelOne.xml").Element("Asset");
            xml.Attribute("Type").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("id").Value));

        }

        public void Reset()
        {

            xml = XElement.Load("../../../XMLLoading/FinalLevelOne.xml").Element("Asset");
            xml.Attribute("Type").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("id").Value));
        }

    
        public void LoadRoom(Game game, int roomNumber)
        {            
            XElement room = roomXMLs[roomNumber];
            RoomEnemies.Instance.LoadRoom(game, room);
            RoomItems.Instance.LoadRoom(game, room);
            RoomBlocks.Instance.LoadRoom(game, room);
            RoomWalls.Instance.LoadRoom(game, room);
            RoomDoors.Instance.LoadRoom(game, room);
            RoomNPCs.Instance.LoadRoom(game, room);
            
            CurrentRoom = roomNumber;
        }
        public void Update()
        {
            RoomEnemies.Instance.Update();
            RoomItems.Instance.Update();
            RoomBlocks.Instance.Update();
            RoomWalls.Instance.Update();
            RoomDoors.Instance.Update();
            RoomNPCs.Instance.Update();
        }


        public void RoomChange(Game game, int roomNumber)
        {
            GridGenerator.Instance.GetGrid(game, 12, 7);
            CollisionHandler.Instance.RoomChange();
            LoadRoom(game, roomNumber);
            Sounds.Instance.RoomChange(roomNumber);
        }

        public void Draw(SpriteBatch batch)
        {
            allRooms.Draw(batch);
            RoomDoors.Instance.Draw(batch);
            
            RoomBlocks.Instance.Draw(batch);
            RoomEnemies.Instance.Draw(batch);
            RoomItems.Instance.Draw(batch);
            RoomNPCs.Instance.Draw(batch);
        }

        public void DrawTopLayer(SpriteBatch batch)
        {
            topLayer.Draw(batch);
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



    }
}
    
