﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class BombExplosionState : IItemsState
    {
        private Bomb item;
        private int runTime;
        private const int maxTime = 240;
        private int frame = 0;

        public BombExplosionState(Bomb item)
        {
            this.item = item;
            item.UpdateSprite(ItemsFactory.Instance.CreateExplosionSprite());
            item.UpdateFrame(runTime);
        }

        public void Update()
        {
            runTime++;
            if (runTime % (maxTime / 3) == 0)
            {
                frame++;
                item.UpdateFrame(frame);
            }

            if (runTime >= maxTime)
            {
                Expire();
            }

        }

        public void Expire()
        {
            item.IsExpired = true;

            item.Expire();
        }

        public void Collected()
        {

            Expire();
        }
    }
}
