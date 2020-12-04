using Microsoft.Xna.Framework;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ReturningBoomerangState : IItemsState
    {
        private Boomerang item;
        private Vector2 position;
        private LinkPlayer link;

        private int frame = 0;
        private int thrownTime = 0;
        private const int buffer = 20;
        private const int frameUpdate = 5;
        private const int frameCalculation = 4;
        private const int squared = 2;

        private const float speedPerSec = 40;
        private const float updatePerSec = 40;
        private float speed;
        private int soundCode;

        public ReturningBoomerangState(Boomerang item, Vector2 initPos, LinkPlayer link, int soundCode)
        {
            this.item = item;
            this.position = initPos;
            this.link = link;
            speed = speedPerSec / updatePerSec;
            this.soundCode = soundCode;
        }

        public void Update()
        {
            Vector2 linkLocation = link.CurrentLocation;
            int distanceToLink = (int)(Math.Sqrt(Math.Pow((position.X-linkLocation.X), squared) + Math.Pow((position.Y-linkLocation.Y), squared)));

            if (distanceToLink < buffer)
            {
                item.ReturnedToLink();
                Expire();
            }
            else
            { 
                AdjustReturnLocation(linkLocation);
                item.UpdateLocation(position);
                thrownTime++;
            }

            if (thrownTime % frameUpdate == 0)
                frame++;
            item.UpdateFrame(frame % frameCalculation);
        }

        public void AdjustReturnLocation(Vector2 linkLocation)
        {
            if (position.X < linkLocation.X)
                position.X += speed;
            else
                position.X -= speed;

            if (position.Y < linkLocation.Y)
                position.Y += speed;
            else
                position.Y -= speed;
        }

        public void Expire()
        {
            item.IsExpired = true;
            Sounds.Instance.StopLoopedSound(soundCode.ToString());
            CollisionHandler.Instance.RemoveCollider(item.Collider);
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {

        }
    }
}
