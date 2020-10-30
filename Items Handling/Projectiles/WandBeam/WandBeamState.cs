using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class WandBeamState : IItemsState
    {
        private WandBeam item;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 60;
        private float updatePerSec = 40;
        private float speed;
        private int runTime;
        private int frame;
        private const int maxTime = 250;

        public WandBeamState(WandBeam item, Vector2 initPos, string direction)
        {
            this.item = item;
            this.location = initPos;
            this.direction = direction;
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
            item.UpdateLocation(location);
            runTime++;

            if (runTime % 5 == 0)
            {
                frame++;
                item.UpdateFrame(frame % 2);
            }

            //for testing only
            ExpireCheck();
        }
        public void ExpireCheck()
        {
            if (location.X < 0 || location.X > GraphicsDeviceManager.DefaultBackBufferWidth)
            {
                Expire();
            }

            if (location.Y < 0 || location.Y > GraphicsDeviceManager.DefaultBackBufferHeight)
            {
                Expire();
            }

            if (runTime >= maxTime)
            {
                Expire();
            }
        }

        public void Expire()
        {
            item.expired = true;
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
            CollisionHandler.Instance.RemoveCollider(item.Collider);
        }

        public void Collected()
        {

        }
    }
}
