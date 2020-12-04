using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class InventoryCommandUtility
    {

        private static readonly InventoryCommandUtility instance = new InventoryCommandUtility();
        public static InventoryCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private InventoryCommandUtility()
        {

        }

        public void PopulateKeyboard (Game1 game, LinkPlayer player, IStateCommands state)
        {

            state.KeyCommands.Add(Keys.Q, new QuitCommand());

            state.KeyCommands.Add(Keys.W, new ChangeItemCommand(Direction.up, player));
            state.KeyCommands.Add(Keys.A, new ChangeItemCommand(Direction.down, player));
            state.KeyCommands.Add(Keys.S, new ChangeItemCommand(Direction.down, player));
            state.KeyCommands.Add(Keys.D, new ChangeItemCommand(Direction.right, player));

            state.KeyCommands.Add(Keys.Up, new ChangeItemCommand(Direction.up, player));
            state.KeyCommands.Add(Keys.Down, new ChangeItemCommand(Direction.down, player));
            state.KeyCommands.Add(Keys.Left, new ChangeItemCommand(Direction.left, player));
            state.KeyCommands.Add(Keys.Right, new ChangeItemCommand(Direction.right, player));

            state.KeyCommands.Add(Keys.R, new ResetCommand(player));

            state.KeyCommands.Add(Keys.Space, new ShowInventoryCommand());

            state.KeyCommands.Add(Keys.I, new ChangeItemCommand(Direction.right, player));
            state.KeyCommands.Add(Keys.U, new ChangeItemCommand(Direction.left, player));

            state.KeyCommands.Add(Keys.Enter, new ConsumeItemCommand(player));

            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));

        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state)
        {
            
            state.ButtonCommands.Add(Buttons.RightTrigger, new ChangeItemCommand(Direction.right, player));
            state.ButtonCommands.Add(Buttons.LeftTrigger, new ChangeItemCommand(Direction.left, player));

            state.ButtonCommands.Add(Buttons.DPadRight, new ChangeItemCommand(Direction.right, player));
            state.ButtonCommands.Add(Buttons.DPadLeft, new ChangeItemCommand(Direction.left, player));
            state.ButtonCommands.Add(Buttons.DPadUp, new ChangeItemCommand(Direction.up, player));
            state.ButtonCommands.Add(Buttons.DPadDown, new ChangeItemCommand(Direction.down, player));

            state.ButtonCommands.Add(Buttons.LeftThumbstickUp, new ChangeItemCommand(Direction.up, player));
            state.ButtonCommands.Add(Buttons.LeftThumbstickLeft, new ChangeItemCommand(Direction.left, player));
            state.ButtonCommands.Add(Buttons.LeftThumbstickDown, new ChangeItemCommand(Direction.down, player));
            state.ButtonCommands.Add(Buttons.LeftThumbstickRight, new ChangeItemCommand(Direction.right, player));
            state.ButtonCommands.Add(Buttons.Start, new ShowInventoryCommand());

            state.ButtonCommands.Add(Buttons.LeftShoulder, new ChangeItemCommand(Direction.left, player));
            state.ButtonCommands.Add(Buttons.RightShoulder, new ChangeItemCommand(Direction.right, player));

        }

        public void populateMouse()
        {

        }
    }
}
