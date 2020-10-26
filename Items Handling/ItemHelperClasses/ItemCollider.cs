using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class ItemCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        public string name;

        public string Name { get => name; }

        public ItemCollider(Rectangle rect, IItems item, IItemsState state, string name)
        {
            bounds = rect;

            this.item = item;

            this.state = state;

            CollisionHandler.Instance.AddCollider(this);

            this.name = name;
        }

        public ItemCollider()
        {

        }

        public void ChangeState(IItemsState state)
        {
            item = state;
        }

        public Rectangle Bounds()
        {
            return bounds;
            
        }

        public bool CompareTag(string tag)
        {
            return tag == name || tag == "Item";
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
            string direction = collision.From().ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);
            if (this.name.Equals("ArrowFlying") || this.name.Equals("CandleFire") || this.name.Equals("SwordBeam") || this.name.Equals("WandBeam"))
            {
                if (col.CompareTag("Enemy")) col.SendMessage("TakeDamage" + direction, damageAmount);
            }

        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "EnemyTakeDamage") enemy.TakeDamage((int)value);
            else if (msg == "Stun" && !Name.Equals("Keese")) enemy.Stun();
            else if (msg == "Stun") enemy.Die();
            
        }


        public void Update(IItems itemObj, IItemsState itemState)
        {
            this.state = itemState;
            bounds.Location = itemState.Location.ToPoint();
        }
    }
}
