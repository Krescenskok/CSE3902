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
        public ScreenName Background { get; set; }

        public List<MenuOption> Options { get; set; }
        public List<ScreenName> DrawList { get; set; }

        public MainMenuScreen()
        {
            Options = new List<MenuOption>();
            DrawList = new List<ScreenName>();
            Background = ScreenName.MainBG;

            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.QuitSelect));

            Options.Add(new MenuOption(StateId.Credits, ScreenName.MainCreditsSelect));
            Options.Add(new MenuOption(StateId.NewGame, ScreenName.MainNewGame));
            Options.Add(new MenuOption(StateId.Options, ScreenName.MainOptionsSelect));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.MainQuitSelect));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.BackEsc));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.BackB));

            DrawList.Add(Background);
        }

        public void Draw(Game1 game, GameTime gameTime)
        {

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            game.Spritebatch.Begin();

            game.GraphicsDevice.Viewport = game.Camera.EntireView;

            foreach (ScreenName screen in DrawList)
            {
                ScreenSprite currentSprite = ScreenSpriteMap.Instance.GetSprite(screen);

                game.Spritebatch.Draw(currentSprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);
            }

            game.Spritebatch.End();
        }

        public void ToggleOption(ScreenName option)
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
    }
}