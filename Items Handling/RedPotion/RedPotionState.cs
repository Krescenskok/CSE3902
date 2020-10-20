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

        public void Drink()
        {
            //restores Link's health
            Expire();
        }

        public void Expire()
        {
            //turn into a blue potion
        }

        public void Collected()
        {
            //add to inventory
        }
    }
}
