using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Items
{
    public class BlueCandleState : IItemsState
    {
        private BlueCandle item;
        private Vector2 position;

        public BlueCandleState(BlueCandle candle, Vector2 initPos)
        {
            this.item = candle;
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
