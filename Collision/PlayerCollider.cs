using Microsoft.Xna.Framework;
using Sprint3;
using Sprint3.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class PlayerCollider : ICollider
    {
        private Rectangle bounds;
        private float damageAmount;
        LinkPlayer linkPlayer;
        String Key;

        public PlayerCollider(LinkPlayer linkPlayer)
        {

            this.linkPlayer = linkPlayer;
            //CollisionHandler.Instance.AddCollider(this);
        }

        public Rectangle Bounds()
        {
            return linkPlayer.Bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Player" || tag == "player";
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
            if (col.CompareTag("enemy"))
            {
                if (linkPlayer.IsAttacking)
                {
                    if (collision.Left())
                    {
                        if (linkPlayer.state is MoveLeft)
                        {
                            if (linkPlayer.CurrentWeapon == Weapon.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 20);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 35);
                            }
                        }
                    }
                }
            }

            if(col.CompareTag("item"))
            {
                col.SendMessage("Disappear", null);
            }
            if(col.CompareTag("block"))
            {
                col.SendMessage("MoveBlock", 10);
            }
        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage")
            {
                linkPlayer.IsDamaged = true;
                linkPlayer.Health -= (float) value;

            }

        }
    }
}
