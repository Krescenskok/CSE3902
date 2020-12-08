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
        public ScreenName Background { get; set; }

        public List<ScreenName> Options { get; set; }
        public List<ScreenName> DrawList { get; set; }

        public OptionsScreen()
        {
            Options = new List<ScreenName>();
            DrawList = new List<ScreenName>();
            Background = ScreenName.OptionsBG;

            Options.Add(ScreenName.BackSelect);

            Options.Add(ScreenName.OptionsControlSelect);
            Options.Add(ScreenName.OptionsFullScreenSelect);
            Options.Add(ScreenName.OptionsKeyBindingsSelect);
            Options.Add(ScreenName.OptionsSoundSelect);
            Options.Add(ScreenName.BackEsc);
            Options.Add(ScreenName.BackB);

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