using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.Items
{
    public class ClockState : IItemsState
    {
        private Clock item;
        private Vector2 position;
        private int runTime = 0;
        private const int maxTime = 800;
        private bool collected = false;

        public ClockState(Clock item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
        }

        public void Update()
        {
            if (collected)
            {
                runTime++;
                if (runTime >= maxTime)
                {
                    collected = false;
                    //disable the effects of the clock
                }
            }
        }

        public void Expire()
        {
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
            collected = true;
            //makes Link flash, freezes enemies, and grants him temporary immunity
            Expire();
        }
    }
}
