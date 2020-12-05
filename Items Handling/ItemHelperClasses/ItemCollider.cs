using Microsoft.Xna.Framework;
using Sprint5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5.Items
{
    public class ItemCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
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

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player"))
            {
                col.SendMessage("Item", this.item);
                this.item.State.Expire();
            }

        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void SendMessage(string msg, object value)
        {
            if (msg == "Dissapear")
            {
                this.item.Expire();
                this.item.State.Expire();
            }            
        }


        public void Update()
        {
            state = item.State;
            bounds.Location = item.Location.ToPoint();
        }
    }
}
