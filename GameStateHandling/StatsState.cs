﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.ScreenHandling;

namespace Sprint5.GameStateHandling
{
    public class StatsState : IGameStates
    {
        private static readonly StatsState instance = new StatsState();
        public StateId Id { get; } = StateId.Stats;
        public IGameStates Parent { get; set; }

        public IScreen Screen { get; set; } = new StatsScreen();

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
            game.Spritebatch.Begin();
            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);
            game.Spritebatch.End();
            Screen.Draw(game, gameTime);
        }
    }
}
