using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class WoodenSwordState : IItemsState
    {
        private WoodenSword item;
        private Vector2 position;

        public WoodenSwordState(WoodenSword item, Vector2 initPos)
        {
            this.item = item;
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
