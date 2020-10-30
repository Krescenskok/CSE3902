using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class MapState : IItemsState
    {
        private Map item;
        private Vector2 position;

        public MapState(Map item, Vector2 initPos)
        {
            this.item = item;
            this.position = initPos;
        }

        public void Update()
        {

        }

        public void Expire()
        {
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
