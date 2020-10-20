using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class RedRingState : IItemsState
    {
        private RedRing ring;
        private Vector2 position;

        public RedRingState(RedRing ring, Vector2 initPos)
        {
            this.ring = ring;
            this.position = initPos;
        }

        public void Update()
        {

        }

        public void Expire()
        {

        }

        public void Collected()
        {

        }
    }
}
