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

            RoomSpawner.Instance.Update();
            game.LinkPersistent.Update(gameTime);
            game.ProjectilePersistent.Update(gameTime);
            CollisionHandler.Instance.Update();
            Sounds.Instance.Update();
            HUD.Instance.UpdateHearts(game.LinkPlayer);
        }
        public void Draw(SpriteFont font, Game1 game, GameTime gameTime)
        {

        }
    }
}
