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

            commandsList.Add(Keys.P, new ResetCommand(player, true));
     



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

            commandsList.Add(Keys.D0, new LinkCommand(player, "D0"));
            commandsList.Add(Keys.NumPad0, new LinkCommand(player, "NumPad0"));
            commandsList.Add(Keys.D1, new LinkCommand(player, "D1"));
            commandsList.Add(Keys.NumPad1, new LinkCommand(player, "NumPad1"));
            commandsList.Add(Keys.D2, new LinkCommand(player, "D2"));
            commandsList.Add(Keys.NumPad2, new LinkCommand(player, "NumPad2"));
            commandsList.Add(Keys.D3, new LinkCommand(player, "D3"));
            commandsList.Add(Keys.NumPad3, new LinkCommand(player, "NumPad3"));
            commandsList.Add(Keys.D4, new LinkCommand(player, "D4"));
            commandsList.Add(Keys.NumPad4, new LinkCommand(player, "NumPad4"));
            commandsList.Add(Keys.D5, new LinkCommand(player, "D5"));
            commandsList.Add(Keys.NumPad5, new LinkCommand(player, "NumPad5"));
            commandsList.Add(Keys.D6, new LinkCommand(player, "D6"));
            commandsList.Add(Keys.NumPad6, new LinkCommand(player, "NumPad6"));
            commandsList.Add(Keys.D7, new LinkCommand(player, "D7"));
            commandsList.Add(Keys.NumPad7, new LinkCommand(player, "NumPad7"));
            commandsList.Add(Keys.D8, new LinkCommand(player, "D8"));
            commandsList.Add(Keys.NumPad8, new LinkCommand(player, "NumPad8"));
            commandsList.Add(Keys.D9, new LinkCommand(player, "D9"));
            commandsList.Add(Keys.NumPad9, new LinkCommand(player, "NumPad9"));
            commandsList.Add(Keys.R, new ResetCommand(player, false));


            commandsList.Add(Keys.Space, new ShowInventoryCommand());
            commandsList.Add(Keys.I, new ChangeItemCommand(true, player));
            commandsList.Add(Keys.U, new ChangeItemCommand(false, player));
            commandsList.Add(Keys.Enter, new ConsumeItemCommand(player));
            commandsList.Add(Keys.F, new FullScreenCommand());
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
                            game.isPaused = !game.isPaused;
                            player.isPaused = !player.isPaused;
                            Sounds.Instance.TogglePause();

                            delay = 20;
                        }
                    }

                    if (activeCommand is FullScreenCommand)
                    {
                        game.switchScreen();
                    }
                }
              
            }
            if (delay > 0)
                delay--;

            prevState = kstate;



            if (game.IsGameOver)
            {
                if (!(activeCommand is ResetCommand) && !(activeCommand is QuitCommand))
                {
                    return null;
                }

                
            }

                

            return activeCommand;
        }
    }
}
