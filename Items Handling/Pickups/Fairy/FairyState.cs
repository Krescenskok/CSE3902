using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class FairyState : IItemsState
    {
        private Fairy item;
        private Vector2 initialPos;
        private Vector2 position;
        private const int horizontal = 100;
        private const int vertical = 80;
        private int runTime;
        private const int maxTime = 500;
        private int frame;
        private bool right;
        private bool down;

        public FairyState(Fairy item, Vector2 initPos)
        {
            this.item = item;
            this.initialPos = initPos;
            this.position = initPos;
            runTime = 0;
            frame = 0;
            right = true;
            down = false;

        }

        public void Update()
        {
            float xDiff = initialPos.X - position.X;
            float yDiff = initialPos.Y - position.Y;

           if (xDiff < 0 && Math.Abs(xDiff) >= horizontal / 2)
           {
               right = false;
           }
           else if (xDiff > 0 && Math.Abs(xDiff) >= horizontal / 2)
           {
               right = true;
           }

           if (yDiff < 0 && Math.Abs(yDiff) >= vertical / 2)
           {
               down = false;
           }
           else if (yDiff > 0 && Math.Abs(yDiff) >= vertical / 2)
           {
               down = true;
           }

           if (right)
           {
                position.X++;
           }
           else
           {
                position.X--;
           }

           if (down)
           {
                position.Y++;
           }
           else
           {
                position.Y--;
           }
            item.UpdateLocation(position);

            if (runTime % 5 == 0)
            {
                frame++;
            }
            item.UpdateFrame(frame % 2);

            runTime++;
            if (runTime >= maxTime)
            {
                Expire();
            }
        }

        public void Expire()
        {
            RoomItems.Instance.Destroy(item);

            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());

            CollisionHandler.Instance.RemoveCollider(item.Collider);

            item.Expire();
        }

        public void Collected()
        {
        }
    }
}
