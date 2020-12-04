using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public interface IStateCommands
    {

        public List<Keys> MovementKeys { get; set; }
        public List<Buttons> MovementButtons { get; set; }
        public IDictionary<Buttons, ICommand> ButtonCommands { get; set; }
        public IDictionary<Keys, ICommand> KeyCommands { get; set; }
    }
}
