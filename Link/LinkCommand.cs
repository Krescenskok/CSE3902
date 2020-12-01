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

        LinkPlayer linkPlayer;
        IItems item;
        String Key;

        bool previouslyAttacking;

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

            if(linkPlayer.IsAttacking && !previouslyAttacking)
            {
                linkPlayer.weaponCollider.TurnOn(linkPlayer.currentDirection);
                previouslyAttacking = true;
            }
            else if(!linkPlayer.IsAttacking && previouslyAttacking)
            {
                
                previouslyAttacking = false;
            }

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
                if (!linkPlayer.IsAttacking)
                {
                        if (Key.Equals("N"))
                        {
                            linkPlayer.IsAttacking = true;
                            linkPlayer.IsStopped = false;
                            linkPlayer.IsSecondAttack = false;

                        }
                        else if (Key.Equals("B"))
                        {
                            linkPlayer.IsAttacking = true;
                            linkPlayer.IsStopped = false;
                            linkPlayer.IsSecondAttack = true;

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
