using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class CandleFireState : IItemsState
    {
        private CandleFire item;
        private Vector2 position;
        private int runTime = 0;
        private const int maxTime = 200;

        public CandleFireState(CandleFire item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
        }

        public void Update()
        {
            runTime++;
            if (runTime >= maxTime)
            {
                Expire();
            }
        }

        public void Expire()
        {
            item.expired = true;
            CollisionHandler.Instance.RemoveCollider(item.Collider);
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
            //both link and enemies can be damaged by fire

            Expire();
        }
    }
}
