﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.ScreenHandling;

namespace Sprint5.GameStateHandling
{
    public class TransitionState : IGameStates
    {
        private static readonly TransitionState instance = new TransitionState();
        public StateId Id { get; } = StateId.Transition;
        public IGameStates Parent { get; set; }
        public static TransitionState Instance
        {
            get
            {
                return instance;
            }
        }

        public IScreen Screen { get; set; }

        private TransitionState()
        {

        }
        public void Update(Game1 game, GameTime gameTime)
        {

            Sounds.Instance.Update();
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

            Camera.Instance.Draw(game.Spritebatch);
            game.Spritebatch.End();

            game.Spritebatch.Begin();
            game.GraphicsDevice.Viewport = game.Camera.HUDView;
            LinkInventory.Instance.Draw(game.Spritebatch);
            HUD.Instance.DrawBottom(game.Spritebatch);

            game.Spritebatch.End();
        }
    }
}
