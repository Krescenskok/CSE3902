using System;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint5.Items;
using Sprint5.Link;

namespace Sprint5.Link
{
    public class LinkCommand : ICommand
    {
        protected LinkSprite linkSprite;
        LinkPlayer linkPlayer;
        IItems item;
        String Key;

        public LinkCommand(LinkPlayer linkPlayer, String key)
        {
            this.linkPlayer = linkPlayer;
            this.Key = key;
        }

        public void DoInit(Game game)
        {
        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            linkPlayer.Draw(game, spriteBatch, gameTime);
            ProjectilesCommand.Instance.ExecuteCommand(game, gameTime, spriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            if(Key.Equals("D1"))
                linkPlayer.sprite = (LinkSprite)SpriteFactory.Instance.CreateRedLinkSprite();
            else if(Key.Equals("D2"))
                linkPlayer.sprite = (LinkSprite)SpriteFactory.Instance.CreateTealLinkSprite();
            else if (Key.Equals("D3"))
                linkPlayer.sprite = (LinkSprite)SpriteFactory.Instance.CreateYellowLinkSprite();
            else if (Key.Equals("D4"))
                linkPlayer.sprite = (LinkSprite)SpriteFactory.Instance.CreateBlueLinkSprite();
            else if (Key.Equals("D5"))
                linkPlayer.sprite = (LinkSprite)SpriteFactory.Instance.CreatePinkLinkSprite();
            else if (Key.Equals("D6"))
                linkPlayer.sprite = (LinkSprite)SpriteFactory.Instance.CreateLinkSprite();

            if (!linkPlayer.isPaused)
            {
                ProjectilesCommand.Instance.Update(gameTime);
                if (Key.Equals("R"))
                {
                    linkPlayer.Reset();
                }

                else if (Key.Equals("E"))
                {
                    linkPlayer.IsDamaged = true;
            }
            else if (Key.Equals("D9") || Key.Equals("NumPad9"))
            {
                linkPlayer.LargeShield = true;
            }
            if (!linkPlayer.IsAttacking)
            {
                if (Key.Equals("B"))
                {
                    linkPlayer.IsAttacking = true;
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsSecondAttack = true;
                }
                    //else if (linkPlayer.itemsPlacedByLink.Count == 0)

                    //{
                    //    linkPlayer.LargeShield = true;
                    //}
                    if (!linkPlayer.IsAttacking)
                    {
                        if (linkPlayer.itemsPlacedByLink.Count == 0)
                        {
                            if ((Key.Equals("N") || (Key.Equals("Z"))))
                            {
                                linkPlayer.IsAttacking = true;
                                linkPlayer.IsStopped = false;
                                linkPlayer.IsSecondAttack = false;
                            }
                            else
                            {
                                foreach (IItems item in linkPlayer.itemsPlacedByLink)
                                {
                                    if (item.IsExpired)
                                    {
                                        if ((Key.Equals("N") || (Key.Equals("Z"))))
                                        {
                                            linkPlayer.IsAttacking = true;
                                            linkPlayer.IsStopped = false;
                                            linkPlayer.IsSecondAttack = false;
                                        }
                                    }
                                }

                            }
                            if ((Key.Equals("A")) || (Key.Equals("Left")))
                            {
                                linkPlayer.IsStopped = false;
                                linkPlayer.IsAttacking = false;
                                linkPlayer.MovingLeft();
                            }
                            else if ((Key.Equals("D")) || (Key.Equals("Right")))
                            {
                                linkPlayer.IsStopped = false;
                                linkPlayer.IsAttacking = false;
                                linkPlayer.MovingRight();
                            }
                            else if ((Key.Equals("W")) || (Key.Equals("Up")))
                            {
                                linkPlayer.IsStopped = false;
                                linkPlayer.IsAttacking = false;
                                linkPlayer.MovingUp();
                            }
                            else if ((Key.Equals("S")) || (Key.Equals("Down")))
                            {
                                linkPlayer.IsStopped = false;
                                linkPlayer.IsAttacking = false;
                                linkPlayer.MovingDown();
                            }
                            else if ((Key.Equals("D0")) || (Key.Equals("NumPad0")))
                            {
                                linkPlayer.CurrentWeapon = ItemForLink.Shield;
                            }
                        }
                    }
                }
                linkPlayer.Update(gameTime);
            }
        }
    }
}
