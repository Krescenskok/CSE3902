﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class MagicBookState : IItemsState
    {
        private MagicBook item;
        private Vector2 position;

        public MagicBookState(MagicBook item, Vector2 initPos)
        {
            this.item = item;
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
