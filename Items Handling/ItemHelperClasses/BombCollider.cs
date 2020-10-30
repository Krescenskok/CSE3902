using Microsoft.Xna.Framework;
using Sprint4;
using Sprint4.Items;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    public class BombCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        public string name;

        public string Name { get => name; }
        public Layer layer { get; set; }

        public BombCollider(Rectangle rect, IItems item, IItemsState state)
        {
            bounds = rect;

            this.item = item;

            this.state = state;

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
            return tag == name || tag == "Projectile";
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
            if ((this.item as Bomb).Exploding)
            {
                if (col.CompareTag("Enemy"))
                {

                    col.SendMessage("EnemyTakeDamage", HPAmount.FourHits);
                }
                else if (col.CompareTag("DodongoFace") && !(this.item as Bomb).Exploding)
                {
                    col.SendMessage("Bomb", null);
                }
            }

        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "Eaten" && !(this.item as Bomb).Exploding) {
                (this.item as Bomb).Expire();
            }
            
        }


        public void Update(IItems itemObj, IItemsState itemState)
        {
            this.state = itemObj.State;
            bounds.Location = itemObj.Location.ToPoint();
        }
    }
}
