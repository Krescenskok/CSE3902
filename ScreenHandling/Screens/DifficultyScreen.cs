using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using Microsoft.Xna.Framework.Content;
using Sprint5.Link;

namespace Sprint5.ScreenHandling
{
    public class DifficultyScreen : IScreen
    {
        private int drawBounds = 0;
        private List<MenuOption> Options = new List<MenuOption>();
        private List<MenuOption> DrawList = new List<MenuOption>();
        private MenuOption Background = new MenuOption(StateId.Difficulty, ScreenName.DifficultyBG);
        private List<MenuOption> Sprites = new List<MenuOption>();
        private Game1 Game;
        private GameTime GameTime;
        private int SelectedIndex = 0;
        public DifficultyScreen()
        {
            Options.Add(new MenuOption(StateId.Difficulty, ScreenName.Navice));
            Options.Add(new MenuOption(StateId.Difficulty, ScreenName.Normal));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.Tough));
            Options.Add(new MenuOption(StateId.Difficulty, ScreenName.Nightmare));
            Options.Add(new MenuOption(StateId.Difficulty, ScreenName.BackSelect));

            DrawList.Add(Background);
            DrawList.Add(new MenuOption(StateId.Difficulty, ScreenName.Back));
            ToggleOption(Options[0]);
        }

        public void Draw(Game1 game, GameTime gameTime)
        {
            Game = game;
            GameTime = gameTime;

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
            Game.State.BackwardSwap();
        }

        public void Select()
        {
            ScreenName currentName = Options[SelectedIndex].Name;
            StateId currentId = Options[SelectedIndex].Id;
            LinkSprite sprite = Game.LinkPlayer.sprite;

            if (currentName == ScreenName.Navice)
            {
                Game.State.Difficulty = DifficultyLevel.Navice;
                DifficultyMultiplier.Instance.SetDifficulty(Game);
                Game.State.Swap(StateId.Mode);
            }
            else
            if (currentName == ScreenName.Normal)
            {
                Game.State.Difficulty = DifficultyLevel.Normal;
                DifficultyMultiplier.Instance.SetDifficulty(Game);
                Game.State.Swap(StateId.Mode);
            }
            else
            if (currentName == ScreenName.Tough)
            {
                Game.State.Difficulty = DifficultyLevel.Tough;
                DifficultyMultiplier.Instance.SetDifficulty(Game);
                Game.State.Swap(StateId.Mode);
            }
            else
            if (currentName == ScreenName.Nightmare)
            {
                Game.State.Difficulty = DifficultyLevel.Nightmare;
                DifficultyMultiplier.Instance.SetDifficulty(Game);
                Game.State.Swap(StateId.Mode);
            }

            else
            {
                Game.State.BackwardSwap();
            }
        }
    }
}