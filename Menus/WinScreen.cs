using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;

namespace Sprint5.Menus
{
    public class WinScreen
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
        private WinScreen()
        {
            sprite = SpriteFactory.Instance.CreateWinSprite();
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font, GameTime gameTime)
        {

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            batch.Begin();


            game.GraphicsDevice.Viewport = game.Camera.EntireView;

            batch.Draw(sprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.EntireArea.Width, game.Camera.EntireArea.Height), Color.White);

            batch.End();
        }


    }
}
