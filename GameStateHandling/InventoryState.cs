using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class InventoryState : IGameStates
    {
        private static readonly InventoryState instance = new InventoryState();

        public StateId Id { get; } = StateId.Inventory;
        public IGameStates Parent { get; set; }
        public static InventoryState Instance
        {
            get
            {
                return instance;
            }
        }
        private InventoryState()
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
