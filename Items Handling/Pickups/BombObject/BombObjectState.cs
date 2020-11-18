using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class BombObjectState : IItemsState
    {
        private BombObject item;
        private Vector2 position;
        private int runTime;
        private const int maxTime = 500;

        public BombObjectState(BombObject item, Vector2 initPos)
        {
            this.item = item;
            runTime = 0;
            this.position = initPos;

            //remove a bomb from inventory
        }

        public void Update()
        {
        }

        public void Expire()
        {
            item.IsExpired = true;

            item.Expire();
        }

        public void Collected()
        {

        }
    }
}
