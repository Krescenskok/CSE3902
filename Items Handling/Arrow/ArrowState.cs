using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class ArrowState : IItemsState
    {
        private Arrow arrow;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 30;
        private float updatePerSec = 40;
        private float speed;
        private int runTime;
        private const int maxTime = 50;

        public ArrowState(Arrow arrow, Vector2 location, string direction)
        {
            this.arrow = arrow;
            this.direction = direction;
            this.location = location;
            runTime = 0;
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
            runTime++;

            if (runTime >= maxTime)
            {
                Expire();
            }    
        }

        public void Expire()
        {
            
        }

        public void Collected()
        {
           
        }
    }
}
