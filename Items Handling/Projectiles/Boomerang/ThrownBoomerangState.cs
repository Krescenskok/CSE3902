using Microsoft.Xna.Framework;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ThrownBoomerangState : IItemsState
    {
        private Boomerang item;
        private Vector2 initPos;
        private Vector2 position;
        private string direction;
        private LinkPlayer link;

        private int frame = 0;
        private int thrownTime = 0;
        private const int travelTime = 100;

        private const float speedPerSec = 70;
        private const float updatePerSec = 40;
        private float speed;

        private bool leaving = true;

        public ThrownBoomerangState(Boomerang item, Vector2 initPos, string direction, LinkPlayer link)
        {
            this.item = item;
            this.initPos = initPos;
            this.position = initPos;
            this.direction = direction;
            this.link = link;
            speed = speedPerSec / updatePerSec;
        }

        public void Update()
        {
            if (leaving)
            {
                item.ThrowBoomerang(true, false);
                AdjustPosition();
            }
            else
            {
                item.ThrowBoomerang(true, true);
                item.Returning();
            }
            item.UpdateLocation(position);

            thrownTime++;
            if (thrownTime > travelTime)
            {
                leaving = false;
            }

            if (thrownTime % 5 == 0)
            {
                frame++;
            }
            item.UpdateFrame(frame % 4);

        }

        public void AdjustPosition()
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
        }

        public void Expire()
        {
            item.ThrowBoomerang(true, true);
            item.Returning();
        }

        public void Collected()
        {
            //if enemy hits it, then enemy takes damage
            item.Impact();
        }
    }
}
