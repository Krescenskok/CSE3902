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
        private IDictionary<Buttons, ICommand> gamePadCommands = new Dictionary<Buttons, ICommand>();

        private IDictionary<Keys, ICommand> KeyboardCommands = new Dictionary<Keys, ICommand>();
        private List<Buttons> GamePadMovement = new List<Buttons>();
        private List<Keys> KeyboardMovement = new List<Keys>();



        public List<Keys> MovementKeys { get => KeyboardMovement; set => KeyboardMovement = value; }
        public List<Buttons> MovementButtons { get => GamePadMovement; set => GamePadMovement = value; }
        public IDictionary<Buttons, ICommand> ButtonCommands { get => gamePadCommands; set => gamePadCommands = value; }
        public IDictionary<Keys, ICommand> KeyCommands { get => KeyboardCommands; set => KeyboardCommands = value; }

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
