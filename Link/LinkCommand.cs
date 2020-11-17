using System;
using System.Windows.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint4.Items;
using Sprint4.Link;

namespace Sprint4.Link
{
    public class LinkCommand : ICommand
    {

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
            if(!linkPlayer.isPaused)
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
                    if (linkPlayer.itemsPlacedByLink.Count == 0)
                    {
                        if ((Key.Equals("N") || (Key.Equals("Z"))))
                        {
                            System.Diagnostics.Debug.WriteLine("N");

                            linkPlayer.IsAttacking = true;
                            linkPlayer.IsStopped = false;
                        }
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

                    else if ((Key.Equals("D1")) || (Key.Equals("NumPad1")))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.Sword;
                    }

                    else if ((Key.Equals("D2")) || (Key.Equals("NumPad2")))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.MagicalRod;
                    }

                    else if ((Key.Equals("D3")) || (Key.Equals("NumPad3")))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.ArrowBow;
                    }

                    else if ((Key.Equals("D4")) || (Key.Equals("NumPad4")))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.BlueRing;
                    }

                    else if (Key.Equals("D5") || Key.Equals("NumPad5"))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.Boomerang;
                    }

                    else if (Key.Equals("D6") || Key.Equals("NumPad6"))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.BlueCandle;
                    }

                    else if (Key.Equals("D7") || Key.Equals("NumPad7"))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.Bomb;
                    }
                    else if (Key.Equals("D8") || Key.Equals("NumPad8"))
                    {
                        linkPlayer.CurrentWeapon = ItemForLink.Clock;
                    }



                }

                linkPlayer.Update(gameTime);
            }
            

        }
    }
}
