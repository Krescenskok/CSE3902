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
            xml = XElement.Load("../../../PartialLevelOne.xml").Element("Asset");
            xml.Attribute("Type").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("Type").Value));
        }

        public void Reset()
        {

            xml = XElement.Load("../../../PartialLevelOne.xml").Element("Asset");
            xml.Attribute("Asset").Name.ToString();
            roomXMLs = xml.Elements("Room").ToDictionary(p => Int32.Parse(p.Attribute("Type").Value));
        }

    
        public void LoadRoom(Game game, int roomNumber)
        {
            XElement room = roomXMLs[roomNumber];
            RoomEnemies.Instance.LoadRoom(game, room);
            RoomItems.Instance.LoadRoom(game, room);
            RoomBlocks.Instance.LoadRoom(game, room);       
        }
        public void Update()
        {
            RoomEnemies.Instance.Update();
            RoomItems.Instance.Update();
            RoomBlocks.Instance.Update();
        }

        public void RoomChange(Game game, int roomNumber)
        {
            LoadRoom(game, roomNumber);
        }

        public void Draw(SpriteBatch batch)
        {
            RoomEnemies.Instance.Draw(batch);
            RoomItems.Instance.Draw(batch);
            RoomBlocks.Instance.Draw(batch);
        }



    }
}
    
