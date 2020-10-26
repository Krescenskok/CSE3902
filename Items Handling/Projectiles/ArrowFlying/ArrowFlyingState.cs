using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class ArrowFlyingState : IItemsState
    {
        private Arrow arrow;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 90;
        private float updatePerSec = 40;
        private int runTime = 0;
        private float speed;

        public ArrowFlyingState(Arrow arrow, Vector2 location, string direction)
        {
            this.arrow = arrow;
            this.direction = direction;
            this.location = location;
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
            arrow.UpdateLocation(location);

            //for testing only
            runTime++;
            if (runTime > 100)
            {
                Expire();
            }
        }

        public void Expire()
        {
            //expires only when it hits an enemy or the walls. always leaves an impact when it expires.
            arrow.Impact();
        }

        public void Collected()
        {
           
        }
    }
}
