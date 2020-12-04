using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class WinState : IGameStates
    {
        private static readonly WinState instance = new WinState();
        public StateId Id { get; } = StateId.Win;
        public IGameStates Parent { get; set; }
        public static WinState Instance
        {
            get
            {
                return instance;
            }
        }
        private WinState()
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
            WinScreen.Instance.Draw(game.Spritebatch, game, font, gameTime);
        }
    }
}
