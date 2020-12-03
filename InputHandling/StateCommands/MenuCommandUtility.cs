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

            state.KeyCommands.Add(Keys.A, new MainMenuCommand(menu, "Left"));
            state.KeyCommands.Add(Keys.Left, new MainMenuCommand(menu, "Left"));
            state.KeyCommands.Add(Keys.D, new MainMenuCommand(menu, "Right"));
            state.KeyCommands.Add(Keys.Right, new MainMenuCommand(menu, "Right"));
            state.KeyCommands.Add(Keys.W, new MainMenuCommand(menu, "Up"));
            state.KeyCommands.Add(Keys.Up, new MainMenuCommand(menu, "Up"));
            state.KeyCommands.Add(Keys.S, new MainMenuCommand(menu, "Down"));
            state.KeyCommands.Add(Keys.Down, new MainMenuCommand(menu, "Down"));
            state.KeyCommands.Add(Keys.Space, new MainMenuCommand(menu, "Enter"));
            state.KeyCommands.Add(Keys.Enter, new MainMenuCommand(menu, "Enter"));
            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));


        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.ButtonCommands.Add(Buttons.LeftThumbstickUp, new MainMenuCommand(menu, "Up"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickDown, new MainMenuCommand(menu, "Down"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickLeft, new MainMenuCommand(menu, "Left"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickRight, new MainMenuCommand(menu, "Right"));
            state.ButtonCommands.Add(Buttons.DPadUp, new MainMenuCommand(menu, "Up"));
            state.ButtonCommands.Add(Buttons.DPadDown, new MainMenuCommand(menu, "Down"));
            state.ButtonCommands.Add(Buttons.DPadLeft, new MainMenuCommand(menu, "Left"));
            state.ButtonCommands.Add(Buttons.DPadRight, new MainMenuCommand(menu, "Right"));
            state.ButtonCommands.Add(Buttons.A, new MainMenuCommand(menu, "Enter"));
            state.ButtonCommands.Add(Buttons.B, new MainMenuCommand(menu, "Back"));

            state.ButtonCommands.Add(Buttons.LeftTrigger, new MainMenuCommand(menu, "Left"));
            state.ButtonCommands.Add(Buttons.RightTrigger, new MainMenuCommand(menu, "Right"));

            state.ButtonCommands.Add(Buttons.Start, new MainMenuCommand(menu, "Enter"));

            state.ButtonCommands.Add(Buttons.LeftShoulder, new MainMenuCommand(menu, "Left"));
            state.ButtonCommands.Add(Buttons.RightShoulder, new MainMenuCommand(menu, "Right"));

            state.ButtonCommands.Add(Buttons.Back, new MainMenuCommand(menu, "Enter"));

        }

        public void populateMouse()
        {

        }
    }
}
