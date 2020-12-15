using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;
using Sprint5.GamePadVibration;
using Sprint5.InputHandling;

namespace Sprint5
{
    public class GamePadController : IController
    {
        private LinkPlayer Player;
        private GamePadState PrevState;
        private GamePadState State;
        private Game1 Game;

        private IDictionary<Buttons, ICommand> CommandsList = new Dictionary<Buttons, ICommand>();
        private List<Buttons> MovementButtons = new List<Buttons>();

        public GamePadController(Game1 game)
        {
            this.Player = game.LinkPlayer;
            this.Game = game;

            GamePadCapabilities Capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            State = new GamePadState();

            if (Capabilities.IsConnected && Capabilities.GamePadType == GamePadType.GamePad)
            {
                State = GamePad.GetState(PlayerIndex.One);
                PrevState = GamePad.GetState(PlayerIndex.One);
            }
        }
        private void StateControl(Game1 game)
        {

            if (game.State.Current.Id == StateId.Gameplay)
            {
                CommandsList = GamePlayCommands.Instance.ButtonCommands;
                MovementButtons = GamePlayCommands.Instance.MovementButtons;
            }
            else if (game.State.Current.Id == StateId.Inventory)
            {
                CommandsList = InventoryCommands.Instance.ButtonCommands;
                MovementButtons = InventoryCommands.Instance.MovementButtons;
            }
            else if (game.State.Current.Id == StateId.Transition)
            {
                CommandsList = new Dictionary<Buttons, ICommand>();
                MovementButtons = new List<Buttons>();
            }
            else
            {
                CommandsList = MenuCommands.Instance.ButtonCommands;
                MovementButtons = MenuCommands.Instance.MovementButtons;
            }
        }

        public void HandleInput(Game1 game)
        {
            StateControl(game);

            ICommand ActiveCommand = null;

            GamePadCapabilities Capabilities = GamePad.GetCapabilities(PlayerIndex.One);

            if (Capabilities.IsConnected && Capabilities.GamePadType == GamePadType.GamePad)
            {
                State = GamePad.GetState(PlayerIndex.One);

                GamePadVibrate.Instance.Update(game);

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
                    }
                }
                PrevState = State;
            }

            game.ActiveCommand = ActiveCommand;
        }
        public Keys getKey()
        {
            return Keys.Zoom;
        }
    }
}
