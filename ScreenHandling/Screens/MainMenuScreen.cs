using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using Microsoft.Xna.Framework.Content;

namespace Sprint5.ScreenHandling
{
    public class MainMenuScreen : IScreen
    {
        private int drawBounds = 0;
        private List<MenuOption> Options = new List<MenuOption>();
        private List<MenuOption> DrawList = new List<MenuOption>();
        private MenuOption Background = new MenuOption(StateId.Controls, ScreenName.MainBG);
        private List<MenuOption> Sprites = new List<MenuOption>();
        private Game1 Game;
        private int SelectedIndex = 0;
        public MainMenuScreen()
        {
            Options.Add(new MenuOption(StateId.Skin, ScreenName.MainNewGame));
            Options.Add(new MenuOption(StateId.Options, ScreenName.MainOptionsSelect));
            Options.Add(new MenuOption(StateId.Credits, ScreenName.MainCreditsSelect));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.MainQuitSelect));
            Sprites.Add(new MenuOption(StateId.MainMenu, ScreenName.BackEsc));
            Sprites.Add(new MenuOption(StateId.MainMenu, ScreenName.BackB));

            DrawList.Add(Background);
            ToggleOption(Options[0]);
        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            Game = game;

            game.Spritebatch.Begin();

            game.GraphicsDevice.Viewport = game.Camera.EntireView;

            foreach (MenuOption option in DrawList)
            {
                ScreenSprite currentSprite = ScreenSpriteMap.Instance.GetSprite(option.Name);

                game.Spritebatch.Draw(currentSprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);
            }

            game.Spritebatch.End();
        }

        public void ToggleOption(MenuOption option)
        {
            if (DrawList.Contains(option))
            {
                DrawList.Remove(option);
            }
            else
            {
                DrawList.Add(option);
            }
        }

        public void Navigate(string action)
        {
            if (action == "Up")
            {
                if (SelectedIndex != 0)
                {
                    ToggleOption(Options[SelectedIndex]);
                    SelectedIndex--;
                    ToggleOption(Options[SelectedIndex]);
                }
            }
            else if (action == "Down")
            {
                if (SelectedIndex != (Options.Count - 1))
                {
                    ToggleOption(Options[SelectedIndex]);
                    SelectedIndex++;
                    ToggleOption(Options[SelectedIndex]);
                }
            }
        }

        public void Back()
        {
            Game.Exit();
        }

        public void Select()
        {
            ScreenName currentName = Options[SelectedIndex].Name;
            StateId currentId = Options[SelectedIndex].Id;
            if (Game.State.Current.Id != currentId)
            {
                Game.State.Swap(currentId);
            }
            else
            {
                Game.Exit();
            }
        }
    }
}