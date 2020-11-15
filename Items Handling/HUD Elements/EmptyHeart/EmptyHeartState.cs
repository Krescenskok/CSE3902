using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Items
{
    public class EmptyHeartState : IItemsState
    {
        private EmptyHeart item;
        private Vector2 position;

        public EmptyHeartState(EmptyHeart item, Vector2 initPos)
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
            item.UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collected()
        {
            
        }
    }
}
