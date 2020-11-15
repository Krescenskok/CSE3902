using Microsoft.Xna.Framework;
using Sprint4;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    public class ItemCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        public string name;

        public string Name { get => name; }
        public Layer layer { get; set; }

        public ItemCollider(Rectangle rect, IItems item, IItemsState state)
        {
            bounds = rect;

            this.item = item;

            this.state = item.State;

            CollisionHandler.Instance.AddCollider(this, Layers.Item);
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
            return tag == name || tag == "Item";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {


            if (col.CompareTag("Player"))
            {

                    col.SendMessage("Item", this.item);
                
            }




        }
        //on impact, damage enemies if projectile, so its just one damage action
        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

                if (col.CompareTag("Player")) col.SendMessage("Item", this.item);

        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void SendMessage(string msg, object value)
        {
            if (msg == "Dissapear")
            {
                this.item.State.Expire();
            }
            
        }

        //can probably be deleted//
        public void Update(IItems itemObj)
        {
            this.state = itemObj.State;
            bounds.Location = itemObj.Location.ToPoint();
        }

        public void Update()
        {
            state = item.State;
            bounds.Location = item.Location.ToPoint();
        }
    }
}
