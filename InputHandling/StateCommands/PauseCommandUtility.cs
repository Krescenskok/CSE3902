using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class PauseCommandUtility
    {

        private static readonly PauseCommandUtility instance = new PauseCommandUtility();
        public static PauseCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private PauseCommandUtility()
        {

        }

        public void PopulateKeyboard (Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.KeyCommands.Add(Keys.G, new PauseCommand(game, player, "NotDoor"));
            state.KeyCommands.Add(Keys.Escape, new PauseCommand(game, player, "NotDoor"));
            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));
            state.KeyCommands.Add(Keys.Q, new QuitCommand());

        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.ButtonCommands.Add(Buttons.A, new PauseCommand(game, player, "NotDoor"));
            state.ButtonCommands.Add(Buttons.Start, new PauseCommand(game, player, "NotDoor"));
            state.ButtonCommands.Add(Buttons.Back, new PauseCommand(game, player, "NotDoor"));

            state.ButtonCommands.Add(Buttons.X, new FullScreenCommand(game));
            state.ButtonCommands.Add(Buttons.B, new QuitCommand());

        }

        public void populateMouse()
        {

        }
    }
}
