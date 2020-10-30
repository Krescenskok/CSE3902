using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class BeamImpactState : IItemsState
    {
        private SwordBeam item;
        private int runTime;
        private const int maxTime = 60;
        private int frame = 0;
        private int stage = 0;

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
                item.UpdateFrame(stage + frame % 2);
            }

            if (runTime % (maxTime / 3) == 0)
            {
                stage += 2;
                item.UpdateFrame(stage + frame % 2);
            }

            if (runTime >= maxTime)
            {
                item.Expire();
            }
        }

        public void Expire()
        {
            item.expired = true;
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {

        }
    }
}