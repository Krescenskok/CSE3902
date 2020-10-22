using Microsoft.Xna.Framework;
using Sprint3.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class BoomerangImpactState : IItemsState
    {
        private Boomerang item;
        private int runTime;
        private const int maxTime = 75;

        public BoomerangImpactState(Boomerang item)
        {
            this.item = item;
            runTime = 0;
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
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           
        }
    }
}
