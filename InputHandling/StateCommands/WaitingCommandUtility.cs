using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class WaitingCommandUtility
    {

        private static readonly WaitingCommandUtility instance = new WaitingCommandUtility();
        public static WaitingCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private WaitingCommandUtility()
        {

        }

        public void PopulateKeyboard(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.KeyCommands.Add(Keys.Q, new WaitingCommands());
            state.KeyCommands.Add(Keys.W, new WaitingCommands());
            state.KeyCommands.Add(Keys.E, new WaitingCommands());
            state.KeyCommands.Add(Keys.R, new WaitingCommands());
            state.KeyCommands.Add(Keys.T, new WaitingCommands());
            state.KeyCommands.Add(Keys.Y, new WaitingCommands());
            state.KeyCommands.Add(Keys.U, new WaitingCommands());
            state.KeyCommands.Add(Keys.I, new WaitingCommands());
            state.KeyCommands.Add(Keys.O, new WaitingCommands());
            state.KeyCommands.Add(Keys.P, new WaitingCommands());
            state.KeyCommands.Add(Keys.A, new WaitingCommands());
            state.KeyCommands.Add(Keys.S, new WaitingCommands());
            state.KeyCommands.Add(Keys.D, new WaitingCommands());
            state.KeyCommands.Add(Keys.F, new WaitingCommands());
            state.KeyCommands.Add(Keys.G, new WaitingCommands());
            state.KeyCommands.Add(Keys.H, new WaitingCommands());
            state.KeyCommands.Add(Keys.J, new WaitingCommands());
            state.KeyCommands.Add(Keys.K, new WaitingCommands());
            state.KeyCommands.Add(Keys.L, new WaitingCommands());
            state.KeyCommands.Add(Keys.Z, new WaitingCommands());
            state.KeyCommands.Add(Keys.X, new WaitingCommands());
            state.KeyCommands.Add(Keys.C, new WaitingCommands());
            state.KeyCommands.Add(Keys.V, new WaitingCommands());
            state.KeyCommands.Add(Keys.B, new WaitingCommands());
            state.KeyCommands.Add(Keys.N, new WaitingCommands());
            state.KeyCommands.Add(Keys.M, new WaitingCommands());
            state.KeyCommands.Add(Keys.Space, new WaitingCommands());
            state.KeyCommands.Add(Keys.Enter, new WaitingCommands());
            state.KeyCommands.Add(Keys.LeftShift, new WaitingCommands());
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
