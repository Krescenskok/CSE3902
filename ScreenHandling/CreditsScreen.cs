using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;
using Sprint5.ScreenHandling;
using Sprint5.ScreenHandling.ScreenSprites;

namespace Sprint5.Menus
{
    public class CreditsScreen : IScreen
    {

        private static readonly CreditsScreen instance = new CreditsScreen();

        private CreditsSprite sprite;

        private int drawBounds = 0;
         



        public static CreditsScreen Instance
        {
            get
            {
                return instance;
            }
        }
        public ScreenSprite Sprite { get; set; }

        private CreditsScreen()
        {
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
