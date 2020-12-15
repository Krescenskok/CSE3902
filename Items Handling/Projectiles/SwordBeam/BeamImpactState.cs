using Microsoft.Xna.Framework;
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
        private const string UP = "Up";
        private const string DOWN = "Down";
        private const int adjust = 15;

        public BeamImpactState(SwordBeam item, string direction)
        {
            this.item = item;
            item.UpdateSprite(ItemsFactory.Instance.CreateBeamImpactSprite());
            item.UpdateFrame(0);
            runTime = 0;
            AdjustVerticalSprite(direction);
        }

        private void AdjustVerticalSprite(string direction)
        {
            Vector2 loc = item.Location;
            if (direction is UP || direction is DOWN)
            { 
                loc.X -= adjust;
                item.UpdateLocation(loc);
            }
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