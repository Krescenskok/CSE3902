using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Menus
{
    public class GameOverScreen
    {
        Texture2D texture;

        private static readonly GameOverScreen instance = new GameOverScreen();

        private static Vector2 gameOver = new Vector2 (260, 95);
        private static Vector2 playAgain = new Vector2(237, 145);
        private static Vector2 quit = new Vector2(250, 175);

        public static GameOverScreen Instance
        {
            get
            {
                return instance;
            }
        }
        private GameOverScreen()
        {
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {
            game.GraphicsDevice.Viewport = game.camera.gameView;
            game.GraphicsDevice.Clear(Color.Black);
            batch.DrawString(font, "GAME OVER", gameOver, Color.White);
            batch.DrawString(font, "Press 'P' to Play Again", playAgain, Color.White);
            batch.DrawString(font, "Press 'Q' to Quit", quit, Color.White);

        }
    }
}
