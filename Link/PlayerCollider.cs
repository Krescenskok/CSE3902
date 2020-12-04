using Microsoft.Xna.Framework;
using Sprint5;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Sprint5.GamePadVibration;

namespace Sprint5
{
    public class PlayerCollider : ICollider
    {
        LinkPlayer linkPlayer;
        private Rectangle bounds;

        public PlayerCollider(LinkPlayer linkPlayer,Rectangle bounds)
        {
            this.linkPlayer = linkPlayer;
            CollisionHandler.Instance.AddCollider(this, Layers.Player);

            this.bounds = bounds;
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
                testKnockback(collision);

            }


        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Block") || col.CompareTag("Wall") || col.CompareTag("Door"))
            {

                linkPlayer.isWalkingInPlace = true;
                linkPlayer.HandleObstacle(collision);
                testKnockback(collision);


            }

            if (col.CompareTag("enemy"))
                col.SendMessage("Dissapear", null);

            if (col.CompareTag("LockedDoor"))
            {
                col.SendMessage("Unlock", null);
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
                        GamePadVibrate.Instance.TakeDamage("Right");
                        linkPlayer.knockback(Direction.right);



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
                        GamePadVibrate.Instance.TakeDamage("Left");
                        linkPlayer.knockback(Direction.left);
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
                        GamePadVibrate.Instance.TakeDamage("Up");
                        linkPlayer.knockback(Direction.up);
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
                        GamePadVibrate.Instance.TakeDamage("Down");
                        linkPlayer.knockback(Direction.down);
                    }
                }

            }

            if (msg == "WalkInPlace")
            {
                linkPlayer.isWalkingInPlace = true;
            }

            if (msg == "Item")
            {
                if (((IItems)value) is Sprint5.Items.TriforcePiece || ((IItems)value) is Sprint5.Items.Bow)
                    linkPlayer.IsPickingUpItem = true;

                LinkInventory.Instance.PickUpItem((IItems)value, linkPlayer);
            }
            if (msg == "Heal")
            {
                if (value is Sprint5.Items.Heart)
                {
                    linkPlayer.Health += HPAmount.Full_Heart;
                }
                else if (value is Sprint5.Items.Fairy)
                {
                    linkPlayer.Health += HPAmount.HalfHeart;
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
                linkPlayer.FullHealth += HPAmount.Full_Heart;
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

        public void testKnockback(Collision collision)
        {
            //if (collision.Right && linkPlayer.DamDir == Direction.right) linkPlayer.stopKnockback();
            //if (collision.Left && linkPlayer.DamDir == Direction.left) linkPlayer.stopKnockback();
            //if (collision.Down && linkPlayer.DamDir == Direction.down) linkPlayer.stopKnockback();
           // if (collision.Up && linkPlayer.DamDir == Direction.up) linkPlayer.stopKnockback();
        }

        public void Update()
        {
            bounds.Location = linkPlayer.currentLocation.ToPoint();
        }
    }
}
