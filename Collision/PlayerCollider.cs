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

            System.Diagnostics.Debug.WriteLine("Collision");

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
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }

                    else if (collision.Right())
                    {
                        if(linkPlayer.state is MoveRight)
                        {
                            if (linkPlayer.CurrentWeapon == Weapon.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }

                    else if (collision.Up())
                    {
                        if (linkPlayer.state is MoveUp)
                        {
                            if (linkPlayer.CurrentWeapon == Weapon.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }

                    else if (collision.Down())
                    {
                        if (linkPlayer.state is MoveDown)
                        {
                            if (linkPlayer.CurrentWeapon == Weapon.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == Weapon.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }
                }
                
            }

            if (col.CompareTag("item"))
            {
                col.SendMessage("Disappear", null);
            }

        }

        public void SendMessage(string msg, object value)
        {
            if (msg == "TakeDamage")
            {
                linkPlayer.IsDamaged = true;
                linkPlayer.Health -= (float) value;
                linkPlayer.currentLocation.X -= 200;
            }

            if (msg == "WalkInPlace")
            {
                linkPlayer.isWalkingInPlace = true;
            }

            if (msg == "PickUpItem")
            {
                linkPlayer.IsPickingUpItem = true;
            }
        }
    }
}
