﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class HeartState : IItemsState
    {
        private Heart item;
        private Vector2 position;
        private int runTime;
        private const int maxTime = 50;
        private int frame;

        public HeartState(Heart item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
            runTime = 0;
            frame = 0;
        }

        public void Update()
        {
            if (runTime % 5 == 0)
            {
                frame = (frame + 1) % 2;
            }
            item.UpdateFrame(frame);

            runTime++;
            if (runTime >= maxTime)
            {
                Expire();
            }
        }

        public void Expire()
        {

        }

        public void Collected()
        {

        }
    }
}
