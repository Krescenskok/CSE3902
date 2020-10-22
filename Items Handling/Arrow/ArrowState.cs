using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2
{
    public class ArrowState : IItemsState
    {
        private ArrowObject arrow;

        public ArrowState(ArrowObject arrow)
        {
            this.arrow = arrow;
        }

        public void Update()
        {
           
        }

        public void Expire()
        {
            //expires only when purchased
            arrow.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
           
        }
    }
}
