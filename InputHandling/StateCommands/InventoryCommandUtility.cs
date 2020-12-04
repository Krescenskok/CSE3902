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

            state.KeyCommands.Add(Keys.A, new LinkCommand(player, "A"));
            state.KeyCommands.Add(Keys.Left, new LinkCommand(player, "Left"));

            state.KeyCommands.Add(Keys.P, new ResetCommand(player));

            state.KeyCommands.Add(Keys.D, new LinkCommand(player, "D"));
            state.KeyCommands.Add(Keys.Right, new LinkCommand(player, "Right"));
            state.KeyCommands.Add(Keys.W, new LinkCommand(player, "W"));
            state.KeyCommands.Add(Keys.Up, new LinkCommand(player, "Up"));
            state.KeyCommands.Add(Keys.S, new LinkCommand(player, "S"));
            state.KeyCommands.Add(Keys.Down, new LinkCommand(player, "Down"));
            state.KeyCommands.Add(Keys.N, new LinkCommand(player, "N"));
            state.KeyCommands.Add(Keys.B, new LinkCommand(player, "B"));
            state.KeyCommands.Add(Keys.Z, new LinkCommand(player, "Z"));
            state.KeyCommands.Add(Keys.E, new LinkCommand(player, "E"));


            state.KeyCommands.Add(Keys.LeftShift, new LinkCommand(player, "Shift"));
            state.KeyCommands.Add(Keys.R, new ResetCommand(player));

            state.KeyCommands.Add(Keys.Space, new ShowInventoryCommand());
            state.KeyCommands.Add(Keys.I, new ChangeItemCommand(true, player));
            state.KeyCommands.Add(Keys.U, new ChangeItemCommand(false, player));
            state.KeyCommands.Add(Keys.Enter, new ConsumeItemCommand(player));
            state.KeyCommands.Add(Keys.G, new PauseCommand(game, player, "NotDoor"));

            state.KeyCommands.Add(Keys.K, new ChangeDifficultyCommand("Up", game));
            state.KeyCommands.Add(Keys.J, new ChangeDifficultyCommand("Down", game));

            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));

        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state)
        {
            
            state.ButtonCommands.Add(Buttons.RightTrigger, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.LeftTrigger, new ChangeItemCommand(true, player));

            state.ButtonCommands.Add(Buttons.DPadRight, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.DPadLeft, new ChangeItemCommand(false, player));
            state.ButtonCommands.Add(Buttons.DPadUp, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.DPadDown, new ChangeItemCommand(false, player));

            state.ButtonCommands.Add(Buttons.LeftThumbstickUp, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.LeftThumbstickLeft, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.LeftThumbstickDown, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.LeftThumbstickRight, new ChangeItemCommand(true, player));

            state.ButtonCommands.Add(Buttons.Start, new ShowInventoryCommand());

            state.ButtonCommands.Add(Buttons.LeftShoulder, new ChangeItemCommand(true, player));
            state.ButtonCommands.Add(Buttons.LeftShoulder, new ChangeItemCommand(true, player));

        }

        public void populateMouse()
        {

        }
    }
}
