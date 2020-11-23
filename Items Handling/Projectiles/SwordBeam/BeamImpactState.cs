using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class BeamImpactState : IItemsState
    {
        private SwordBeam item;
        private int runTime;
        private const int maxTime = 60;
        private const int thirdTime = maxTime / 3;
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
            runTime++;
            if (runTime % 5 == 0)
            {
                frame++;
                item.UpdateFrame(stage + frame % 2);
            }

            if (runTime % thirdTime == 0)
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
            item.IsExpired = true;
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {

        }
    }
}