using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sprint5
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying and updating enemies in current room</para>
    /// </summary>
    public class RoomItems
    {

        private static readonly RoomItems instance = new RoomItems();

        private List<IItems> roomItems;
        private Camera cam = Camera.Instance;

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
                string objType = typeTag.Value;

                

                if (objType.Equals("Item"))
                {
                    XElement nameTag = item.Element("ObjectName");
                    XElement locTag = item.Element("Location");
                    XElement aliveTag = item.Element("Alive");


                    string objName = nameTag.Value;
                    string objLoc = locTag.Value;

                    bool alive = aliveTag == null || aliveTag.Value.Equals("true");

                    if (alive)
                    {
                        int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                        int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));

                        Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                        if (objName.Equals("Arrow"))
                        {

                            roomItems.Add(new ArrowObject(ItemsFactory.Instance.CreateArrowSprite("Up"), location, item));
                        }
                        else if (objName.Equals("BlueCandle"))
                        {
                            roomItems.Add(new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), location, item));
                        }
                        else if (objName.Equals("BluePotion"))
                        {
                            roomItems.Add(new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), location, item));
                        }
                        else if (objName.Equals("BlueRing"))
                        {
                            roomItems.Add(new BlueRing(ItemsFactory.Instance.CreateBlueRingSprite(), location, item));
                        }
                        else if (objName.Equals("Bomb"))
                        {
                            roomItems.Add(new Bomb(ItemsFactory.Instance.CreateBombSprite(), location));
                        }
                        else if (objName.Equals("Boomerang"))
                        {
                            roomItems.Add(new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), location, item));
                        }
                        else if (objName.Equals("Bow"))
                        {
                            roomItems.Add(new Bow(ItemsFactory.Instance.CreateBowSprite(), location, item));
                        }
                        else if (objName.Equals("Clock"))
                        {
                            roomItems.Add(new Clock(ItemsFactory.Instance.CreateClockSprite(), location, item));
                        }
                        else if (objName.Equals("Compass"))
                        {
                            roomItems.Add(new Compass(ItemsFactory.Instance.CreateCompassSprite(), location, item));
                        }
                        else if (objName.Equals("Fairy"))
                        {
                            roomItems.Add(new Fairy(ItemsFactory.Instance.CreateFairySprite(), location, item));
                        }
                        else if (objName.Equals("Heart"))
                        {
                            roomItems.Add(new Heart(ItemsFactory.Instance.CreateHeartSprite(), location, item));
                        }
                        else if (objName.Equals("HeartContainer"))
                        {
                            roomItems.Add(new HeartContainer(ItemsFactory.Instance.CreateHeartContainerSprite(), location, item));
                        }
                        else if (objName.Equals("Key"))
                        {
                            roomItems.Add(new Key(ItemsFactory.Instance.CreateKeySprite(), location, item));
                        }
                        else if (objName.Equals("Map"))
                        {
                            roomItems.Add(new Map(ItemsFactory.Instance.CreateMapSprite(), location, item));
                        }
                        else if (objName.Equals("Rupee"))
                        {
                            roomItems.Add(new Rupee(ItemsFactory.Instance.CreateRupeeSprite(), location, item));
                        }
                        else if (objName.Equals("Shield"))
                        {
                            roomItems.Add(new Shield(ItemsFactory.Instance.CreateShieldSprite(), location, item));
                        }
                        else if (objName.Equals("SilverSword"))
                        {
                            roomItems.Add(new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), location, item));
                        }
                        else if (objName.Equals("TriforcePiece"))
                        {
                            roomItems.Add(new TriforcePiece(ItemsFactory.Instance.CreateTriforcePieceSprite(), location, item));
                        }
                        else if (objName.Equals("Wand"))
                        {
                            roomItems.Add(new Wand(ItemsFactory.Instance.CreateWandSprite(), location, item));
                        }
                        else if (objName.Equals("WoodenSword"))
                        {
                            roomItems.Add(new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), location, item));
                        }
                    }
                    

                }
            }

        }

        public void DropItem(string itemName, Vector2 location)
        {
            if (itemName.Equals("Rupee")) roomItems.Add(new Rupee(ItemsFactory.Instance.CreateRupeeSprite(), location));
            else if (itemName.Equals("Heart")) roomItems.Add(new Heart(ItemsFactory.Instance.CreateHeartSprite(), location));
            else if (itemName.Equals("Boomerang")) roomItems.Add(new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), location));
            else if (itemName.Equals("Fairy")) roomItems.Add(new Fairy(ItemsFactory.Instance.CreateFairySprite(), location));
            else if (itemName.Equals("Clock")) roomItems.Add(new Clock(ItemsFactory.Instance.CreateClockSprite(), location));
            else if (itemName.Equals("HeartContainer")) roomItems.Add(new HeartContainer(ItemsFactory.Instance.CreateHeartContainerSprite(), location));
        }

        public void DropRandom(Vector2 location)
        {
            Random rand = new Random();
            int num = rand.Next(0, 5);
            if (num == 0) DropItem("Heart", location);
            else if (num == 1) DropItem("Rupee", location);
            else if (num == 2) DropItem("Fairy", location);
            else if (num == 3) DropItem("Clock", location);
        }
        public void DropHeartContainer(Vector2 location)
        {
            DropItem("HeartContainer", location);
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
            
        }
        


    }
}

    
