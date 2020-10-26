using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sprint3
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying and updating enemies in current room</para>
    /// </summary>
    public class RoomItems
    {

        private static readonly RoomItems instance = new RoomItems();

        private List<IItems> roomItems;

        private List<TestCollider> testObjects;
        public static RoomItems Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomItems()
        {
            roomItems = new List<IItems>();
            testObjects = new List<TestCollider>();
        }

    
        public void LoadRoom(Game game, XElement room)
        {
            roomItems = new List<IItems>();
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

                if (objType.Equals("Item") && alive)
                {
                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));

                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Arrow"))
                    {
                        
                        roomItems.Add(new ArrowObject(ItemsFactory.Instance.CreateArrowSprite("Up"), location));
                    }
                    else if (objName.Equals("BlueCandle"))
                    {
                        roomItems.Add(new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), location));
                    }
                    else if (objName.Equals("BluePotion"))
                    {
                        roomItems.Add(new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), location));
                    }
                    else if (objName.Equals("BlueRing"))
                    {
                        roomItems.Add(new BlueRing(ItemsFactory.Instance.CreateBlueRingSprite(), location));
                    }else if (objName.Equals("Bomb"))
                    {
                        roomItems.Add(new Bomb(ItemsFactory.Instance.CreateBombSprite(), location));
                    }
                    else if (objName.Equals("Boomerang"))
                    {
                        roomItems.Add(new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), location));
                    }
                    else if (objName.Equals("Bow"))
                    {
                        roomItems.Add(new Bow(ItemsFactory.Instance.CreateBowSprite(), location));
                    }
                    else if (objName.Equals("Clock"))
                    {
                        roomItems.Add(new Clock(ItemsFactory.Instance.CreateClockSprite(), location));
                    }
                    else if (objName.Equals("Compass"))
                    {
                        roomItems.Add(new Compass(ItemsFactory.Instance.CreateCompassSprite(), location));
                    }
                    else if (objName.Equals("Fairy"))
                    {
                        roomItems.Add(new Fairy(ItemsFactory.Instance.CreateFairySprite(), location));
                    }
                    else if (objName.Equals("Heart"))
                    {
                        roomItems.Add(new Heart(ItemsFactory.Instance.CreateHeartSprite(), location));
                    }
                    else if (objName.Equals("HeartContainer"))
                    {
                        roomItems.Add(new HeartContainer(ItemsFactory.Instance.CreateHeartContainerSprite(), location));
                    }
                    else if (objName.Equals("Key"))
                    {
                        roomItems.Add(new Key(ItemsFactory.Instance.CreateKeySprite(), location));
                    }
                    else if (objName.Equals("Map"))
                    {
                        roomItems.Add(new Map(ItemsFactory.Instance.CreateMapSprite(), location));
                    }
                    else if (objName.Equals("Rupee"))
                    {
                        roomItems.Add(new Rupee(ItemsFactory.Instance.CreateRupeeSprite(), location));
                    }
                    else if (objName.Equals("Shield"))
                    {
                        roomItems.Add(new Shield(ItemsFactory.Instance.CreateShieldSprite(), location)) ;
                    }
                    else if (objName.Equals("SilverSword"))
                    {
                        roomItems.Add(new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), location));
                    }
                    else if (objName.Equals("TriforcePiece"))
                    {
                        roomItems.Add(new TriforcePiece(ItemsFactory.Instance.CreateTriforcePieceSprite(), location));
                    }
                    else if (objName.Equals("Wand"))
                    {
                        roomItems.Add(new Wand(ItemsFactory.Instance.CreateWandSprite(), location));
                    }
                    else if (objName.Equals("WoodenSword"))
                    {
                        roomItems.Add(new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), location));
                    }

                }
            }

        }
        public void Update()
        {
            for(int i = 0; i < roomItems.Count; i++)
            {
                //this will do collision checking and remove the items from XML?
                roomItems[i].Update();  
                }
            }
            
        



    public void Draw(SpriteBatch batch)
        {
            
            for (int i = 0; i < roomItems.Count; i++)
            {
                roomItems[i].Draw(batch);
            }

            foreach (TestCollider col in testObjects)
            {
                col.Draw(batch);
            }
        }


        public void Destroy(IItems item)
        {
            roomItems.Remove(item);
            CollisionHandler.Instance.RemoveCollider(roomItems.GetCollider());
        }
        


    }
}

    
