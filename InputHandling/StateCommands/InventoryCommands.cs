using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class InventoryCommands : IStateCommands
    {
        private static readonly InventoryCommands instance = new InventoryCommands();
        public static InventoryCommands Instance
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

        private InventoryCommands()
        {
        }

        public void LoadCommands(Game1 game)
        {
            InventoryCommandUtility.Instance.PopulateKeyboard(game, game.LinkPlayer, this);
            InventoryCommandUtility.Instance.PopulateGamePad(game, game.LinkPlayer, this);

        }
    }
}
