﻿using Microsoft.Xna.Framework.Input;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.InputHandling
{
    public class MenuCommandUtility
    {

        private static readonly MenuCommandUtility instance = new MenuCommandUtility();
        public static MenuCommandUtility Instance
        {
            get
            {
                return instance;
            }
        }

        private MenuCommandUtility()
        {

        }

        public void PopulateKeyboard (Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.KeyCommands.Add(Keys.Q, new QuitCommand());

            state.KeyCommands.Add(Keys.A, new MainMenuCommand(game.State.Current.Screen, "Left"));
            state.KeyCommands.Add(Keys.Left, new MainMenuCommand(game.State.Current.Screen, "Left"));
            state.KeyCommands.Add(Keys.D, new MainMenuCommand(game.State.Current.Screen, "Right"));
            state.KeyCommands.Add(Keys.Right, new MainMenuCommand(game.State.Current.Screen, "Right"));
            state.KeyCommands.Add(Keys.W, new MainMenuCommand(game.State.Current.Screen, "Up"));
            state.KeyCommands.Add(Keys.Up, new MainMenuCommand(game.State.Current.Screen, "Up"));
            state.KeyCommands.Add(Keys.S, new MainMenuCommand(game.State.Current.Screen, "Down"));
            state.KeyCommands.Add(Keys.Down, new MainMenuCommand(game.State.Current.Screen, "Down"));
            state.KeyCommands.Add(Keys.Space, new MainMenuCommand(game.State.Current.Screen, "Enter"));
            state.KeyCommands.Add(Keys.Enter, new MainMenuCommand(game.State.Current.Screen, "Enter"));
            state.KeyCommands.Add(Keys.F, new FullScreenCommand(game));


        }

        public void PopulateGamePad(Game1 game, LinkPlayer player, IStateCommands state, MainMenu menu)
        {
            state.ButtonCommands.Add(Buttons.LeftThumbstickUp, new MainMenuCommand(game.State.Current.Screen, "Up"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickDown, new MainMenuCommand(game.State.Current.Screen, "Down"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickLeft, new MainMenuCommand(game.State.Current.Screen, "Left"));
            state.ButtonCommands.Add(Buttons.LeftThumbstickRight, new MainMenuCommand(game.State.Current.Screen, "Right"));
            state.ButtonCommands.Add(Buttons.DPadUp, new MainMenuCommand(game.State.Current.Screen, "Up"));
            state.ButtonCommands.Add(Buttons.DPadDown, new MainMenuCommand(game.State.Current.Screen, "Down"));
            state.ButtonCommands.Add(Buttons.DPadLeft, new MainMenuCommand(game.State.Current.Screen, "Left"));
            state.ButtonCommands.Add(Buttons.DPadRight, new MainMenuCommand(game.State.Current.Screen, "Right"));
            state.ButtonCommands.Add(Buttons.A, new MainMenuCommand(game.State.Current.Screen, "Enter"));
            state.ButtonCommands.Add(Buttons.B, new MainMenuCommand(game.State.Current.Screen, "Back"));

            state.ButtonCommands.Add(Buttons.LeftTrigger, new MainMenuCommand(game.State.Current.Screen, "Left"));
            state.ButtonCommands.Add(Buttons.RightTrigger, new MainMenuCommand(game.State.Current.Screen, "Right"));

            state.ButtonCommands.Add(Buttons.Start, new MainMenuCommand(game.State.Current.Screen, "Enter"));

            state.ButtonCommands.Add(Buttons.LeftShoulder, new MainMenuCommand(game.State.Current.Screen, "Left"));
            state.ButtonCommands.Add(Buttons.RightShoulder, new MainMenuCommand(game.State.Current.Screen, "Right"));

            state.ButtonCommands.Add(Buttons.Back, new MainMenuCommand(game.State.Current.Screen, "Enter"));

        }

        public void populateMouse()
        {

        }
    }
}
