using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BowState : IItemsState
    {
        private Bow item;
        private Vector2 position;

        public BowState(Bow item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
        }

        public void Update()
        {

        }

        public void Expire()
        {
            //never expires
        }

        public void Collected()
        {
            //player touch only

        }
    }
}
