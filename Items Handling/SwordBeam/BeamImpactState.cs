using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class BeamImpactState : IItemsState
    {
        private SwordBeam item;
        private string direction;
        private Vector2 location;
        private float speedPerSec = 30;
        private float updatePerSec = 40;
        private float speed;
        private int runTime;
        private const int maxTime = 250;

        public BeamImpactState(SwordBeam item, Vector2 location)
        {
            this.item = item;
            this.location = location;
            runTime = 0;
            speed = speedPerSec / updatePerSec;
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
