using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class InventoryCommands
    {
        private static readonly InventoryCommands instance = new InventoryCommands();
        public static InventoryCommands Instance
        {
            get
            {
                return instance;
            }
        }
        private IDictionary<Buttons, ICommand> GamePadCommands = new Dictionary<Buttons, ICommand>();

        private IDictionary<Keys, ICommand> KeyboardCommands = new Dictionary<Keys, ICommand>();
        private List<Buttons> GamePadMovement = new List<Buttons>();
        private List<Keys> KeyboardMovement = new List<Keys>();



        public List<Keys> MovementKeys { get => KeyboardMovement; set => KeyboardMovement = value; }
        public IDictionary<Buttons, ICommand> ButtonCommands { get => GamePadCommands; set => GamePadCommands = value; }
        public IDictionary<Keys, ICommand> KeyCommands { get => KeyboardCommands; set => KeyboardCommands = value; }

        private InventoryCommands()
        {

        }
    }
}
