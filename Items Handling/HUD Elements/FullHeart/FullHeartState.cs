using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class FullHeartState : IItemsState
    {
        private FullHeart item;

        public FullHeartState(FullHeart item)
        {
            this.item = item;
        }

        public void Update()
        {
           
        }

        public void Expire()
        {
            item.IsExpired = true;
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {

        }
    }
}
