using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint3.Items;
using Sprint3.Link;
using Sprint3.Blocks;

namespace Sprint3
{
    public class KeyboardController : IController
    {
        LinkPlayer player;


        public Items.LinkItems items;

        //LinkItems item;

        IDictionary<Keys, ICommand> commandsList = new Dictionary<Keys, ICommand>();

        public KeyboardController(LinkPlayer linkPlayer, Game1 game, SpriteBatch spriteBatch)
        {
            this.player = linkPlayer;

            var state = Keyboard.GetState();
            commandsList.Add(Keys.U, new ItemsCommand(spriteBatch, game.items, true, false));
            commandsList.Add(Keys.I, new ItemsCommand(spriteBatch, game.items, true, true));
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
            commandsList.Add(Keys.Z, new LinkCommand(player, "Z"));
            commandsList.Add(Keys.E, new LinkCommand(player, "E"));
            commandsList.Add(Keys.D1, new LinkCommand(player, "D1"));
            commandsList.Add(Keys.NumPad1, new LinkCommand(player, "NumPad1"));
            commandsList.Add(Keys.D2, new LinkCommand(player, "D2"));
            commandsList.Add(Keys.NumPad2, new LinkCommand(player, "NumPad2"));
            commandsList.Add(Keys.D3, new LinkCommand(player, "D3"));
            commandsList.Add(Keys.NumPad3, new LinkCommand(player, "NumPad3"));
            commandsList.Add(Keys.R, new ResetCommand(player, game.items, game.blocks));
            commandsList.Add(Keys.P, new DisplayNextEnemy(Keys.P));
            commandsList.Add(Keys.O, new DisplayPreviousEnemy(Keys.O));
            commandsList.Add(Keys.T, new BlocksCommand(spriteBatch, game.blocks, true, false));
            commandsList.Add(Keys.Y, new BlocksCommand(spriteBatch, game.blocks, true, true));


        }

        public ICommand HandleInput(Game1 game)
        {

            ICommand activeCommand= null;


            var kstate = Keyboard.GetState();
            foreach (KeyValuePair<Keys, ICommand> kvp in commandsList)
            {
                if (kstate.IsKeyDown(kvp.Key))
                {

                        activeCommand = kvp.Value;
                    

                }
            }


            return activeCommand;
        }
    }
}
