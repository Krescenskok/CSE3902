using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    public class MainMenuCommand : ICommand
    {

        private MainMenu MainScreen;

        private string Action;
        public MainMenuCommand(MainMenu menu, string action)
        {
            this.MainScreen = menu;
            this.Action = action;
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {
        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            if (Action == "Up")
            {
                MainScreen.goUp();
            }
            else if (Action == "Down")
            {
                MainScreen.goDown();
            }
            else if (Action == "Enter")
            {
                MainScreen.select();
            }
        }

    }
}
