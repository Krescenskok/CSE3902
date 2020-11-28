using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ArrowFlyingState : IItemsState
    {
        private Arrow item;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 90;
        private float updatePerSec = 40;
        private int runTime = 0;
        private float speed;
        private const int maxTime = 250;

        public ArrowFlyingState(Arrow arrow, Vector2 location, string direction)
        {
            this.item = arrow;
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
            item.UpdateLocation(location);


            runTime++;
            if (runTime > maxTime)
            {
                Expire();
            }
        }

        public void Expire()
        {
            CollisionHandler.Instance.RemoveCollider(item.Collider);
            //expires only when it hits an enemy or the walls. always leaves an impact when it expires.
            item.Impact();
        }

        public void Collected()
        {
           
        }
    }
}
