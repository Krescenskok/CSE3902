using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class MagicalBoomerangState : IItemsState
    {
        private MagicalBoomerang item;
        private Vector2 initPos;
        private Vector2 position;
        private string direction;
        private bool thrown;
        private bool comingBack;
        private int frame = 0;
        private int thrownTime = 0;
        private const int travelTime = 50;

        private float speedPerSec = 30;
        private float updatePerSec = 40;
        private float speed;

        public MagicalBoomerangState(MagicalBoomerang item, Vector2 initPos, string direction)
        {
            this.item = item;
            this.initPos = initPos;
            this.position = initPos;
            this.direction = direction;
            thrown = true;
            comingBack = false;
            speed = speedPerSec / updatePerSec;
        }

        public void Update()
        {
            bool leaving = thrown && (thrownTime < travelTime) && !comingBack;
            bool returning = thrown && (thrownTime > 0) && comingBack;
            bool midpoint = thrown && (thrownTime == travelTime) && !comingBack;
            bool finishedThrow = thrown && (thrownTime == 0) && comingBack;

            if (leaving)
            {
                if (direction.Equals("Right"))
                {
                    position.X += speed;
                }
                else if (direction.Equals("Left"))
                {
                    position.X -= speed;
                }
                else if (direction.Equals("Up"))
                {
                    position.Y -= speed;
                }
                else if (direction.Equals("Down"))
                {
                    position.Y += speed;
                }
                thrownTime++;
            }
            else if (midpoint)
            {
                comingBack = true;
            }
            else if (returning)
            {
                if (direction.Equals("Right"))
                {
                    position.X -= speed;
                }
                else if (direction.Equals("Left"))
                {
                    position.X += speed;
                }
                else if (direction.Equals("Up"))
                {
                    position.Y += speed;
                }
                else if (direction.Equals("Down"))
                {
                    position.Y -= speed;
                }
            }
            else if (finishedThrow)
            {
                thrown = false;
                comingBack = false;
                Expire();
            }
            item.UpdateLocation(position);

            frame++;
            item.UpdateFrame(frame % 4);
        }

        public void Expire()
        {
            //erases boomerang only - Link still has in inventory
        }

        public void Collected()
        {
            //if enemy hits it, then enemy takes damage

        }
    }
}
