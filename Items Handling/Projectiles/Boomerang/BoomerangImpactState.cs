using Microsoft.Xna.Framework;
using Sprint5.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class BoomerangImpactState : IItemsState
    {
        private BoomerangImpact item;
        private Vector2 location;
        private int runTime;
        private const int maxTime = 50;

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
            item.IsExpired = true;
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           
        }
    }
}
