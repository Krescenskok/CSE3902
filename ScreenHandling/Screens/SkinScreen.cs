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
    public class SkinScreen : IScreen
    {
        private int drawBounds = 0;
        private List<MenuOption> Options = new List<MenuOption>();
        private List<MenuOption> DrawList = new List<MenuOption>();
        private MenuOption Background = new MenuOption(StateId.Skin, ScreenName.SkinBG);
        private List<MenuOption> Sprites = new List<MenuOption>();
        private Game1 Game;
        private GameTime GameTime;
        private int SelectedIndex = 0;
        public SkinScreen()
        {
            Options.Add(new MenuOption(StateId.Skin, ScreenName.ClassicLink));
            Options.Add(new MenuOption(StateId.Skin, ScreenName.Yellow));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.Pink));
            Options.Add(new MenuOption(StateId.Skin, ScreenName.Red));
            Options.Add(new MenuOption(StateId.Skin, ScreenName.Blue));
            Options.Add(new MenuOption(StateId.Skin, ScreenName.Teal));
            Options.Add(new MenuOption(StateId.Skin, ScreenName.BackSelect));

            DrawList.Add(Background);
            DrawList.Add(new MenuOption(StateId.Skin, ScreenName.Back));
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

            if (currentName == ScreenName.ClassicLink)
            {
                Game.LinkPlayer.sprite = SpriteFactory.Instance.CreateLinkSprite();
                Game.State.Swap(StateId.Difficulty);
            }
            else
            if (currentName == ScreenName.Pink)
            {
                Game.LinkPlayer.sprite = SpriteFactory.Instance.CreatePinkLinkSprite();
                Game.State.Swap(StateId.Difficulty);
            }
            else
            if (currentName == ScreenName.Red)
            {
                Game.LinkPlayer.sprite = SpriteFactory.Instance.CreateRedLinkSprite();
                Game.State.Swap(StateId.Difficulty);
            }
            else
            if (currentName == ScreenName.Blue)
            {
                Game.LinkPlayer.sprite = SpriteFactory.Instance.CreateBlueLinkSprite();
                Game.State.Swap(StateId.Difficulty);
            }
            else
            if (currentName == ScreenName.Teal)
            {
                Game.LinkPlayer.sprite = SpriteFactory.Instance.CreateTealLinkSprite();
                Game.State.Swap(StateId.Difficulty);
            }
            else
            if (currentName == ScreenName.Yellow)
            {
                Game.LinkPlayer.sprite = SpriteFactory.Instance.CreateYellowLinkSprite();
                Game.State.Swap(StateId.Difficulty);

            } else
            {
                Game.State.BackwardSwap();
            }
        }
    }
}