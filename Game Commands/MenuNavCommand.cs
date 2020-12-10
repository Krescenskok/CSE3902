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
        private StateId Dest;

        public MenuNavCommand(Game1 game, StateId Destination)
        {
            Debug.WriteLine("Swap");
            this.Dest = Destination;
        }

        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            (game as Game1).State.Swap(Dest);

        }
    }
}
