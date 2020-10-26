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

            if (col.CompareTag("enemy"))
            {
                if (linkPlayer.IsAttacking)
                {
                    if (collision.Left())
                    {
                        if (linkPlayer.state is MoveLeft)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }

                    else if (collision.Right())
                    {
                        if (linkPlayer.state is MoveRight)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }

                    else if (collision.Up())
                    {
                        if (linkPlayer.state is MoveUp)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", 15);
                            }
                        }
                    }

                    else if (collision.Down())
                    {
                        if (linkPlayer.state is MoveDown)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", 5);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", 10);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
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
            if (msg == "TakeDamageRight")
            {
                linkPlayer.IsDamaged = true;
                if (linkPlayer.UseRing)
                {
                    linkPlayer.Health -= ((int)value) / 2;
                }
                else
                {
                    linkPlayer.Health -= (int)value;
                }
                linkPlayer.currentLocation.X -= 100;
            }

            else if (msg == "TakeDamageLeft")
            {
                linkPlayer.IsDamaged = true;
                if (linkPlayer.UseRing)
                {
                    linkPlayer.Health -= ((int)value) / 2;
                }
                else
                {
                    linkPlayer.Health -= (int)value;
                }
                linkPlayer.currentLocation.X += 100;
            }

            else if (msg == "TakeDamageUp")
            {
                linkPlayer.IsDamaged = true;
                if (linkPlayer.UseRing)
                {
                    linkPlayer.Health -= ((int)value) / 2;
                }
                else
                {
                    linkPlayer.Health -= (int)value;
                }
                linkPlayer.currentLocation.Y += 100;
            }

            else if (msg == "TakeDamageDown")
            {
                linkPlayer.IsDamaged = true;
                if (linkPlayer.UseRing)
                {
                    linkPlayer.Health -= ((int)value) / 2;
                }
                else
                {
                    linkPlayer.Health -= (int)value;
                }
                linkPlayer.currentLocation.Y -= 100;
            }

            if (msg == "WalkInPlace")
            {
                linkPlayer.isWalkingInPlace = true;
            }

            if (msg == "Item")
            {
                linkPlayer.IsPickingUpItem = true;
                linkPlayer.itemsPickedUp.Add((IItems)value);
            }
            if (msg == "Heal")
            {
                linkPlayer.Health += (float)value;
                if (linkPlayer.Health >= linkPlayer.FullHealth)
                {
                    linkPlayer.Health = 30;
                }
            }
            if (msg == "Heartcontainer")
            {
                linkPlayer.FullHealth += 10;
                linkPlayer.Health = linkPlayer.FullHealth;
            }

            if (msg == "Rupee")
            {
                linkPlayer.NumOfRupee++;
            }

            if (msg == "Hand")
            {
                linkPlayer.currentLocation = (Vector2)value;
            }

            if (msg == "Special Block")
            {
                linkPlayer.isWalkingInPlace = true;
                if (linkPlayer.Delay <= 0)
                {
                    linkPlayer.isWalkingInPlace = false;
                }
            }
        }
    }
}
