using Microsoft.Xna.Framework;
using Sprint3;
using Sprint3.Link;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class PlayerCollider : ICollider
    {
        //private Rectangle bounds;
        LinkPlayer linkPlayer;
        private IItems item;
        //String Key;
        int WOODEN_DAMAGE = 5;
        int SWORD_DAMAGE = 10;
        int ROD_DAMAGE = 15;
        int TWO = 2;
        int LOC_CHANGE = 50;
        int HEAL = 30;
        int HEALTH_INC = 10;
        String ENEMY_TAKE_DAMAGE = "EnemyTakeDamage";
        String TAKE_DAMAGE_RIGHT = "TakeDamageRight";
        String TAKE_DAMAGE_DOWN = "TakeDamageDown";
        String TAKE_DAMAGE_LEFT = "TakeDamageLeft";
        String TAKE_DAMAGE_UP = "TakeDamageUP";
        String PLAYER = "Player";
        String pPLAYER = "player";
        String BLOCK = "Block";
        String ENEMY = "enemy";
        String ITEM = "Item";
        String DISAPPEAR = "Disappear";
        String WALKINPLACE = "WalkInPlace";
        String HEAL_STR = "Heal";
        String HEARTCONTAINER = "HeartContainer";
        String RUPEE = "Rupee";
        String HAND = "Hand";
        String SPECIALBLOCK = "Special Block";
        String LARGESHIELD = "Large Shield";
  

        public PlayerCollider(LinkPlayer linkPlayer)
        {
            this.linkPlayer = linkPlayer;
            this.item = item;
            CollisionHandler.Instance.AddCollider(this, Layers.Player);
        }

        public string Name { get => PLAYER; }
        public Layer layer { get; set; }

        public Rectangle Bounds()
        {

            return linkPlayer.Bounds;

        }

        public bool CompareTag(string tag)
        {
            return tag == PLAYER || tag == pPLAYER;
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag(BLOCK))
            {

                linkPlayer.isWalkingInPlace = true;

            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

                if (col.CompareTag(BLOCK))
                {

                linkPlayer.isWalkingInPlace = true;

            } else 
                if (col.CompareTag(ENEMY))
            {
                if (linkPlayer.IsAttacking)
                {
                    if (collision.Left())
                    {
                        if (linkPlayer.state is MoveLeft)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, WOODEN_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, SWORD_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, ROD_DAMAGE);
                            }
                        }
                    }

                    else if (collision.Right())
                    {
                        if (linkPlayer.state is MoveRight)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, WOODEN_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, SWORD_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, ROD_DAMAGE);
                            }
                        }
                    }

                    else if (collision.Up())
                    {
                        if (linkPlayer.state is MoveUp)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, WOODEN_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, SWORD_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, ROD_DAMAGE);
                            }
                        }
                    }

                    else if (collision.Down())
                    {
                        if (linkPlayer.state is MoveDown)
                        {
                            if (linkPlayer.CurrentWeapon == ItemForLink.WoodenSword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, WOODEN_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.Sword)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, SWORD_DAMAGE);
                            }
                            else if (linkPlayer.CurrentWeapon == ItemForLink.MagicalRod)
                            {
                                col.SendMessage(ENEMY_TAKE_DAMAGE, ROD_DAMAGE);
                            }
                        }
                    }
                }

            }

            if (col.CompareTag(ITEM))
            {
                col.SendMessage(DISAPPEAR, null);
            }

        }

        public void SendMessage(string msg, object value)
        {
            if(!linkPlayer.IsDamaged)
            {
                if (msg == TAKE_DAMAGE_RIGHT)
                {
                    linkPlayer.IsDamaged = true;
                    if (linkPlayer.UseRing)
                    {
                        linkPlayer.Health -= ((int)value) / TWO;
                    }
                    else
                    {
                        linkPlayer.Health -= (int)value;
                    }
                    linkPlayer.currentLocation.X += LOC_CHANGE;

                }

                else if (msg == TAKE_DAMAGE_LEFT)
                {
                    linkPlayer.IsDamaged = true;
                    if (linkPlayer.UseRing)
                    {
                        linkPlayer.Health -= ((int)value) / TWO;
                    }
                    else
                    {
                        linkPlayer.Health -= (int)value;
                    }

                    linkPlayer.currentLocation.X -= LOC_CHANGE;
                }

                else if (msg == TAKE_DAMAGE_UP)
                {
                    linkPlayer.IsDamaged = true;
                    if (linkPlayer.UseRing)
                    {
                        linkPlayer.Health -= ((int)value) / TWO;
                    }
                    else
                    {
                        linkPlayer.Health -= (int)value;
                    }

                    linkPlayer.currentLocation.Y -= LOC_CHANGE;
                }

                else if (msg == TAKE_DAMAGE_DOWN)
                {
                    linkPlayer.IsDamaged = true;
                    if (linkPlayer.UseRing)
                    {
                        linkPlayer.Health -= ((int)value) / TWO;
                    }
                    else
                    {
                        linkPlayer.Health -= (int)value;
                    }

                    linkPlayer.currentLocation.Y += LOC_CHANGE;
                }
            }
            

            if (msg == WALKINPLACE)
            {
                linkPlayer.isWalkingInPlace = true;
            }

            if (msg == ITEM)
            {
                linkPlayer.IsPickingUpItem = true;
                linkPlayer.itemsPickedUp.Add((IItems)value);
            }
            if (msg == HEAL_STR)
            {
                linkPlayer.Health += (float)value;
                if (linkPlayer.Health >= linkPlayer.FullHealth)
                {
                    linkPlayer.Health = HEAL;
                }
            }
            if (msg == HEARTCONTAINER)
            {
                linkPlayer.FullHealth += HEALTH_INC;
                linkPlayer.Health = linkPlayer.FullHealth;
            }

            if (msg == RUPEE)
            {
                linkPlayer.NumOfRupee++;
            }

            if (msg == HAND)
            {
                linkPlayer.currentLocation = (Vector2)value;
            }

            if (msg == SPECIALBLOCK)
            {
                linkPlayer.isWalkingInPlace = true;
                linkPlayer.Delay = TWO;
            }

            if( msg == LARGESHIELD)
            {
                linkPlayer.LargeShield = true;
            }
        }
    }
}
