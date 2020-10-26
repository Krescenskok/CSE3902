using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class BoomerangCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        public string name;

        public string Name { get => name; }

        public BoomerangCollider(Rectangle rect, IItems item, IItemsState state, string name)
        {
            bounds = rect;

            this.item = item;

            this.state = state;

            CollisionHandler.Instance.AddCollider(this);

            this.name = name;
        }

        public BoomerangCollider()
        {

        }

        public void ChangeState(IItemsState state)
        {
            this.state = state;
        }

        public Rectangle Bounds()
        {
            return bounds;
            
        }

        public bool CompareTag(string tag)
        {
            return tag == name || tag == "Boomerang";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {

            if (col.CompareTag("Block")) {

                col.SendMessage("Item", this.item);

            } else if (col.CompareTag("Player"))
            {
                if (this.name.Equals("Heart"))
                {
                    col.SendMessage("Heal", this.item);
                } else if (this.name.Equals("HeartContainer"))
                {
                    col.SendMessage("HealContainer", this.item);
                } else
                {
                    col.SendMessage("Item", this.item);
                }
            } else if (col.CompareTag("Enemy"))
            {
                //ignore because only projectiles need interaction w/ enemy and this is handled in below function

            } //we ignore other items (for now)

        }
        //on impact, damage enemies if projectile, so its just one damage action
        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
       
                if (col.CompareTag("Enemy")) col.SendMessage("Stun", null);
            
        }

        public void SendMessage(string msg, object value)
        {
            
        }


        public void Update(IItems itemObj)
        {
            this.state = itemObj.State;
            bounds.Location = itemObj.Location.ToPoint();
        }
    }
}
