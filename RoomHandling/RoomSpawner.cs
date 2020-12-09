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


        public static RoomSpawner Instance { get; } = new RoomSpawner();

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

            RoomDoors.Instance.Reset();
        }


        public void LoadCustomRooms(Game game, string file)
        {
            xml = XElement.Load("../../../XMLLoading/" + file + ".xml");
            roomXMLs = xml.Elements("Room").ToDictionary(p => int.Parse(p.Attribute("id").Value));

            roomSprites = RoomGenerator.RoomSprites(xml);
            roomSpritesTopLayer = RoomGenerator.RoomSpritesTop(xml);
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
            GridGenerator.Instance.GetGrid(12, 7);
            CollisionHandler.Instance.RoomChange();
            HPBarDrawer.RemoveAll();
            LoadRoom(game, roomNumber);
            Sounds.Instance.RoomChange();
        }

        public void Draw(SpriteBatch batch)
        {
            //allRooms.Draw(batch);
            foreach (RoomSprite sprite in roomSprites)
            {
                sprite.Draw(batch);
            }

            RoomDoors.Instance.Draw(batch);
            
            RoomBlocks.Instance.Draw(batch);
            RoomEnemies.Instance.Draw(batch);
            RoomItems.Instance.Draw(batch);
            RoomNPCs.Instance.Draw(batch);

            
        }

        public void DrawTopLayer(SpriteBatch batch)
        {
            //topLayer.Draw(batch);
            foreach(RoomSprite sprite in roomSpritesTopLayer)
            {
                sprite.Draw(batch);
            }
            HPBarDrawer.Draw(batch);
        }

        public void LoadAllRooms(Game game)
        {
            roomSprites = new List<RoomSprite>();
            roomSpritesTopLayer = new List<RoomSprite>();
            roomSpriteSheet = game.Content.Load<Texture2D>("RoomMap2");
            dungeonSheet = roomSpriteSheet;
            dungeonSheetOuter = game.Content.Load<Texture2D>("RoomMapOuter2");
            Point drawSize = new Point(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);

            allRooms = new DungeonSprite(dungeonSheet);
            topLayer = new DungeonSprite(dungeonSheetOuter);

            
        }



    }
}
    
