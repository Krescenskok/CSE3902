using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class MenuCommands : IStateCommands
    {
        private static readonly MenuCommands instance = new MenuCommands();
        public static MenuCommands Instance
        {
            get
            {
                return instance;
            }
        }
        public List<Keys> MovementKeys { get; set; } = new List<Keys>();
        public List<Buttons> MovementButtons { get; set; } = new List<Buttons>();
        public IDictionary<Buttons, ICommand> ButtonCommands { get; set; } = new Dictionary<Buttons, ICommand>();
        public IDictionary<Keys, ICommand> KeyCommands { get; set; } = new Dictionary<Keys, ICommand>();

        private MenuCommands()
        {

        }

        public void LoadCommands(Game1 game)
        {
            MenuCommandUtility.Instance.PopulateKeyboard(game, game.LinkPlayer, this, game.mainScreen);
            MenuCommandUtility.Instance.PopulateGamePad(game, game.LinkPlayer, this, game.mainScreen);
            
        }
    }
}
