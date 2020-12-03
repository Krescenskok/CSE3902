using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    public class FullScreenCommand : ICommand
    {
        Game1 Game;
        public FullScreenCommand(Game1 game)
        {
            this.Game = game;
        }


        public void DoInit(Game game)
        {
            
        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            this.Game.switchScreen();
        }
    }
}
