using System;
using Microsoft.Xna.Framework;

namespace Sprint3.Link
{
    public class Projectiles
    {

        LinkPlayer link;
        Vector2 itemLocation;
  

        public static Projectiles Instance
        {
            get { return Instance; }
        }

        public Projectiles(LinkPlayer link)
        {
            this.link = link;
            itemLocation = link.currentLocation;

            if (link.state is MoveRight)
            {
                itemLocation.X += 10;
            }
            else if (link.state is MoveLeft)
            {
                itemLocation.X -= 10;
            }
            else if (link.state is MoveUp)
            {
                itemLocation.Y -= 10;
            }
            else if (link.state is MoveDown)
            {
                itemLocation.Y += 10;
            }
        }

        public void ArrowBow()
        {

        }

        public void SwordBeam()
        {
          
        }

    }
}
