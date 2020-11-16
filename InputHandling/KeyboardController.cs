using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint4.Items;
using Sprint4.Link;
using Sprint4.Blocks;

namespace Sprint4
{
    public class KeyboardController : IController
    {
        LinkPlayer player;
        KeyboardState prevState;

        int delay = 0;

        //LinkItems item;

        IDictionary<Keys, ICommand> commandsList = new Dictionary<Keys, ICommand>();
        List<Keys> movementKeys = new List<Keys>();

        public KeyboardController(LinkPlayer linkPlayer, Game1 game, SpriteBatch spriteBatch)
        {
            this.player = linkPlayer;

            var state = Keyboard.GetState();
            commandsList.Add(Keys.Q, new QuitCommand());
            commandsList.Add(Keys.A, new LinkCommand(player, "A"));
            commandsList.Add(Keys.Left, new LinkCommand(player, "Left"));
            commandsList.Add(Keys.D, new LinkCommand(player, "D"));
            commandsList.Add(Keys.Right, new LinkCommand(player, "Right"));
            commandsList.Add(Keys.W, new LinkCommand(player, "W"));
            commandsList.Add(Keys.Up, new LinkCommand(player, "Up"));
            commandsList.Add(Keys.S, new LinkCommand(player, "S"));
            commandsList.Add(Keys.Down, new LinkCommand(player, "Down"));
            commandsList.Add(Keys.N, new LinkCommand(player, "N"));
            commandsList.Add(Keys.B, new LinkCommand(player, "B"));
            commandsList.Add(Keys.Z, new LinkCommand(player, "Z"));
            commandsList.Add(Keys.E, new LinkCommand(player, "E"));
            commandsList.Add(Keys.R, new ResetCommand(player));
            commandsList.Add(Keys.Space, new ShowInventoryCommand());
            commandsList.Add(Keys.I, new ChangeItemCommand(true, player));
            commandsList.Add(Keys.U, new ChangeItemCommand(false, player));
            commandsList.Add(Keys.Enter, new ConsumeItemCommand(player));
            commandsList.Add(Keys.G, new PauseCommand());

            movementKeys.Add(Keys.W);
            movementKeys.Add(Keys.A);
            movementKeys.Add(Keys.S);
            movementKeys.Add(Keys.D);
            movementKeys.Add(Keys.Up);
            movementKeys.Add(Keys.Down);
            movementKeys.Add(Keys.Right);
            movementKeys.Add(Keys.Left);


            prevState = Keyboard.GetState();
        }

        public ICommand HandleInput(Game1 game)
        {

            ICommand activeCommand= null;


            var kstate = Keyboard.GetState();
            foreach (KeyValuePair<Keys, ICommand> kvp in commandsList)
            {
                if (kstate.IsKeyDown(kvp.Key))
                {
                    foreach (Keys k in movementKeys)
                    {
                        if (kstate.IsKeyDown(k))
                        {
                            activeCommand = kvp.Value;
                        }

                    }
                    if (kstate.IsKeyDown(kvp.Key) != prevState.IsKeyDown(kvp.Key))
                    {
                        activeCommand = kvp.Value;
                    }

                    if (activeCommand is PauseCommand)
                    {
                        if (delay == 0)
                        {
                            ((PauseCommand)activeCommand).IsPause = !((PauseCommand)activeCommand).IsPause;

                            delay = 20;
                        }
                    }

                }
              
            }
            if (delay > 0)
                delay--;

            prevState = kstate;

            return activeCommand;
        }
    }
}
