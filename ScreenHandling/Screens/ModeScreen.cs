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
    public class ModeScreen : IScreen
    {
        private int drawBounds = 0;
        private List<MenuOption> Options = new List<MenuOption>();
        private List<MenuOption> DrawList = new List<MenuOption>();
        private MenuOption Background = new MenuOption(StateId.Mode, ScreenName.ModeBG);
        private List<MenuOption> Sprites = new List<MenuOption>();
        private Game1 Game;
        private GameTime GameTime;
        private int SelectedIndex = 0;
        public ModeScreen()
        {
            Options.Add(new MenuOption(StateId.Mode, ScreenName.ClassicSelect));
            Options.Add(new MenuOption(StateId.Mode, ScreenName.RogueLikeSelect));
            Options.Add(new MenuOption(StateId.MainMenu, ScreenName.BackSelect));
            Sprites.Add(new MenuOption(StateId.Mode, ScreenName.BackEsc));
            Sprites.Add(new MenuOption(StateId.Mode, ScreenName.BackB));

            DrawList.Add(Background);
            DrawList.Add(new MenuOption(StateId.Mode, ScreenName.Back));
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

            if (currentName == ScreenName.ClassicSelect)
            {

                GridGenerator.Instance.GetGrid(12, 7);
                RoomSpawner.Instance.LoadRoom(Game, Camera.Instance.firstRoom);
                Game.State.Swap(StateId.Gameplay);
            } else if (currentName == ScreenName.RogueLikeSelect)
            {
 
                MapGenerator.Instance.GenerateMap(10, "newMap"); 
                GridGenerator.Instance.GetGrid(12, 7);
                RoomSpawner.Instance.SwitchToCustomXML("newMap"); 
                RoomSpawner.Instance.LoadRoom(Game, Camera.Instance.firstRoom);
                Game.State.Swap(StateId.Gameplay);

            } else
            {
                Game.State.BackwardSwap();
            }
        }
    }
}