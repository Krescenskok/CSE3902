using Microsoft.Xna.Framework.Input;
using Sprint5.GameStateHandling;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class WinLoseCommandUtility
    {

        private static readonly WinLoseCommandUtility instance = new WinLoseCommandUtility();
        public static WinLoseCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private WinLoseCommandUtility()
        {

        }

        public void PopulateKeyboard (Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {

            state.KeyCommands.Add(Keys.Enter, new ResetCommand(player));
            state.KeyCommands.Add(Keys.E, new MenuNavCommand(game, "E"));
            state.KeyCommands.Add(Keys.F, new MenuNavCommand(game, "F"));
            state.KeyCommands.Add(Keys.Q, new QuitCommand());
        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {

            state.ButtonCommands.Add(Buttons.A, new ResetCommand(player));
            state.ButtonCommands.Add(Buttons.X, new MenuNavCommand(game, "X"));
            state.ButtonCommands.Add(Buttons.Y, new MenuNavCommand(game, "Y"));
            state.ButtonCommands.Add(Buttons.B, new QuitCommand());
        }

        public void populateMouse()
        {

        }
    }
}
