using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    public class EnemyFactory
    {


        public static EnemyFactory Instance { get; } = new EnemyFactory();

        private EnemyFactory()
        {

        }


        public IEnemy CreateEnemy(Game1 game, XElement item)
        {

            XElement nameTag = item.Element("ObjectName");
            XElement locTag = item.Element("Location");
            XElement aliveTag = item.Element("Alive");


            string name = nameTag.Value;
            string objLoc = locTag.Value;

            bool alive = aliveTag == null || aliveTag.Value.Equals("true");

            if (alive)
            {
                int row = int.Parse(objLoc.Substring(0, objLoc.IndexOf(" ")));
                int column = int.Parse(objLoc.Substring(objLoc.IndexOf(" ")));
                Vector2 location = GridGenerator.Instance.GetLocation(row, column);

                if (name.Equals("Stalfos")) return new Stalfos(game, location, item);
                else if (name.Equals("Gel")) return new Gel(game, location, item);
                else if (name.Equals("Keese")) return new Keese(game, location, item);
                else if (name.Equals("Goriya")) return new Goriya(game, location, item);
                else if (name.Equals("WallMaster")) return new WallMaster(game, location, item);
                else if (name.Equals("Aquamentus")) return new Aquamentus(game, location, item, game.LinkPlayer);
                else if (name.Equals("Trap"))
                {
                    string dir1 = item.Element("Direction1").Value;
                    string dir2 = item.Element("Direction2").Value;
                    return new BladeTrap(game, location, dir1, dir2);
                }
                else if (name.Equals("Rope")) return new Rope(game, location, item);
                else if (name.Equals("Dodongo")) return new Dodongo(game, location, item);
            }
            

            return null;

        }

    }
}
