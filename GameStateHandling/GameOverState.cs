using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class GameOverState : IGameStates
    {

        public StateId Id { get; } = StateId.GameOver;
        public IGameStates Parent { get; set; }
        private static readonly GameOverState instance = new GameOverState();
        public static GameOverState Instance
        {
            get
            {
                return instance;
            }
        }
        private GameOverState()
        {

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            if (game.ActiveCommand != null)
                game.ActiveCommand.Update(gameTime);

            Sounds.Instance.Update();
        }
        public void Draw(SpriteFont font, Game1 game, GameTime gameTime)
        {
            GameOverScreen.Instance.Draw(game.Spritebatch, game, font, gameTime);
        }
    }
}
