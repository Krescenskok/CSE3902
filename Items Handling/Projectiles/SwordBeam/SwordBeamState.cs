using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class SwordBeamState : IItemsState
    {
        private SwordBeam item;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 90;
        private float updatePerSec = 40;
        private float speed;
        private int runTime;
        private int frame;
        private const int maxTime = 50;

        public SwordBeamState(SwordBeam item, Vector2 location, string direction)
        {
            this.item = item;
            this.direction = direction;
            this.location = location;
            runTime = 0;
            frame = 0;
            speed = speedPerSec / updatePerSec;
        }

        public void Update()
        {
            if (direction.Equals("Right"))
            {
                location.X += speed;
            }
            else if (direction.Equals("Left"))
            {
                location.X -= speed;
            }
            else if (direction.Equals("Up"))
            {
                location.Y -= speed;
            }
            else if (direction.Equals("Down"))
            {
                location.Y += speed;
            }
            item.UpdateLocation(location);
            runTime++;

            if (runTime % 5 == 0)
            {
                frame++;
                item.UpdateFrame(frame % 2);
            }

            //for testing only
            if (runTime > 100)
            {
                item.Expire();
            }

        }


        public void Expire()
        {
            item.SwordImpact();
        }

        public void Collected()
        {
            //enemy gets damaged
            item.SwordImpact();
        }
    }
}