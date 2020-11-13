using Microsoft.Xna.Framework;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class ReturningBoomerangState : IItemsState
    {
        private Boomerang item;
        private Vector2 position;
        private LinkPlayer link;

        private int frame = 0;
        private int thrownTime = 0;
        private const int buffer = 10;

        private const float speedPerSec = 40;
        private const float updatePerSec = 40;
        private float speed;

        public ReturningBoomerangState(Boomerang item, Vector2 initPos, LinkPlayer link)
        {
            this.item = item;
            this.position = initPos;
            this.link = link;
            speed = speedPerSec / updatePerSec;
        }

        public void Update()
        {
            Vector2 linkLocation = link.CurrentLocation;
            int distanceToLink = (int)Math.Sqrt(Math.Pow((position.X+linkLocation.X), 2) + Math.Pow((position.Y+linkLocation.Y), 2));

            AdjustReturnLocation(linkLocation);
            item.UpdateLocation(position);
            thrownTime++;

            //check if returned to link
            if (Math.Abs((int) position.X - (int) link.CurrentLocation.X) < buffer && Math.Abs((int)position.Y - (int)link.CurrentLocation.Y) < buffer)
            {
                item.ReturnedToLink();
            }

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
            CollisionHandler.Instance.RemoveCollider(item.Collider);
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
            //for enemies only
            item.Impact();
        }
    }
}
