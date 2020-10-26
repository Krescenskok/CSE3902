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

namespace Sprint3
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying and updating enemies in current room</para>
    /// </summary>
    public class RoomEnemies
    {

        private static readonly RoomEnemies instance = new RoomEnemies();

        private List<IEnemy> enemies;
        private List<EnemyDeath> deaths;

        private List<TestCollider> testObjects;

   

        public static RoomEnemies Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomEnemies()
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
                XElement aliveTag = item.Element("Alive");

                string objType = typeTag.Value;
                string objName = nameTag.Value;
                string objLoc = locTag.Value;

                bool alive = aliveTag == null || aliveTag.Value.Equals("true");

                if (objType.Equals("Enemy") && alive)
                {
                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));


                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Rope"))
                    {
                        enemies.Add(new Rope(game, location, item));


                    }
                    else if (objName.Equals("Stalfos"))
                    {
                        enemies.Add(new Stalfos(game, location, item));
                    }
                    else if (objName.Equals("Goriya"))
                    {
                        enemies.Add(new Goriya(game, location,item));
                    }
                    else if (objName.Equals("Keese"))
                    {
                        enemies.Add(new Keese(game, location,item));
                    }else if (objName.Equals("Gel"))
                    {
                        enemies.Add(new Gel(game, location, item));
                    }
                    else if (objName.Equals("Dodongo"))
                    {
                        enemies.Add(new Dodongo(game,location,item));

                    }else if (objName.Equals("Trap"))
                    {
                        string dir1 = item.Element("Direction1").Value;
                        string dir2 = item.Element("Direction2").Value;
                        enemies.Add(new BladeTrap(game, location, dir1, dir2));
                    }else if (objName.Equals("WallMaster"))
                    {
                        enemies.Add(new WallMaster(game, location, item));
                    }
                //more if-else for other enemies


                    

                    if (objName.Equals("Blocks"))
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
                        
                    }

                    int areaWidth = GridGenerator.Instance.GetGridWidth();
                    int areaHeight = GridGenerator.Instance.GetGridHeight();

                    if (objName.Equals("Walls"))
                    {
                        Rectangle leftWall = new Rectangle(new Point(-1, 0), new Point(1, areaHeight));
                        Rectangle rightWall = new Rectangle(new Point(areaWidth + 1, 0), new Point(1, areaHeight));
                        Rectangle topWall = new Rectangle(new Point(0, -1), new Point(areaWidth, 1));
                        Rectangle bottomWall = new Rectangle(new Point(0, areaHeight + 1), new Point(areaWidth, 1));

                        testObjects.Add(new TestCollider(leftWall,game));
                        testObjects.Add(new TestCollider(rightWall, game));
                        testObjects.Add(new TestCollider(topWall, game));
                        testObjects.Add(new TestCollider(bottomWall, game));
                    }
                }
            }

        }

           






        public void AddTestCollider(Rectangle rect, Game game)
        {
            testObjects.Add(new TestCollider(rect, game));
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
            foreach (TestCollider col in testObjects)
            {
                col.Draw(batch);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(batch);
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
            CollisionHandler.Instance.RemoveCollider(enemy.GetCollider());
        }

        public void Destroy(IEnemy enemy)
        {
            enemies.Remove(enemy);
            CollisionHandler.Instance.RemoveCollider(enemy.GetCollider());
        }
        

        public void Destroy(EnemyDeath death)
        {
            deaths.Remove(death);
            
        }

        public void StunAllEnemies()
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                enemies[i].State.Stun();
            }
        }


    }
}
    
