using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BombExplosionState : IItemsState
    {
        private Bomb item;
        private Vector2 position;
        private int runTime = 0;
        private int frame = 0;
        private const int maxTime = 100;

        public BombExplosionState(Bomb item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
            item.UpdateSprite(ItemsFactory.Instance.CreateExplosionSprite());
            item.UpdateFrame(runTime);
        }

        public void Update()
        {
            runTime++;
            if (runTime == maxTime / 2)
            {
                frame++;
                item.UpdateFrame(frame);
            }
            else if (runTime >= maxTime)
            {
                Expire();
            }

        }

        public void Expire()
        {
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());

            //remove item from room
        }

        public void Collected()
        {
            //add to inventory
            Expire();
        }
    }
}
