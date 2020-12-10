using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class WinLoseCommands : IStateCommands
    {
        private static readonly WinLoseCommands instance = new WinLoseCommands();
        public static WinLoseCommands Instance
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

        private WinLoseCommands()
        {

        }

        public void LoadCommands(Game1 game)
        {
            WinLoseCommandUtility.Instance.PopulateKeyboard(game, game.LinkPlayer, this, game.mainScreen);
            WinLoseCommandUtility.Instance.PopulateGamePad(game, game.LinkPlayer, this, game.mainScreen);
            
        }
    }
}
