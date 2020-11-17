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
                if (Key.Equals("B"))
                {
                    linkPlayer.IsAttacking = true;
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsSecondAttack = true;
                }

                else if (linkPlayer.itemsPlacedByLink.Count == 0)

                {
                    linkPlayer.LargeShield = true;
                }
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
                else if ((Key.Equals("S")) || (Key.Equals("Down")))
                {
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsAttacking = false;
                    linkPlayer.MovingDown();
                }              



                }

                linkPlayer.Update(gameTime);
            }
            

        }
    }
}
