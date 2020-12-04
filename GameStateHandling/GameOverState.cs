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

            RoomSpawner.Instance.Update();
            game.LinkPersistent.Update(gameTime);
            game.ProjectilePersistent.Update(gameTime);
            CollisionHandler.Instance.Update();
            Sounds.Instance.Update();
            HUD.Instance.UpdateHearts(game.LinkPlayer);
        }
        public void Draw(SpriteFont font, Game1 game, GameTime gameTime)
        {
            game.Spritebatch.Begin();

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            GameOverScreen.Instance.Draw(game.Spritebatch, game, font);


            game.Spritebatch.End();
        }
    }
}
