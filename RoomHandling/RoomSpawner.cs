using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sprint3
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying Enemies and NPCs. Used in main Game class</para>
    /// </summary>
    public class RoomSpawner
    {

        private static readonly RoomSpawner instance = new RoomSpawner();

        List<IEnemy> enemies;
        List<ItemSprite> items; //PLACEHOLDER
        List<EnemyDeath> deaths;
        List<List<IStatus>> roomStatus = new List<List<IStatus>>
        List<TestCollider> testObjects;
        

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
            testObjects = new List<TestCollider>();
            deaths = new List<EnemyDeath>();
        }

    
        public void LoadRoom(Game game, XElement room)
        {


            enemies = new List<IEnemy>();

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
                    int row = int.Parse( objLoc.Substring(0, objLoc.IndexOf(" ")));
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
                } else if (objType.Equals("Item"))
                {
                    itemSprite = SpriteFactory.Instance.CreateItemsSprite();
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
        }

        public void Draw(SpriteBatch batch)
        {
            
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(batch);
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
    
