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
using Sprint3.Items.States;
using Sprint3.Blocks;

namespace Sprint3.RoomHandling
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying Enemies and NPCs. Used in main Game class</para>
    /// </summary>
    public class RoomSpawner
    {

        private static readonly RoomSpawner instance = new RoomSpawner();
        ISprite currentSprite;
        List<IEnemy> enemies;
        List<IItemsState> IItems; //PLACEHOLDER
        List<IBlockState> IBlocks; //PLACEHOLDER
        List<EnemyDeath> deaths;
        List<List<IStatus>> dungeonStatus;
        List<TestCollider> testObjects;
        int roomNumber;
        

        public static RoomSpawner Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomSpawner()
        {
            enemies = new List<IEnemy>();
            List<IItemsState> IItems = new List<IItemsState>();//PLACEHOLDERS
            List<IBlockState> IBlocks = new List<IBlockState>();
            testObjects = new List<TestCollider>();
            deaths = new List<EnemyDeath>();
            dungeonStatus = new List<List<IStatus>>();
        }

    
        public void LoadRoom(Game game, XElement room)
        {
            roomNumber = 1; //PULL FROM FILE

            List<IStatus> roomStatus = new List<IStatus>();
            dungeonStatus.Insert(roomNumber, roomStatus);

            testObjects = new List<TestCollider>();

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                XElement nameTag = item.Element("ObjectName");
                XElement locTag = item.Element("Location");

                string objType = typeTag.Value;
                string objName = nameTag.Value;
                string objLoc = locTag.Value;



                if (objType.Equals("Enemy"))
                {
                    currentSprite = SpriteFactory.Instance.CreateItemsSprite(); //PLACEHOLDER
                    roomStatus.Add(new ItemStatus(currentSprite));
                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));


                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Rope"))
                    {
                        enemies.Add(new Rope(game, location));


                    }
                    else if (objName.Equals("Stalfos"))
                    {
                        enemies.Add(new Stalfos(game, location));
                    }
                    //more if-else for other enemies


                    if (objName.Equals("PlayerTest1"))
                    {
                        testObjects.Add(new TestCollider(location.ToPoint(), new Point(50, game.Window.ClientBounds.Height), game));
                    }

                    if (objName.Equals("PlayerTest2"))
                    {
                        testObjects.Add(new TestCollider(location.ToPoint(), new Point(game.Window.ClientBounds.Width, 50), game));
                    }
                }
                else if (objType.Equals("Item"))
                {
                    currentSprite = SpriteFactory.Instance.CreateItemsSprite(); //PLACEHOLDER
                    roomStatus.Add(new ItemStatus(currentSprite));

                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));


                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Rupee"))
                    {
                        IItems.Add(new Rupee(currentSprite));


                    }
                    else if (objName.Equals("Stalfos"))
                    {
                        enemies.Add(new Stalfos(game, location));
                    }
                    //more if-else for other enemies


                    if (objName.Equals("PlayerTest1"))
                    {
                        testObjects.Add(new TestCollider(location.ToPoint(), new Point(50, game.Window.ClientBounds.Height), game));
                    }

                    if (objName.Equals("PlayerTest2"))
                    {
                        testObjects.Add(new TestCollider(location.ToPoint(), new Point(game.Window.ClientBounds.Width, 50), game));
                    }
                }
                else if (objType.Equals("Block"))
                {
                    currentSprite = SpriteFactory.Instance.CreateBlocksSprite(); //PLACEHOLDER
                    roomStatus.Add(new BlockStatus(currentSprite));

                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));


                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Column"))
                    {
                        IBlocks.Add(new Column(currentSprite));


                    }
                    else if (objName.Equals("Stalfos"))
                    {
                        enemies.Add(new Stalfos(game, location));
                    }
                    //more if-else for other enemies


                    if (objName.Equals("PlayerTest1"))
                    {
                        testObjects.Add(new TestCollider(location.ToPoint(), new Point(50, game.Window.ClientBounds.Height), game));
                    }

                    if (objName.Equals("PlayerTest2"))
                    {
                        testObjects.Add(new TestCollider(location.ToPoint(), new Point(game.Window.ClientBounds.Width, 50), game));
                    }
                }

            }  
        }
        public void Update()
        {
           
            
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update();
            }
            for (int i = 0; i < deaths.Count; i++)
            {
                deaths[i].Update();
            }
            //PLACEHOLDERS
            for (int i = 0; i < IItems.Count; i++)
            {
                IItems[i].Update();
            }
            for (int i = 0; i < IBlocks.Count; i++)
            {
                IBlocks[i].Update();
            }
        }

        public void Draw(SpriteBatch batch)
        {
            
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(batch);
            }
            //PLACEHOLDERS
            for (int i = 0; i < IItems.Count; i++)
            {
                IItems[i].Draw(batch);
            }
            for (int i = 0; i < IBlocks.Count; i++)
            {
                IBlocks[i].Draw(batch);
            }

            foreach (TestCollider col in testObjects)
            {
                col.Draw(batch);
            }

            for(int i = 0; i < deaths.Count; i++)
            {
                deaths[i].Draw(batch);
            }
        }

        public void Destroy(IEnemy enemy, Vector2 location)
        {
            deaths.Add(new EnemyDeath(location));
            enemies.Remove(enemy);
        }

        public void Destroy(EnemyDeath death)
        {
            deaths.Remove(death);
            
        }


    }
}
    
