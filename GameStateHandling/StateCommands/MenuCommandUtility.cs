using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class MenuCommandUtility
    {

        private static readonly MenuCommandUtility instance = new MenuCommandUtility();
        public static MenuCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private MenuCommandUtility()
        {

        }

        public void PopulateKeyboard (Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.KeyCommands.Add(Keys.Q, new QuitCommand());

            state.KeyCommands.Add(Keys.A, new MainMenuCommand(game, "Left"));
            state.KeyCommands.Add(Keys.Left, new MainMenuCommand(game, "Left"));
            state.KeyCommands.Add(Keys.D, new MainMenuCommand(game, "Right"));
            state.KeyCommands.Add(Keys.Right, new MainMenuCommand(game, "Right"));
            state.KeyCommands.Add(Keys.W, new MainMenuCommand(game, "Up"));
            state.KeyCommands.Add(Keys.Up, new MainMenuCommand(game, "Up"));
            state.KeyCommands.Add(Keys.S, new MainMenuCommand(game, "Down"));
            state.KeyCommands.Add(Keys.Down, new MainMenuCommand(game, "Down"));
            state.KeyCommands.Add(Keys.Space, new MainMenuCommand(game, "Enter"));
            state.KeyCommands.Add(Keys.Enter, new MainMenuCommand(game, "Enter"));
            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));


        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.ButtonCommands.Add(Buttons.LeftThumbstickUp, new MainMenuCommand(game, "Up"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickDown, new MainMenuCommand(game, "Down"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickLeft, new MainMenuCommand(game, "Left"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickRight, new MainMenuCommand(game, "Right"));
            state.ButtonCommands.Add(Buttons.DPadUp, new MainMenuCommand(game, "Up"));
            state.ButtonCommands.Add(Buttons.DPadDown, new MainMenuCommand(game, "Down"));
            state.ButtonCommands.Add(Buttons.DPadLeft, new MainMenuCommand(game, "Left"));
            state.ButtonCommands.Add(Buttons.DPadRight, new MainMenuCommand(game, "Right"));
            state.ButtonCommands.Add(Buttons.A, new MainMenuCommand(game, "Enter"));
            state.ButtonCommands.Add(Buttons.B, new MainMenuCommand(game, "Back"));

            state.ButtonCommands.Add(Buttons.LeftTrigger, new MainMenuCommand(game, "Left"));
            state.ButtonCommands.Add(Buttons.RightTrigger, new MainMenuCommand(game, "Right"));

            state.ButtonCommands.Add(Buttons.Start, new MainMenuCommand(game, "Enter"));

            state.ButtonCommands.Add(Buttons.LeftShoulder, new MainMenuCommand(game, "Left"));
            state.ButtonCommands.Add(Buttons.RightShoulder, new MainMenuCommand(game, "Right"));

            state.ButtonCommands.Add(Buttons.Back, new MainMenuCommand(game, "Enter"));

        }

        public void populateMouse()
        {

        }
    }
}
