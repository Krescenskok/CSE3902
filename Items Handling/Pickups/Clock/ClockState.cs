using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ClockState : IItemsState
    {
        private Clock item;
        private Vector2 position;
        private int runTime = 0;
        private const int maxTime = 800;
        private bool collected = false;

        public ClockState(Clock item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
        }

        public void Update()
        {
            if (collected)
            {
                runTime++;
                if (runTime >= maxTime)
                {
                    collected = false;
                    //disable the effects of the clock
                }
            }
        }

        public void Expire()
        {
            item.IsExpired = true;

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
