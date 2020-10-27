﻿using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class HealthCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        private string name;

        public string Name { get => name; }

        public HealthCollider(Rectangle rect, IItems item, IItemsState state, String name)
        {
            bounds = rect;

            this.item = item;

            this.state = item.State;

            this.name = name;

            CollisionHandler.Instance.AddCollider(this);
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
            


        }
        //on impact, damage enemies if projectile, so its just one damage action
        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

            if (col.CompareTag("Player"))
            {
                if (this.name.Equals("Heart"))
                {
                    col.SendMessage("Heal", this.item);
                }
                else if (this.name.Equals("HeartContainer"))
                {
                    col.SendMessage("HealContainer", this.item);
                }

                col.SendMessage("Item", this.item);
            }


        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "Dissapear")
            {
                this.item.State.Expire();
            }
            
        }


        public void Update(IItems itemObj)
        {
            this.state = itemObj.State;
            bounds.Location = itemObj.Location.ToPoint();
        }
    }
}
