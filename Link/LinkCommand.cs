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
            ProjectilesCommand.Instance.Update(gameTime);
            if (Key.Equals("R"))
            {
                linkPlayer.Reset();
            }
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

                else if ((Key.Equals("A")) || (Key.Equals("Left")))
                {
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsAttacking = false;
                    linkPlayer.IsSecondAttack = false;
                    linkPlayer.MovingLeft();

                }
                else if ((Key.Equals("D")) || (Key.Equals("Right")))
                {
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsAttacking = false;
                    linkPlayer.IsSecondAttack = false;
                    linkPlayer.MovingRight();

                }
                else if ((Key.Equals("W")) || (Key.Equals("Up")))
                {
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsAttacking = false;
                    linkPlayer.IsSecondAttack = false;
                    linkPlayer.MovingUp();

                }
                else if ((Key.Equals("S")) || (Key.Equals("Down")))
                {
                    linkPlayer.IsStopped = false;
                    linkPlayer.IsAttacking = false;
                    linkPlayer.IsSecondAttack = false;
                    linkPlayer.MovingDown();
                }
                              


            }

            linkPlayer.Update(gameTime);

        }
    }
}
