using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Sprint3.Items;
using Sprint3.Blocks;
using System.Linq.Expressions;


namespace Sprint3
{

    /// <summary>
    /// Author: JT Thrash & Andrew Sanchez
    /// <para>Singleton class for spawning enemies, blocks, and items in each room. Used in main Game class</para>
    /// </summary>
    public class RoomSpawner
    {

        private static readonly RoomSpawner instance = new RoomSpawner();

        int roomNumber;
        Dictionary<int, XElement> roomXMLs;

        List<RoomSprite> roomSprites;
        RoomSprite currentRoomSprite;
        Texture2D roomSpriteSheet;

        XElement xml;




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

           
        }

        public void Reset()
        {

            xml = XElement.Load("../../../FinalLevelOne.xml").Element("Asset");
            xml.Attribute("Asset").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("Type").Value));
        }

    
        public void LoadRoom(Game game, int roomNumber)

        {
            Debug.WriteLine("reading room");
            XElement room = roomXMLs[roomNumber];
            currentRoomSprite = roomSprites[roomNumber];
            RoomEnemies.Instance.LoadRoom(game, room);
            RoomItems.Instance.LoadRoom(game, room);
            RoomBlocks.Instance.LoadRoom(game, room);       
        }
        public void Update()
        {
            RoomEnemies.Instance.Update();
            //RoomItems.Instance.Update();
            //RoomBlocks.Instance.Update();
        }

        public void RoomChange(Game game, int roomNumber)
        {
            Debug.WriteLine("changing room");
            LoadRoom(game, roomNumber);
        }

        public void Draw(SpriteBatch batch)
        {

            currentRoomSprite.Draw(batch, Vector2.Zero, 0, Color.White);
            RoomEnemies.Instance.Draw(batch);
            RoomItems.Instance.Draw(batch);
            RoomBlocks.Instance.Draw(batch);
        }

        public void LoadAllRooms(Game game)
        {
            roomSprites = new List<RoomSprite>();
            roomSpriteSheet = game.Content.Load<Texture2D>("RoomMap");
            Point drawSize = new Point(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);

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
        }

    }
}
    
