using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;

namespace Sprint5.Menus
{
    public class GameOverScreen
    {

        private static readonly GameOverScreen instance = new GameOverScreen();

        private GameOverSprite sprite;

        private int drawBounds = 0;
         




        public static GameOverScreen Instance
        {
            get
            {
                return instance;
            }
        }
        private GameOverScreen()
        {
            sprite = SpriteFactory.Instance.CreateGameOverSprite();
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
