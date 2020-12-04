using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Sprint5.InputHandling;

namespace Sprint5
{
    public class ChangeBinding
    {
        private static readonly ChangeBinding instance = new ChangeBinding();
        public static ChangeBinding Instance
        {
            get
            {
                return instance;
            }
        }
        private ChangeBinding()
        {

        }

        public void SwapKeys(Keys KeyOld, Keys KeyNew, IStateCommands state)
        {
            if (state.KeyCommands.ContainsKey(KeyNew))
            {
                ICommand newPreviousCommand = state.KeyCommands[KeyNew];
                ICommand oldPreviousCommand = state.KeyCommands[KeyOld];
                state.KeyCommands.Remove(KeyNew);
                state.KeyCommands.Remove(KeyOld);
                state.KeyCommands.Add(KeyOld, newPreviousCommand);
                state.KeyCommands.Add(KeyNew, oldPreviousCommand);
            } else
            {
                ICommand oldPreviousCommand = state.KeyCommands[KeyOld];
                state.KeyCommands.Remove(KeyOld);
                state.KeyCommands.Add(KeyNew, oldPreviousCommand);
            }

            if (state.MovementKeys.Contains(KeyNew) && !state.MovementKeys.Contains(KeyOld))
            {

                state.MovementKeys.Remove(KeyNew);
                state.MovementKeys.Add(KeyOld);

            } else if (state.MovementKeys.Contains(KeyOld) && !state.MovementKeys.Contains(KeyNew))
            {
                state.MovementKeys.Remove(KeyOld);
                state.MovementKeys.Add(KeyNew);
            }
        }

        public void SwapButtons(Buttons ButtonOld, Buttons ButtonNew, IStateCommands state)
        {
            if (state.ButtonCommands.ContainsKey(ButtonNew))
            {
                ICommand newPreviousCommand = state.ButtonCommands[ButtonNew];
                ICommand oldPreviousCommand = state.ButtonCommands[ButtonOld];
                state.ButtonCommands.Remove(ButtonNew);
                state.ButtonCommands.Remove(ButtonOld);
                state.ButtonCommands.Add(ButtonOld, newPreviousCommand);
                state.ButtonCommands.Add(ButtonNew, oldPreviousCommand);
            }
            else
            {
                ICommand oldPreviousCommand = state.ButtonCommands[ButtonOld];
                state.ButtonCommands.Remove(ButtonOld);
                state.ButtonCommands.Add(ButtonNew, oldPreviousCommand);
            }

            if (state.MovementButtons.Contains(ButtonNew) && !state.MovementButtons.Contains(ButtonOld))
            {

                state.MovementButtons.Remove(ButtonNew);
                state.MovementButtons.Add(ButtonOld);

            }
            else if (state.MovementButtons.Contains(ButtonOld) && !state.MovementButtons.Contains(ButtonNew))
            {
                state.MovementButtons.Remove(ButtonOld);
                state.MovementButtons.Add(ButtonNew);
            }
        }


    }
}
