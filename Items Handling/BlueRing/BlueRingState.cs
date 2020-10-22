using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.Items
{
    public class BlueRingState : IItemsState
    {
        private BlueRing ring;
        private Vector2 position;

        public BlueRingState(BlueRing ring, Vector2 initPos)
        {
            this.ring = ring;
            this.position = initPos;
        }

        public void Update()
        {

        }

        public void Expire()
        {
            ring.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
            //add 1 to inventory
            Expire();
        }
    }
}
