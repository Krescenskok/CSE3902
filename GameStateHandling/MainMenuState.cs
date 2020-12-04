using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class MainMenuState : IGameStates
    {
        private static readonly MainMenuState instance = new MainMenuState();

        public StateId Id { get; } = StateId.MainMenu;
        public IGameStates Parent { get; set; }
        public static MainMenuState Instance
        {
            get
            {
                return instance;
            }
        }
        private MainMenuState()
        {

        }
        public void Draw(SpriteFont font, Game1 game, GameTime gameTime)
        {
            game.Spritebatch.Begin();

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            game.mainScreen.Draw(game.Spritebatch, game, font);

            game.Spritebatch.End();
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
    }
}
