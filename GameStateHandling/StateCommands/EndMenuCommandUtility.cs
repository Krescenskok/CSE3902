using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class EndMenuCommandUtility
    {

        private static readonly EndMenuCommandUtility instance = new EndMenuCommandUtility();
        public static EndMenuCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private EndMenuCommandUtility()
        {

        }

        public void PopulateKeyboard (Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            /*
            state.KeyCommands.Add(Keys.F, new MenuNavCommand(game, "F"));
            state.KeyCommands.Add(Keys.Q, new QuitCommand());
            */
        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {

            /*
            state.ButtonCommands.Add(Buttons.Y, new MenuNavCommand(game, "Y"));
            state.ButtonCommands.Add(Buttons.B, new QuitCommand());
            */

        }

        public void populateMouse()
        {

        }
    }
}
