using Microsoft.Xna.Framework;
using Sprint4;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    public class PlayerCollider : ICollider
    {
        //private Rectangle bounds;
        LinkPlayer linkPlayer;
        //String Key;

        bool done = false;
        public PlayerCollider(LinkPlayer linkPlayer)
        {
            this.linkPlayer = linkPlayer;
            CollisionHandler.Instance.AddCollider(this, Layers.Player);

           
        }

        public string Name { get => "Player"; }
        public Layer layer { get; set; }

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
                if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("Door"))
                {

                    linkPlayer.isWalkingInPlace = true;
                    linkPlayer.HandleObstacle(collision);


                }


        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("Door"))
            {

                linkPlayer.isWalkingInPlace = true;
                linkPlayer.HandleObstacle(collision);


            }
            
           if (col.CompareTag("enemy"))
                col.SendMessage("Dissapear", null);

            if (col.CompareTag("LockedDoor")) //and link has key!
            {
                col.SendMessage("Unlock", null);
            }


            
                if (linkPlayer.IsAttacking)
                {
                    if (collision.Left)
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

                    else if (collision.Right)
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

                    else if (collision.Up)
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

                    else if (collision.Down)
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

            

            if (col.CompareTag("Item"))
            {
                col.SendMessage("Disappear", null);
            }

        }


        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }

        public void SendMessage(string msg, object value)
        {
            if(!linkPlayer.IsDamaged)
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

                    linkPlayer.currentLocation.X += 100;

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
                    linkPlayer.currentLocation.X -= 100;
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
                    linkPlayer.currentLocation.Y -= 100;
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
                    linkPlayer.currentLocation.Y += 100;
                }
            }

            if (msg == "WalkInPlace")
            {
                linkPlayer.isWalkingInPlace = true;
            }

            if (msg == "Item")
            {
                if(((IItems) value) is Sprint4.Items.TriforcePiece || ((IItems)value) is Sprint4.Items.Bow)
                    linkPlayer.IsPickingUpItem = true;

                LinkInventory.Instance.PickUpItem((IItems) value, linkPlayer);
            }
            if (msg == "Heal")
            {
                linkPlayer.Health += (float)value;
                if (linkPlayer.Health >= linkPlayer.FullHealth)
                {
                    linkPlayer.Health = 30;
                }
                HUD.Instance.UpdateHearts(linkPlayer);
            }
            if (msg == "Heartcontainer")
            {
                linkPlayer.FullHealth += 10;
                linkPlayer.Health = linkPlayer.FullHealth;
                HUD.Instance.IncreaseMaxHeartNumber();
                HUD.Instance.UpdateHearts(linkPlayer);
            }

            if (msg == "Rupee")
            {
                LinkInventory.Instance.RupeeCount++;
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

        public void Update()
        {
            Debug.WriteLine(Bounds());
            if (Bounds() != null && !done)
            {

                RoomEnemies.Instance.AddTestCollider(this);
                if (Bounds().X > 1000) done = true;

                
            }
        }
    }
}
