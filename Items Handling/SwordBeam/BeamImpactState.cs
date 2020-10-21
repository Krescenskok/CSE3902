﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class BeamImpactState : IItemsState
    {
        private SwordBeam item;
        private int runTime;
        private const int maxTime = 250;

        public BeamImpactState(SwordBeam item)
        {
            this.item = item;
            runTime = 0;
        }

        public void Update()
        {
            //make the sprites spread out

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
