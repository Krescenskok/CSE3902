using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.Items
{
    public class CompassState : IItemsState
    {
        private Compass item;
        private Vector2 position;

        public CompassState(Compass item, Vector2 initPos)
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
            //show dots on map locating bosses
            Expire();
        }
    }
}
