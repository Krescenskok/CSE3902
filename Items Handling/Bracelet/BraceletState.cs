using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BraceletState : IItemsState
    {
        private Bracelet item;
        private Vector2 position;

        public BraceletState(Bracelet item, Vector2 initPos)
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
