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
    public class OptionsScreen : IScreen
    {
        private int drawBounds = 0;
        public MenuOption Background { get; set; }

        public List<MenuOption> Options { get; set; }

        public List<MenuOption> Sprites { get; set; }

        private int SelectedIndex = 0;

        private Game1 Game;
        public List<MenuOption> DrawList { get; set; }

        public OptionsScreen()
        {
            Options = new List<MenuOption>();
            DrawList = new List<MenuOption>();
            Background = new MenuOption(StateId.Options, ScreenName.OptionsBG);

            Options.Add(new MenuOption(StateId.Sound, ScreenName.OptionsSoundSelect));
            Options.Add(new MenuOption(StateId.Controls, ScreenName.OptionsControlSelect));
            Options.Add(new MenuOption(StateId.KeyBinding, ScreenName.OptionsKeyBindingsSelect));
            Options.Add(new MenuOption(StateId.Options, ScreenName.OptionsFullScreenSelect));
            Options.Add(new MenuOption(StateId.Options, ScreenName.BackSelect));
            Options.Add(new MenuOption(StateId.Options, ScreenName.BackEsc));
            Options.Add(new MenuOption(StateId.Options, ScreenName.BackB));

            DrawList.Add(Background);
            ToggleOption(Options[0]);
        }

        public void Draw(Game1 game, GameTime gameTime)
        {

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

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
            Game.State.Swap(Game.State.Previous.Id);
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
                Game.State.Swap(Game.State.Previous.Id);
            }
        }
    }
}