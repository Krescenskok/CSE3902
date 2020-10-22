﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
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
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {

        }
    }
}
