using Microsoft.Xna.Framework;
using Sprint5;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class ProjectileCollider : ICollider
    {
        private Rectangle bounds;
        private IItemsState state;
        private IItems item;
        private int damageAmount;
        public string name;

        public string Name { get => name; }
        public Layer layer { get; set; }

        public ProjectileCollider(Rectangle rect, IItems item, IItemsState state, String name)
        {
            bounds = rect;

            this.item = item;

            this.state = state;

            this.name = name;

            CollisionHandler.Instance.AddCollider(this, Layers.PlayerWeapon);

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
            if (col.CompareTag("Wall"))
            {
                item.State.Expire();
            }
        }


        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            string direction = collision.From.ToString();
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

            if (col.CompareTag("Wall") || col.CompareTag("Door"))
            {
                item.Expire();
            }
        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "Impact") item.Expire();
            
        }

        //can probably be deleted//
        public void Update(IItems itemObj, IItemsState itemState)
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
