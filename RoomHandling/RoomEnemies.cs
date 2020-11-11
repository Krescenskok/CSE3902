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

namespace Sprint4
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

        private Game game;
        private Camera cam;
   

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
            cam = Camera.Instance;
        }

    
        public void LoadRoom(Game game, XElement room)
        {
            enemies = new List<IEnemy>();
            testObjects = new List<TestCollider>();
            this.game = game;

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                string objType = typeTag.Value;

                XElement nameTag = item.Element("ObjectName");
                XElement locTag = item.Element("Location");
                XElement aliveTag = item.Element("Alive");

                
                string objName = nameTag.Value;
                string objLoc = locTag.Value;

                bool alive = aliveTag == null || aliveTag.Value.Equals("true");

                if (objType.Equals("Enemy") && alive)
                {
                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));

                    
                    Vector2 location = GridGenerator.Instance.GetLocation(row, column) - cam.Location;

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
                   


                   

                  
                }
            }

        }

           






        public void AddTestCollider(Rectangle rect)
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
            CollisionHandler.Instance.RemoveCollider(enemy.Colliders);
        }

        public void Destroy(IEnemy enemy)
        {
            enemies.Remove(enemy);
            CollisionHandler.Instance.RemoveCollider(enemy.Colliders);
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
    
