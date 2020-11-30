using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Menus
{
    public class PauseScreen
    {
        Texture2D texture;

                    private static readonly PauseScreen instance = new PauseScreen();

        private static int drawBounds = 0;
        private static float opacity = .7f;

        private static Vector2 pause = new Vector2(285, 95);
        private static Vector2 resume = new Vector2(237, 145);
        private static Vector2 quit = new Vector2(250, 175);

        public static PauseScreen Instance
        {
            get
            {
                return instance;
            }
        }
        private PauseScreen()
        {
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {
            batch.Begin();

            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Black });

            game.GraphicsDevice.Viewport = game.camera.gameView;

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White * opacity);

            batch.DrawString(font, "Paused", pause, Color.White);
            batch.DrawString(font, "Press 'G' to resume", resume, Color.White);
            batch.DrawString(font, "Press 'Q' to Quit", quit, Color.White);

            batch.End();
        }
    }
}
