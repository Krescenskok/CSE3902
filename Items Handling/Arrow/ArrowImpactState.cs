using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class ArrowImpactState : IItemsState
    {
        private Arrow arrow;
        private Vector2 location;
        private int runTime;
        private const int maxTime = 75;

        public ArrowImpactState(Arrow arrow)
        {
            this.arrow = arrow;
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
            arrow.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           
        }
    }
}
