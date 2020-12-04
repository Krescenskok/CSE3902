using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class GamePlayCommandUtility
    {

        private static readonly GamePlayCommandUtility instance = new GamePlayCommandUtility();
        public static GamePlayCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private GamePlayCommandUtility()
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
            state.KeyCommands.Add(Keys.Enter, new ConsumeItemCommand(player));
            state.KeyCommands.Add(Keys.G, new PauseCommand(game, player, "NotDoor"));

            state.KeyCommands.Add(Keys.K, new ChangeDifficultyCommand("Up", game));
            state.KeyCommands.Add(Keys.J, new ChangeDifficultyCommand("Down", game));

            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));

            state.MovementKeys.Add(Keys.W);
            state.MovementKeys.Add(Keys.A);
            state.MovementKeys.Add(Keys.S);
            state.MovementKeys.Add(Keys.D);
            state.MovementKeys.Add(Keys.Up);
            state.MovementKeys.Add(Keys.Down);
            state.MovementKeys.Add(Keys.Right);
            state.MovementKeys.Add(Keys.Left);
        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state)
        {
            state.ButtonCommands.Add(Buttons.LeftThumbstickUp, new LinkCommand(player, "W"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickLeft, new LinkCommand(player, "A"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickDown, new LinkCommand(player, "S"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickRight, new LinkCommand(player, "D"));

            state.ButtonCommands.Add(Buttons.A, new LinkCommand(player, "N"));
            state.ButtonCommands.Add(Buttons.B, new LinkCommand(player, "B"));

            state.ButtonCommands.Add(Buttons.RightTrigger, new LinkCommand(player, "N"));
            state.ButtonCommands.Add(Buttons.LeftTrigger, new LinkCommand(player, "B"));

            state.ButtonCommands.Add(Buttons.X, new ConsumeItemCommand(player));

            state.ButtonCommands.Add(Buttons.Start, new ShowInventoryCommand());

            state.ButtonCommands.Add(Buttons.LeftStick, new LinkCommand(player, "Shift"));
            state.ButtonCommands.Add(Buttons.LeftShoulder, new LinkCommand(player, "Shift"));

            state.ButtonCommands.Add(Buttons.Back, new PauseCommand(game, player, "NotDoor"));

            state.ButtonCommands.Add(Buttons.BigButton, new ResetCommand(player));

            state.MovementButtons.Add(Buttons.LeftThumbstickDown);
            state.MovementButtons.Add(Buttons.LeftThumbstickLeft);
            state.MovementButtons.Add(Buttons.LeftThumbstickUp);
            state.MovementButtons.Add(Buttons.LeftThumbstickRight);
        }

        public void populateMouse()
        {

        }
    }
}
