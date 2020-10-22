using Microsoft.Xna.Framework;
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
        private int frame = 0;

        public BeamImpactState(SwordBeam item)
        {
            this.item = item;
            item.UpdateSprite(ItemsFactory.Instance.CreateBeamImpactSprite());
            item.UpdateFrame(0);
            runTime = 0;
        }

        public void Update()
        {
            //make the sprites spread out

            runTime++;
            if (runTime % 5 == 0)
            {
                frame++;
                item.UpdateFrame(frame % 2);
            }

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
