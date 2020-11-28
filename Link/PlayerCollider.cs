using Microsoft.Xna.Framework;
using Sprint5;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public class PlayerCollider : ICollider
    {
        LinkPlayer linkPlayer;
        const int FULL_HEART = 20;
        const int THREE_QUARTERS_HEART = 15;
        const int HALF_HEART = 10;
        const int QUARTER_HEART = 5;
        const int DISPLACEMENT = 100;

        bool done = false;

        private Rectangle bounds;

        public PlayerCollider(LinkPlayer linkPlayer)
        {
            this.linkPlayer = linkPlayer;
            CollisionHandler.Instance.AddCollider(this, Layers.Player);

            bounds = linkPlayer.hitbox;
            
        }

        public string Name { get => "Player"; }
        public Layer layer { get; set; }

        public Rectangle Bounds()
        {
            return bounds;
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
                                col.SendMessage("EnemyTakeDamage", QUARTER_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", HALF_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", THREE_QUARTERS_HEART);
                            }
                        }
                    }

                    else if (collision.Right)
                    {
                        if (linkPlayer.state is MoveRight)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", QUARTER_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", HALF_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", THREE_QUARTERS_HEART);
                            }
                        }
                    }

                    else if (collision.Up)
                    {
                        if (linkPlayer.state is MoveUp)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", QUARTER_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", HALF_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", THREE_QUARTERS_HEART);
                            }
                        }
                    }

                    else if (collision.Down)
                    {
                        if (linkPlayer.state is MoveDown)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage("EnemyTakeDamage", QUARTER_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage("EnemyTakeDamage", HALF_HEART);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage("EnemyTakeDamage", THREE_QUARTERS_HEART);
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
                if (msg.Contains("TakeDamage"))
                {
                    Sounds.Instance.PlaySoundEffect("LinkHurt");
                }

                if(linkPlayer.IsAttacking && linkPlayer.state is MoveLeft)
                {

                }
                else
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
                }

                if (linkPlayer.IsAttacking && linkPlayer.state is MoveRight)
                {

                }
                else
                {
                    if (msg == "TakeDamageLeft")
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
                }

                if (linkPlayer.IsAttacking && linkPlayer.state is MoveDown)
                {

                }
                else
                {
                    if (msg == "TakeDamageUp")
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
                }

                if (linkPlayer.IsAttacking && linkPlayer.state is MoveUp)
                {

                }
                else
                {
                    if (msg == "TakeDamageDown")
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
              
            }

            if (msg == "WalkInPlace")
            {
                linkPlayer.isWalkingInPlace = true;
            }

            if (msg == "Item")
            {
                if(((IItems) value) is Items.TriforcePiece || ((IItems)value) is Items.Bow)
                    linkPlayer.IsPickingUpItem = true;

                LinkInventory.Instance.PickUpItem((IItems) value, linkPlayer);
            }
            if (msg == "Heal")
            {
                if (value is Items.Heart)
                {
                    linkPlayer.Health += FULL_HEART;
                }
                else if (value is Items.Fairy)
                {
                    linkPlayer.Health += HALF_HEART;
                }

                if (linkPlayer.Health >= linkPlayer.FullHealth)
                {
                    linkPlayer.Health = linkPlayer.FullHealth;
                }
                HUD.Instance.UpdateHearts(linkPlayer);
            }
            if (msg == "HeartContainer")
            {
                HUD.Instance.IncreaseMaxHeartNumber();
                linkPlayer.FullHealth += FULL_HEART;
                linkPlayer.Health = linkPlayer.FullHealth;
                HUD.Instance.UpdateHearts(linkPlayer);
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

           
            bounds.Location = linkPlayer.currentLocation.ToPoint();
        }
    }
}
