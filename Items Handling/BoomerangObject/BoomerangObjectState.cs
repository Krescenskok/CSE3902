﻿using Microsoft.Xna.Framework;
using Sprint3.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.Items
{
    public class BoomerangObjectState : IItemsState
    {
        private BoomerangObject item;
        private Vector2 position;

        private bool leaving = true;

        public BoomerangObjectState(BoomerangObject item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
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
            //add to inventory
            Expire();
        }
    }
}
