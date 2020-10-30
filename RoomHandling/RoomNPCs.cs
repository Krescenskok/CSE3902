using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint4.EnemyAndNPC.Merchant;
using Sprint4.EnemyAndNPC.OldMan;
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
    public class RoomNPCs
    {

        private static readonly RoomNPCs instance = new RoomNPCs();

        private List<INPC> NPCs;
        

       



        public static RoomNPCs Instance
        {
            get
            {
                return instance;
            }
        }

        private RoomNPCs()
        {
            NPCs = new List<INPC>();

        }


        public void LoadRoom(Game game, XElement room)
        {
            NPCs = new List<INPC>();
          

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

                if (objType.Equals("NPC") && alive)
                {
                    int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                    int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));


                    Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                    if (objName.Equals("Flame"))
                    {
                        NPCs.Add(new Flame(location));
                    }
                    else if (objName.Equals("OldMan"))
                    {
                        NPCs.Add(new OldMan(location));
                    }
                    else if (objName.Equals("Merchant"))
                    {
                        NPCs.Add(new Merchant(location));
                    }

                }
            }

        }







        public void Update()
        {


            for (int i = 0; i < NPCs.Count; i++)
            {
                NPCs[i].Update();
            }

        }



        public void Draw(SpriteBatch batch)
        {
            GameTime time = new GameTime();
            for (int i = 0; i < NPCs.Count; i++)
            {
                NPCs[i].Draw(batch);
            }

        }


        public void Destroy(INPC NPC)
        {
            NPCs.Remove(NPC);
            
        }

    }
}

