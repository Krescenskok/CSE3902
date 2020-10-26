using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Sprint3.Blocks;

namespace Sprint3
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying and updating enemies in current room</para>
    /// </summary>
    public class RoomBlocks
    {

        private static readonly RoomBlocks instance = new RoomBlocks();

        private List<LinkBlocks> roomBlocks;

        private List<TestCollider> testObjects;

        public static RoomBlocks Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomBlocks()
        {
            roomBlocks = new List<LinkBlocks>();
            testObjects = new List<TestCollider>();
        }
        public void LoadRoom(Game game, XElement room)
        {

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                XElement nameTag = item.Element("ObjectName");
                XElement locTag = item.Element("Location");
                XElement aliveTag = item.Element("Alive");

                string objType = typeTag.Value;
                string objName = nameTag.Value;
                string objLoc = locTag.Value;

                bool alive = aliveTag == null || aliveTag.Value.Equals("true");

                if (objType.Equals("Block") && alive)
                {
                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));

                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Block")) //Whgat is this code??
                    {

                        Point size = GridGenerator.Instance.GetTileSize();
                        Point small = size;
                        small.X = size.X / 2;
                        small.Y = size.Y / 2;
                        for(int i = 0; i < 5; i++)
                        {
                            for(int j = 0; j < 10; j++)
                            {
                                Point loc = new Point(j * size.X, i* size.Y);

                                if (j % 2 == 0 && i % 2 == 0) testObjects.Add(new TestCollider(loc, size, game,0));                             
                            }
                        }        
                    } else if (objName.Equals("BirdLeft"))
                    {
                        roomBlocks.Add(new BirdLeft(ItemsFactory.Instance.CreateBirdRightSprite(), location));
                    }
                    else if (objName.Equals("BirdRight"))
                    {
                        roomBlocks.Add(new BirdRight(ItemsFactory.Instance.CreateBirdRightSprite(), location));
                    }
                    else if (objName.Equals("BirdRight"))
                    {
                        roomBlocks.Add(new BirdRight(ItemsFactory.Instance.CreateBirdRightSprite(), location));
                    }
                }
            }

        }
        public void Update()
        {
            for (int i = 0; i < roomBlocks.Count; i++)
            {
                roomBlocks[i].Update();
            }
        }
        public void Draw(SpriteBatch batch)
        {
            
            for (int i = 0; i < roomBlocks.Count; i++)
            {
                roomBlocks[i].Draw(batch);
            }

            foreach (TestCollider col in testObjects)
            {
                col.Draw(batch);
            }
        }
        public void Destroy(LinkBlocks block)
        {
            roomBlocks.Remove(block);
            CollisionHandler.Instance.RemoveCollider(enemy.GetCollider());
        }        



    }
}
    
