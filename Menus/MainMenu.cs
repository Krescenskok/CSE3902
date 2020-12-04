using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum MenuState
    {
        main,
        difficulty,
        color,
        controls,
        sound
    }
    public class MainMenu
    {
        public MainMenuTexture mainScreen;
        public ControlMenuTexture controlsScreen;
        public SoundMenuTexture soundScreen;
        public DifficultyMenuTexture difficultyScreen;
        public ColorMenuTexture colorScreen;

        public Game1 currentGame;

        public MenuState state = MenuState.main;
        public MainMenu(LinkPlayer link)
        {
            mainScreen = SpriteFactory.Instance.createMainMenu();
            controlsScreen = new ControlMenuTexture(mainScreen.Texture);
            soundScreen = new SoundMenuTexture(mainScreen.Texture);
            difficultyScreen = new DifficultyMenuTexture(mainScreen.Texture);
            colorScreen = new ColorMenuTexture(mainScreen.Texture, link);
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {
            if (state == MenuState.main) mainScreen.Draw(batch, game, font);
            else if (state == MenuState.difficulty) difficultyScreen.Draw(batch, game, font);
            else if (state == MenuState.controls) controlsScreen.Draw(batch, game, font);
            else if (state == MenuState.sound) soundScreen.Draw(batch, game, font);
            else if (state == MenuState.color) colorScreen.Draw(batch, game, font);
            currentGame = game;
        }

        public void goUp()
        {
            if (state == MenuState.main) mainScreen.goUp();
            else if (state == MenuState.difficulty) difficultyScreen.goUp();
            else if (state == MenuState.controls) controlsScreen.goUp();
            else if (state == MenuState.sound) soundScreen.goUp();
            else if (state == MenuState.color) colorScreen.goUp();
        }
        public void goDown()
        {
            if (state == MenuState.main) mainScreen.goDown();
            else if (state == MenuState.difficulty) difficultyScreen.goDown();
            else if (state == MenuState.controls) controlsScreen.goDown();
            else if (state == MenuState.sound) soundScreen.goDown();
            else if (state == MenuState.color) colorScreen.goDown();
        }
        public void select()
        {
            if (state == MenuState.main) mainScreen.select(this);
            else if (state == MenuState.difficulty) difficultyScreen.select(this);
            else if (state == MenuState.controls) controlsScreen.select(this);
            else if (state == MenuState.sound) soundScreen.select(this);
            else if (state == MenuState.color) colorScreen.select(this);
        }
    }
}
