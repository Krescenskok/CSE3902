using System;
using Microsoft.Xna.Framework;
using Sprint5.Items;

namespace Sprint5.Link
{
    public class ProjectilesFactory
    {
        private const string UP = "Up";
        private const string DOWN = "Down";
        private const string LEFT = "Left";
        private const string RIGHT = "Right";

        private const int buffer = 50;

        private const int DISPLACEMENT = 10;
        private const int SIX = 6;
        private const int TWELVE = 12;
        private const int FIVE = 5;
        private const int SEVEN = 7;

        public ProjectilesFactory()
        {
        }

        static public Arrow CreateArrowBow(string direction, LinkPlayer link)
        {
            Vector2 itemLocation = link.CurrentLocation;

            itemLocation.Y += DISPLACEMENT;
            return new Arrow(ItemsFactory.Instance.CreateArrowSprite(direction), itemLocation, direction);
        }

        static public WandBeam CreateWandBeam(string direction, LinkPlayer link)
        {
            Vector2 itemLocation = link.CurrentLocation;

            if (direction.Equals(DOWN) || link.state is Stationary)
            {
                itemLocation.Y += DISPLACEMENT;
                itemLocation.X += DISPLACEMENT;
            }
            else if (direction.Equals(LEFT))
            {
                itemLocation.Y += DISPLACEMENT;
                itemLocation.X -= DISPLACEMENT;
            }
            else if (direction.Equals(RIGHT))
            {
                itemLocation.Y += DISPLACEMENT;
                itemLocation.X += DISPLACEMENT;
            }
            else
            {
                itemLocation.Y -= DISPLACEMENT;
                itemLocation.X += SIX;
            }

            return new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);

        }

        static public SwordBeam CreateSwordBeam(string direction, LinkPlayer link)
        {
            Vector2 itemLocation = link.CurrentLocation;

            if (direction.Equals("Down") || link.state is Stationary)
            {
                itemLocation.X += TWELVE;
                return new SwordBeam(ItemsFactory.Instance.CreateDownBeamSprite(), itemLocation, direction);
            }
            else if (direction.Equals("Right"))
            {
                itemLocation.Y += FIVE;
                return new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), itemLocation, direction);
            }
            else if (direction.Equals("Left"))
            {
                itemLocation.Y += SEVEN;
                return new SwordBeam(ItemsFactory.Instance.CreateLeftBeamSprite(), itemLocation, direction);

            }

            itemLocation.X += DISPLACEMENT;
            return new SwordBeam(ItemsFactory.Instance.CreateUpBeamSprite(), itemLocation, direction);

        }

        static public CandleFire CreateCandleFire(string direction, LinkPlayer link)
        {
            Vector2 loc = link.CurrentLocation;


            loc.X += DISPLACEMENT;
            if (direction.Equals(UP))
            {
                loc.Y -= buffer;
            }
            else if (direction.Equals(DOWN))
            {
                loc.Y += buffer;
            }
            else if (direction.Equals(RIGHT))
            {
                loc.X += buffer;
            }
            else
            {
                loc.X -= buffer;
            }

            return new CandleFire(ItemsFactory.Instance.CreateCandleFireSprite(), loc);
        }

        static public Bomb CreateSpawnBomb(string direction, LinkPlayer link)
        {
            LinkInventory.Instance.BombCount--;
            Vector2 loc = link.CurrentLocation;

            if (direction.Equals(LEFT))
                loc.X -= buffer;
            else if (direction.Equals(DOWN) || link.state is Stationary)
                loc.Y += buffer;
            else if (direction.Equals(RIGHT))
                loc.X += buffer;
            else
                loc.Y -= buffer;

            return new Bomb(ItemsFactory.Instance.CreateBombSprite(), loc);
        }
        static public Boomerang CreateBoomerangThrow(string direction, LinkPlayer link)
        {
            Vector2 itemLocation = link.CurrentLocation;
            itemLocation = link.CurrentLocation;
            itemLocation.Y += DISPLACEMENT;
            return new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), itemLocation, direction, link);

        }
    }
}
