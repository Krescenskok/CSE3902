using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2.Items
{
    public class BluePotionState : IItemsState
    {
        private BluePotion potion;
        private Vector2 position;

        public BluePotionState(BluePotion potion, Vector2 initPos)
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
            //remove from inventory
        }

        public void Collected()
        {
            //add to inventory
            //if there is already a blue potion in inventory - combine 2 blues to make a red potion
        }
    }
}
