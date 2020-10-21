using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BoomerangState : IItemsState
    {
        private Boomerang item;
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

        private bool leaving = true;
        private bool returning = false;
        private bool midpoint = false;
        private bool finishedThrow = false;

        public BoomerangState(Boomerang item, Vector2 initPos, string direction)
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
            Vector2 linkLocation = new Vector2(20, 20); //need to be able to gain Link's current location for return logic
            float distanceToLink = (float)Math.Sqrt(Math.Pow((position.X), 2) + Math.Pow((position.Y), 2));

            leaving = thrown && (thrownTime < travelTime) && !comingBack;
            returning = thrown && (thrownTime > 0) && comingBack;
            midpoint = thrown && (thrownTime == travelTime) && !comingBack;
            finishedThrow = thrown && comingBack && distanceToLink == 0;

            if (leaving)
            {
                item.ThrowBoomerang(true);
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
            }
            else if (midpoint)
            {
                comingBack = true;
            }
            else if (returning)
            {
                distanceToLink = (float)Math.Sqrt(Math.Pow((position.X+linkLocation.X), 2) + Math.Pow((position.Y+linkLocation.Y), 2));
                if (distanceToLink != 0)
                {
                    AdjustReturnLocation(linkLocation);
                }
                else
                {
                    returning = false;
                    finishedThrow = true;
                }
            }
            else if (finishedThrow)
            {
                thrown = false;
                comingBack = false;
                item.ThrowBoomerang(false);
                Expire();
            }
            thrownTime++;
            item.UpdateLocation(position);

            if (thrownTime % 5 == 0)
            {
                frame++;
            }
            item.UpdateFrame(frame % 4);
        }


        public void AdjustReturnLocation(Vector2 linkLocation)
        {
            if (position.X < linkLocation.X)
            {
                position.X += speed;
            }
            else
            {
                position.X -= speed;
            }

            if (position.Y < linkLocation.Y)
            {
                position.Y += speed;
            }
            else
            {
                position.Y -= speed;
            }
        }

        public void Expire()
        {
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
            //if enemy hits it, then enemy takes damage

        }
    }
}
