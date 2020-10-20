using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class RedPotionState : IItemsState
    {
        private RedPotion potion;
        private Vector2 position;

        public RedPotionState(RedPotion potion, Vector2 initPos)
        {
            this.potion = potion;
            this.position = initPos;
        }

        public void Update()
        {

        }

        public void Expire()
        {

        }

        public void Collected()
        {

        }
    }
}
