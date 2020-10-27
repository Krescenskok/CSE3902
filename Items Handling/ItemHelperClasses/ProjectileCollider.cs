using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class ProjectileCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        public string name;

        public string Name { get => name; }

        public ProjectileCollider(Rectangle rect, IItems item, IItemsState state, String name)
        {
            bounds = rect;

            this.item = item;

            this.state = state;

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
            return tag == name || tag == "Projectile";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
        }
        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string direction = collision.From().ToString();
            direction = char.ToUpper(direction[0]) + direction.Substring(1);

            if (col.CompareTag("Enemy"))
            {
                if (name == "Arrow")
                {
                    col.SendMessage("EnemyTakeDamage" + direction, HPAmount.QuarterHeart);
                }
                else if (name == "CandleFire")
                {
                    col.SendMessage("EnemyTakeDamage" + direction, HPAmount.QuarterHeart);
                }
                else if (name == "SwordBeam")
                {
                    col.SendMessage("EnemyTakeDamage" + direction, HPAmount.QuarterHeart);
                }
                else if (name == "WandBeam")
                {
                    col.SendMessage("EnemyTakeDamage" + direction, HPAmount.QuarterHeart);
                }
                item.State.Expire();
            }
        }

        public void SendMessage(string msg, object value)
        {

            
        }


        public void Update(IItems itemObj, IItemsState itemState)
        {
            this.state = itemObj.State;
            bounds.Location = itemObj.Location.ToPoint();
        }
    }
}
