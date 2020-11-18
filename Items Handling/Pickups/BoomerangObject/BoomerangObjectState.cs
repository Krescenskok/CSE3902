using Microsoft.Xna.Framework;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class BoomerangObjectState : IItemsState
    {
        private BoomerangObject item;
        private Vector2 position;

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
            item.IsExpired = true;

            RoomItems.Instance.Destroy(item);

            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());

            CollisionHandler.Instance.RemoveCollider(item.Collider);

            
        }

        public void Collected()
        {
        }
    }
}
