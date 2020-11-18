using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class RupeeState : IItemsState
    {
        private Rupee item;
        private Vector2 position;
        private int runTime;
        private const int maxTime = 500;
        private int frame;

        public RupeeState(Rupee item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
            runTime = 0;
            frame = 0;
        }

        public void Update()
        {
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
            item.IsExpired = true;

            RoomItems.Instance.Destroy(item);

            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());

            CollisionHandler.Instance.RemoveCollider(item.Collider);

            Sounds.Instance.PlaySoundEffect("GetRupee");
        }

        public void Collected()
        {
        }
    }
}
