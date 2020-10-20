using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace Sprint2
{
    public class SilverArrowState : IItemsState
    {
        private SilverArrow arrow;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 30;
        private float updatePerSec = 40;
        private float speed;

        public SilverArrowState(SilverArrow arrow, Vector2 location, string direction)
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
        }


        public void Expire()
        {
            //expires when it travels a certain distance

        }

        public void Collected()
        {
            //player hits it, then player takes damage

        }
    }
}
