using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class HeartContainerState : IItemsState
    {
        private HeartContainer item;
        private Vector2 position;

        public HeartContainerState(HeartContainer item, Vector2 initPos)
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
            //add another heart container
            Expire();
        }
    }
}
