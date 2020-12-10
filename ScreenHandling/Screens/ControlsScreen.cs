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
    public class ControlsScreen : IScreen
    {
        private int drawBounds = 0;


        public List<MenuOption> Options { get; set; }
        public List<MenuOption> DrawList { get; set; }
        public MenuOption Background { get; set; }
        public List<MenuOption> Sprites { get; set; }

        private Game1 Game;

        public ControlsScreen()
        {
            Options = new List<MenuOption>();
            DrawList = new List<MenuOption>();
            Background = new MenuOption(StateId.Controls, ScreenName.ControlsBG);
            DrawList.Add(Background);



            Options.Add(new MenuOption(StateId.Controls, ScreenName.BackSelect));

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
           
        }

        public void Back()
        {
            Game.State.Swap(Game.State.Previous.Id);
        }

        public void Select()
        {
            Game.State.Swap(Game.State.Previous.Id);
        }
    }
}