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
        public MainMenuCommand(Game1 game, string action)
        {
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
            this.Screen = (game as Game1).State.Current.Screen;
            if (Action == "Left" || Action == "Right" || Action == "Down" || Action == "Up")
            {
                Sounds.Instance.Play("UI_Switch");
                Screen.Navigate(Action);
            }
            else if (Action == "Back")
            {
                Sounds.Instance.Play("UI_Enter");
                Screen.Back();
            }
            else if (Action == "Enter")
            {
                Sounds.Instance.Play("UI_Enter");
                Screen.Select();
            }
        }

    }
}
