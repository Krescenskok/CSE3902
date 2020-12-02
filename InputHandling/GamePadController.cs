using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;

namespace Sprint5
{
    public class GamePadController : IController
    {
        private LinkPlayer Player;
        private GamePadState PrevState;
        private GamePadState State;
        private Game1 Game;

        private int delay = 0;

        //LinkItems item;

        private IDictionary<Buttons, ICommand> CommandsList = new Dictionary<Buttons, ICommand>();
        private List<Buttons> MovementButtons = new List<Buttons>();

        public GamePadController(LinkPlayer linkPlayer, Game1 game, SpriteBatch spriteBatch)
        {
            this.Player = linkPlayer;
            this.Game = game;
            GamePadCapabilities Capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            State = new GamePadState();

            // checks that controller is connected and is an XBOX controller type
            if (Capabilities.IsConnected && Capabilities.GamePadType == GamePadType.GamePad)
            {
                State = GamePad.GetState(PlayerIndex.One);
                PrevState = GamePad.GetState(PlayerIndex.One);
            }

            /*
            CommandsList.Add(Buttons.DPadRight, new LinkCommand(Player, "Right"));
            CommandsList.Add(Buttons.DPadUp, new LinkCommand(Player, "Up"));
            CommandsList.Add(Buttons.DPadDown, new LinkCommand(Player, "Down"));
            CommandsList.Add(Buttons.DPadLeft, new LinkCommand(Player, "Left"));
            */

            CommandsList.Add(Buttons.LeftThumbstickUp, new LinkCommand(Player, "W"));
            CommandsList.Add(Buttons.LeftThumbstickLeft, new LinkCommand(Player, "A"));
            CommandsList.Add(Buttons.LeftThumbstickDown, new LinkCommand(Player, "S"));
            CommandsList.Add(Buttons.LeftThumbstickRight, new LinkCommand(Player, "D"));

            CommandsList.Add(Buttons.A, new LinkCommand(Player, "N"));
            CommandsList.Add(Buttons.B, new LinkCommand(Player, "B"));

            CommandsList.Add(Buttons.DPadRight, new ChangeItemCommand(true, Player));
            CommandsList.Add(Buttons.DPadLeft, new ChangeItemCommand(false, Player));

            CommandsList.Add(Buttons.X, new ConsumeItemCommand(Player));

            CommandsList.Add(Buttons.Start, new ShowInventoryCommand());

            CommandsList.Add(Buttons.LeftStick, new LinkCommand(Player, "Shift"));
            CommandsList.Add(Buttons.LeftShoulder, new LinkCommand(Player, "Shift"));

            CommandsList.Add(Buttons.Back, new PauseCommand(Game, Player,  "NotDoor"));

            CommandsList.Add(Buttons.RightTrigger, new ChangeDifficultyCommand("Up", Game));
            CommandsList.Add(Buttons.LeftTrigger, new ChangeDifficultyCommand("Down", Game));

            MovementButtons.Add(Buttons.LeftThumbstickDown);
            MovementButtons.Add(Buttons.LeftThumbstickLeft);
            MovementButtons.Add(Buttons.LeftThumbstickUp);
            MovementButtons.Add(Buttons.LeftThumbstickRight);
        }

        public ICommand HandleInput(Game1 game)
        {
            ICommand ActiveCommand = null;
            
            // Check the device for Player One
            GamePadCapabilities Capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            // checks that controller is connected and is an XBOX controller type
            if (Capabilities.IsConnected && Capabilities.GamePadType == GamePadType.GamePad)
            {
                // Get the current state of Controller1
                State = GamePad.GetState(PlayerIndex.One);

                foreach (KeyValuePair<Buttons, ICommand> Pair in CommandsList)
                {
                    if (State.IsButtonDown(Pair.Key))
                    {
                        foreach (Buttons b in MovementButtons)
                        {
                            if (State.IsButtonDown(b))
                            {
                                ActiveCommand = Pair.Value;
                            }
                        }
                        if (State.IsButtonDown(Pair.Key) != PrevState.IsButtonDown(Pair.Key))
                        {
                            ActiveCommand = Pair.Value;
                        }
                        //there was a pause delay here but wtf
                    }
                }
                PrevState = State;
            }
            if (game.IsGameOver)
            {
                if (!(ActiveCommand is ResetCommand) && !(ActiveCommand is QuitCommand))
                {
                    return null;
                }  
            }
            return ActiveCommand;
        }
    }
}
