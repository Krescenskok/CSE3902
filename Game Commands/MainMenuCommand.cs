using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    public class MainMenuCommand : ICommand
    {

        private MainMenu mainScreen;

        public Keys currentKey;
        public MainMenuCommand(MainMenu menu)
        {
            mainScreen = menu;
            currentKey = Keys.Zoom;
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {
        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            /*switch (newKey)
            {
                case Keys.W:
                case Keys.Up:
                    mainScreen.goUp();
                    currentKey = Keys.Zoom;
                    break;
                case Keys.S:
                case Keys.Down:
                    mainScreen.goDown();
                    currentKey = Keys.Zoom;
                    break;
                case Keys.Enter:
                    mainScreen.select();
                    currentKey = Keys.Zoom;
                    break;
            }
            */
        }

        public void updateKey(Keys newKey)
        {
            switch (newKey)
            {
                case Keys.W:
                case Keys.Up:
                    mainScreen.goUp();
                    currentKey = Keys.Zoom;
                    break;
                case Keys.S:
                case Keys.Down:
                    mainScreen.goDown();
                    currentKey = Keys.Zoom;
                    break;
                case Keys.Enter:
                    mainScreen.select();
                    currentKey = Keys.Zoom;
                    break;
            }
        }
    }
}
