using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using Microsoft.Xna.Framework.Content;

namespace Sprint5.Menus
{
    public class WinScreen : IScreen
    {

        private static readonly WinScreen instance = new WinScreen();

        private WinSprite sprite;

        private int drawBounds = 0;
        
        public static WinScreen Instance
        {
            get
            {
                return instance;
            }
        }

        public ScreenSprite Sprite { get; set; }
        public List<ScreenSprite> Options { get; set; }

        private WinScreen()
        {
        }

        public void generateOptions(ContentManager content)
        {
            Texture2D current = content.Load<Texture2D>("icons/winmainmenu");

            ScreenSprite CurrentSprite = ScreenFactory.Instance.createScreenSprite(current);

            Options.Add(CurrentSprite);

            current = content.Load<Texture2D>("icons/winmainmenuSelect");

            CurrentSprite = ScreenFactory.Instance.createScreenSprite(current);

            Options.Add(CurrentSprite);

            current = content.Load<Texture2D>("icons/winquit");

            CurrentSprite = ScreenFactory.Instance.createScreenSprite(current);

            Options.Add(CurrentSprite);

            current = content.Load<Texture2D>("icons/winquitselect");

            CurrentSprite = ScreenFactory.Instance.createScreenSprite(current);

            Options.Add(CurrentSprite);

            current = content.Load<Texture2D>("icons/wincrediselect");

            CurrentSprite = ScreenFactory.Instance.createScreenSprite(current);

            Options.Add(CurrentSprite);

            current = content.Load<Texture2D>("icons/win");

            CurrentSprite = ScreenFactory.Instance.createScreenSprite(current);

            Options.Add(CurrentSprite);



        }

        public void Draw(Game1 game, GameTime gameTime)
        {

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            game.Spritebatch.Begin();


            game.GraphicsDevice.Viewport = game.Camera.EntireView;

            game.Spritebatch.Draw(Sprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);

            game.Spritebatch.End();
        }


    }
}
