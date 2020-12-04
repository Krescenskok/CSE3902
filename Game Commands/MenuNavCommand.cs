using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
 using Sprint5.Blocks;
using Sprint5.Items;
using Sprint5.EnemyAndNPC;
using System.Diagnostics;
using Sprint5.GameStateHandling;

namespace Sprint5
{

    public class MenuNavCommand : ICommand
    {
        private String Input;
        private IGameStates Prev;
        private IGameStates Dest;

        public MenuNavCommand(Game1 game, String input )
        {
            Debug.WriteLine("Created");
            this.Input = input;
        }

        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            if ((Input == "E" || Input == "X") && ((game as Game1).State.Id == StateId.Win || (game as Game1).State.Id == StateId.GameOver))
            {
                (game as Game1).State.Previous = (game as Game1).State.Current;
                this.Dest = CreditsState.Instance;

            }
            else if (Input == "F" || Input == "Y")
            {
                if ((game as Game1).State.Id == StateId.GameOver || (game as Game1).State.Id == StateId.Win)
                {
                    this.Dest = StatsState.Instance;
                }
                else
                {
                    this.Dest = (game as Game1).State.Previous;
                    Debug.WriteLine(this.Dest.Id);
                }
            }

            (game as Game1).State.Swap(Dest);
        }
    }
}
