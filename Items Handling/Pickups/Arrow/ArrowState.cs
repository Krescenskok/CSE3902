using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class ArrowState : IItemsState
    {
        private ArrowObject item;

        public ArrowState(ArrowObject arrow)
        {
            this.item = arrow;
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
