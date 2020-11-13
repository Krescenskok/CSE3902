using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class ArrowImpactState : IItemsState
    {
        private Arrow arrow;
        private int runTime;
        private const int maxTime = 75;

        public ArrowImpactState(Arrow arrow)
        {
            this.arrow = arrow;
            runTime = 0;
            arrow.UpdateSprite(ItemsFactory.Instance.CreateProjectileImpactSprite());
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
            arrow.expired = true;
            arrow.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           //can't collect
        }
    }
}
