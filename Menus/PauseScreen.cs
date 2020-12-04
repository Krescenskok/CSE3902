﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.Menus
{
    public class PauseScreen
    {
        private Texture2D texture;

                    private static readonly PauseScreen instance = new PauseScreen();

        private static int drawBounds = 0;
        private static float opacity = .7f;

        private static Vector2 pause = new Vector2(285, 95);
        private static Vector2 resume = new Vector2(237, 145);
        private static Vector2 quit = new Vector2(250, 175);
        private PauseSprite sprite;
        public static PauseScreen Instance
        {
            get
            {
                return instance;
            }
        }
        private PauseScreen()
        {
            sprite = SpriteFactory.Instance.CreatePauseSprite();
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {
            game.Spritebatch.Begin();
           
            texture = new Texture2D(game.GraphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Black });

            game.GraphicsDevice.Viewport = game.Camera.gameView;
            batch.Draw(sprite.Texture, new Rectangle(drawBounds, drawBounds, game.Camera.playArea.Width, game.Camera.playArea.Height), Color.White); ; ;

            game.Spritebatch.End();
        }
    }
}
