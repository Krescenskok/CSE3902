using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class StatsState : IGameStates
    {
        private static readonly StatsState instance = new StatsState();
        public StateId Id { get; } = StateId.Stats;
        public IGameStates Parent { get; set; }
        public static StatsState Instance
        {
            get
            {
                return instance;
            }
        }
        private StatsState()
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
            StatsScreen.Instance.Draw(game.Spritebatch, game, font, gameTime);
        }
    }
}
