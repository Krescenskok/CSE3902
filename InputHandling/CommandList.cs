using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class InputCommands
    {

        private static readonly InputCommands instance = new InputCommands();
        public static InputCommands Instance
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

        public List<Buttons> MovementButtons { get => GamePadMovement; set => GamePadMovement = value; }
        public List<Keys> MovementKeys { get => KeyboardMovement; set => KeyboardMovement = value; }
        public IDictionary<Buttons, ICommand> ButtonCommands { get => GamePadCommands; set => GamePadCommands = value; }
        public IDictionary<Keys, ICommand> Keycommands { get => KeyboardCommands; set => KeyboardCommands = value; }

        private InputCommands()
        {

        }
    }
}
