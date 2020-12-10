using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    public class MainMenuCommand : ICommand
    {

        private IScreen Screen;

        private string Action;
        public MainMenuCommand(IScreen screen, string action)
        {
            this.Screen = screen;
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
            if (Action == "Left" || Action == "Right" || Action == "Down" || Action == "Up")
            {
                Screen.Navigate(Action);
            }
            else if (Action == "Back")
            {
                Screen.Back();
            }
            else if (Action == "Enter")
            {
                Screen.Select();
            }
        }

    }
}
