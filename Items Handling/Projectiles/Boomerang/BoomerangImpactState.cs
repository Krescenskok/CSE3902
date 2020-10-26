using Microsoft.Xna.Framework;
using Sprint3.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class BoomerangImpactState : IItemsState
    {
        private BoomerangImpact item;
        private Vector2 location;
        private int runTime;
        private const int maxTime = 50;
        public bool isExpired = false;

        public BoomerangImpactState(BoomerangImpact item, Vector2 location)
        {
            this.location = location; 
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
            isExpired = true;
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           
        }
    }
}
