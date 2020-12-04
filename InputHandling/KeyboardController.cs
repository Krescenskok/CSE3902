using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;
using Sprint5.InputHandling;

namespace Sprint5
{
    public class KeyboardController : IController
    {
        MainMenuCommand mainCommand;
        private LinkPlayer Player;
        private KeyboardState PrevState;
        private KeyboardState State;
        private Game1 Game;

        private IDictionary<Keys, ICommand> CommandsList = new Dictionary<Keys, ICommand>();
        private List<Keys> MovementKeys = new List<Keys>();

        public KeyboardController(Game1 game)
        {
            this.Player = game.LinkPlayer;
            this.Game = game;

            State = Keyboard.GetState();
            PrevState = Keyboard.GetState();
        }

        private void StateControl(Game1 game)
        {
            if (game.State.Id == StateId.Gameplay)
            {
                CommandsList = GamePlayCommands.Instance.KeyCommands;
                MovementKeys = GamePlayCommands.Instance.MovementKeys;
            } else
            {
                CommandsList = MenuCommands.Instance.KeyCommands;
                MovementKeys = MenuCommands.Instance.MovementKeys;
            }
        }

        public void HandleInput(Game1 game)
        {
            StateControl(game);
            
            ICommand ActiveCommand= null;

            State = Keyboard.GetState();

            foreach (KeyValuePair<Keys, ICommand> Pair in CommandsList)
            {
                if (State.IsKeyDown(Pair.Key))
                {
                    foreach (Keys k in MovementKeys)
                    {
                        if (State.IsKeyDown(k))
                        {
                            ActiveCommand = Pair.Value;
                        }

                    }
                    if (State.IsKeyDown(Pair.Key) != PrevState.IsKeyDown(Pair.Key))
                    {
                        ActiveCommand = Pair.Value;
                    }
                }
            }

            PrevState = State;

            if (Game.State.Id == StateId.GameOver)
            {
                if (!(ActiveCommand is ResetCommand) && !(ActiveCommand is QuitCommand))
                {
                    ActiveCommand = null;
                }  
            }
            game.ActiveCommand = ActiveCommand;
        }
    }
}
