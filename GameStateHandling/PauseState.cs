using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class PauseState : IGameStates
    {
        private static readonly PauseState instance = new PauseState();
        public static PauseState Instance
        {
            get
            {
                return instance;
            }
        }
        private PauseState()
        {

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


            if (game.State.Id == StateId.Pause && !game.DoorPause)
            {
                PauseScreen.Instance.Draw(game.Spritebatch, game, font);
            }
            game.Spritebatch.End();
        }

        public void Update(Game1 game, GameTime gameTime)
        {
        }
    }
}
