using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class ArrowImpactState : IItemsState
    {
        private Arrow arrow;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 30;
        private float updatePerSec = 40;
        private float speed;
        private int runTime;
        private const int maxTime = 75;

        public ArrowImpactState(Arrow arrow, Vector2 location)
        {
            this.arrow = arrow;
            this.location = location;
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
