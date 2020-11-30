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

        private const int DISPLACEMENT = 10;


        public ProjectilesFactory()
        {
        }


        static public WandBeam CreateWandBeam( string direction, LinkPlayer link)
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
                itemLocation.X += 6;
            }

            return  new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);
           
        }

        //static public SwordBeam CreateSwordBeam(string direction, LinkPlayer link)
        //{
        //    //Vector2 itemLocation = link.CurrentLocation;

        //    //if (direction.Equals("Down") || link.state is Stationary)
        //    //{
        //    //    itemLocation.X += 14;
        //    //    return new SwordBeam(ItemsFactory.Instance.CreateDownBeamSprite(), itemLocation, direction);
        //    //}
        //    //else if (direction.Equals("Right"))
        //    //{
        //    //    itemLocation.Y += 5;
        //    //    return new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), itemLocation, direction);
        //    //}
        //    //else if (direction.Equals("Left"))
        //    //{
        //    //    itemLocation.Y += 7;
        //    //    return new SwordBeam(ItemsFactory.Instance.CreateLeftBeamSprite(), itemLocation, direction);

        //    //}
        //    //else
        //    //{
        //    //    itemLocation.X += 8;
        //    //    return new SwordBeam(ItemsFactory.Instance.CreateUpBeamSprite(), itemLocation, direction);
        //    //}
        //    //return null;

        //}
    }
}
