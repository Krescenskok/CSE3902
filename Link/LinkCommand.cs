using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class LinkCommand : ICommand
    {

        LinkPlayer linkPlayer;
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
            linkPlayer.Draw(game, spriteBatch, gameTime );
        }

        public void Update(GameTime gameTime )
        {
            System.Diagnostics.Debug.WriteLine(Key);

            if (Key.Equals("R"))
            {
                linkPlayer.Reset();
            }

            else if (Key.Equals("E"))
            {
            
                linkPlayer.IsDamaged = true;
                
            }
            if (!linkPlayer.IsAttacking)
            {

                if ((Key.Equals("N") || (Key.Equals("Z"))))
                {

                    linkPlayer.IsAttacking = true;
                    linkPlayer.IsStopped = false;

                }

                else if ((Key.Equals("A")) || (Key.Equals("Left")))
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

            }

            linkPlayer.Update(gameTime);

        }
    }
}
