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
using Sprint5.Blocks;
using Sprint5.Link;

namespace Sprint5
{

    /// <summary>
    /// Author: JT Thrash
    /// <para>Singleton class for displaying and updating enemies in current room</para>
    /// </summary>
    public class RoomBlocks
    {

        private static readonly RoomBlocks instance = new RoomBlocks();

        private List<IBlock> roomBlocks;

        private Camera cam = Camera.Instance;

        public Boolean movedRight;
        public Boolean movedUp; 

        public static RoomBlocks Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomBlocks()
        {
            roomBlocks = new List<IBlock>();

            movedRight = false;
            movedUp = false;
           
        }
        public void LoadRoom(Game game, XElement room)
        {
            roomBlocks = new List<IBlock>();

            List<XElement> items = room.Elements("Item").ToList();
            foreach (XElement item in items)
            {
                XElement typeTag = item.Element("ObjectType");
                string objType = typeTag.Value;

                

                if (objType.Equals("Block"))
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

                        int width = GridGenerator.Instance.GetTileSize().X;
                        int height = GridGenerator.Instance.GetTileSize().Y;

                        if (objName.Equals("BirdLeft"))
                        {
                            roomBlocks.Add(new BirdLeft(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }
                        else if (objName.Equals("BirdRight"))
                        {
                            roomBlocks.Add(new BirdRight(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }
                        else if (objName.Equals("Water"))
                        {
                            roomBlocks.Add(new WaterTile(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }
                        else if (objName.Equals("Stairs"))
                        {
                            roomBlocks.Add(new Stairs(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }
                        else if (objName.Equals("Column"))
                        {
                            roomBlocks.Add(new Column(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }
                        else if (objName.Equals("Brick"))
                        {
                            location = GridGenerator.Instance.GetOutsideLocation(row, column);
                            
                            roomBlocks.Add(new Bricks(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }else if (objName.Equals("BrickLong"))
                        {
                            location = GridGenerator.Instance.GetOutsideLocation(row, column);
                            string orientation = item.Element("Orientation").Value;
                            int length = int.Parse(item.Element("Length").Value);
                            roomBlocks.Add(new Bricks(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location,orientation,length));
                        }
                        else if (objName.Equals("BlackTile"))
                        {
                            roomBlocks.Add(new BlackTile(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location));
                        }
                        else if (objName.Equals("MoveableColumnRight"))
                        {
                            roomBlocks.Add(new MoveableRight(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location, movedRight));
                        }
                        else if (objName.Equals("MoveableColumnUp"))
                        {
                            roomBlocks.Add(new MoveableUp(SpriteFactory.Instance.CreateBlocksSprite() as BlocksSprite, location, movedUp));
                        }else if (objName.Equals("Plain"))
                        {
                            new BlockCollider(location);
                        }
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

  
        }



    }
}
    
