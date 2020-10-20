using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BombState : IItemsState
    {
        private Bomb item;
        private Vector2 position;
        private int runTime;
        private const int maxTime = 50;

        public BombState(Bomb item, Vector2 initPos)
        {
            this.item = item;
            runTime = 0;
            this.position = initPos;
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
            //bomb explodes
            item.Exploded();
        }

        public void Collected()
        {

        }
    }
}
