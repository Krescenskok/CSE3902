using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.ScreenHandling;
using System;
using System.Collections.Generic;

namespace Sprint5.GameStateHandling
{
    public class GameplayState : IGameStates
    {

        public StateId Id { get; } = StateId.Gameplay;
        public IGameStates Parent { get; set; }
        public List<String> Options { get; set; }
        private static readonly GameplayState instance = new GameplayState();
        public static GameplayState Instance
        {
            get
            {
                return instance;
            }
        }

        public IScreen Screen { get; set; }

        private GameplayState()
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
            game.Spritebatch.Begin(transformMatrix: game.Camera.Transform);

            game.GraphicsDevice.Viewport = game.Camera.gameView;
            game.GraphicsDevice.Clear(Color.Black);

            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);

            RoomSpawner.Instance.Draw(game.Spritebatch);
            game.LinkPersistent.ExecuteCommand(game, gameTime, game.Spritebatch);
            RoomSpawner.Instance.DrawTopLayer(game.Spritebatch);
            game.ProjectilePersistent.ExecuteCommand(game, gameTime, game.Spritebatch);
            RoomEnemies.Instance.DrawTests(game.Spritebatch);

            game.Spritebatch.End();

            game.Spritebatch.Begin();
            game.GraphicsDevice.Viewport = game.Camera.HUDView;
            LinkInventory.Instance.Draw(game.Spritebatch);
            HUD.Instance.DrawBottom(game.Spritebatch);

            game.Spritebatch.End();
        }
    }
}
