﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class BluePotionState : IItemsState
    {
        private BluePotion item;
        private Vector2 position;

        public BluePotionState(BluePotion potion, Vector2 initPos)
        {
            this.item = potion;
            this.position = initPos;
        }

        public void Update()
        {
            
        }

        public void Expire()
        {
            item.IsExpired = true;

            RoomItems.Instance.Destroy(item);

            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());

            CollisionHandler.Instance.RemoveCollider(item.Collider);

            item.Expire();
        }

        public void Collected()
        {
        }
    }
}
