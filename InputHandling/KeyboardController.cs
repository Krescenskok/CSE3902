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
    public class KeyboardController : IController
    {
        private LinkPlayer Player;
        private KeyboardState PrevState;
        private KeyboardState State;
        private Game1 Game;

        private IDictionary<Keys, ICommand> CommandsList = new Dictionary<Keys, ICommand>();
        private List<Keys> MovementKeys = new List<Keys>();

        public KeyboardController(LinkPlayer linkPlayer, Game1 game, SpriteBatch spriteBatch)
        {
            this.Player = linkPlayer;
            this.Game = game;

            var state = Keyboard.GetState();
            CommandsList.Add(Keys.Q, new QuitCommand());

            CommandsList.Add(Keys.A, new LinkCommand(Player, "A"));
            CommandsList.Add(Keys.Left, new LinkCommand(Player, "Left"));

            CommandsList.Add(Keys.P, new ResetCommand(Player));
     
            CommandsList.Add(Keys.D, new LinkCommand(Player, "D"));
            CommandsList.Add(Keys.Right, new LinkCommand(Player, "Right"));
            CommandsList.Add(Keys.W, new LinkCommand(Player, "W"));
            CommandsList.Add(Keys.Up, new LinkCommand(Player, "Up"));
            CommandsList.Add(Keys.S, new LinkCommand(Player, "S"));
            CommandsList.Add(Keys.Down, new LinkCommand(Player, "Down"));
            CommandsList.Add(Keys.N, new LinkCommand(Player, "N"));
            CommandsList.Add(Keys.B, new LinkCommand(Player, "B"));
            CommandsList.Add(Keys.Z, new LinkCommand(Player, "Z"));
            CommandsList.Add(Keys.E, new LinkCommand(Player, "E"));

            CommandsList.Add(Keys.D0, new LinkCommand(Player, "D0"));
            CommandsList.Add(Keys.NumPad0, new LinkCommand(Player, "NumPad0"));
            CommandsList.Add(Keys.D1, new LinkCommand(Player, "D1"));
            CommandsList.Add(Keys.NumPad1, new LinkCommand(Player, "NumPad1"));
            CommandsList.Add(Keys.D2, new LinkCommand(Player, "D2"));
            CommandsList.Add(Keys.NumPad2, new LinkCommand(Player, "NumPad2"));
            CommandsList.Add(Keys.D3, new LinkCommand(Player, "D3"));
            CommandsList.Add(Keys.NumPad3, new LinkCommand(Player, "NumPad3"));
            CommandsList.Add(Keys.D4, new LinkCommand(Player, "D4"));
            CommandsList.Add(Keys.NumPad4, new LinkCommand(Player, "NumPad4"));
            CommandsList.Add(Keys.D5, new LinkCommand(Player, "D5"));
            CommandsList.Add(Keys.NumPad5, new LinkCommand(Player, "NumPad5"));
            CommandsList.Add(Keys.D6, new LinkCommand(Player, "D6"));
            CommandsList.Add(Keys.NumPad6, new LinkCommand(Player, "NumPad6"));
            CommandsList.Add(Keys.D7, new LinkCommand(Player, "D7"));
            CommandsList.Add(Keys.NumPad7, new LinkCommand(Player, "NumPad7"));
            CommandsList.Add(Keys.D8, new LinkCommand(Player, "D8"));
            CommandsList.Add(Keys.NumPad8, new LinkCommand(Player, "NumPad8"));
            CommandsList.Add(Keys.D9, new LinkCommand(Player, "D9"));
            CommandsList.Add(Keys.NumPad9, new LinkCommand(Player, "NumPad9"));
            CommandsList.Add(Keys.LeftShift, new LinkCommand(Player, "Shift"));
            CommandsList.Add(Keys.R, new ResetCommand(Player));

            CommandsList.Add(Keys.Space, new ShowInventoryCommand());
            CommandsList.Add(Keys.I, new ChangeItemCommand(true, Player));
            CommandsList.Add(Keys.U, new ChangeItemCommand(false, Player));
            CommandsList.Add(Keys.Enter, new ConsumeItemCommand(Player));
            CommandsList.Add(Keys.G, new PauseCommand(Game, Player, "NotDoor"));

            CommandsList.Add(Keys.K, new ChangeDifficultyCommand("Up", Game));
            CommandsList.Add(Keys.J, new ChangeDifficultyCommand("Down", Game));
            
            CommandsList.Add(Keys.F, new FullScreenCommand(Game));

            MovementKeys.Add(Keys.W);
            MovementKeys.Add(Keys.A);
            MovementKeys.Add(Keys.S);
            MovementKeys.Add(Keys.D);
            MovementKeys.Add(Keys.Up);
            MovementKeys.Add(Keys.Down);
            MovementKeys.Add(Keys.Right);
            MovementKeys.Add(Keys.Left);

            PrevState = Keyboard.GetState();
        }

        public ICommand HandleInput(Game1 game)
        {
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

            if (Game.IsGameOver)
            {
                if (!(ActiveCommand is ResetCommand) && !(ActiveCommand is QuitCommand))
                {
                    ActiveCommand = null;
                }  
            }
            return ActiveCommand;
        }
    }
}
