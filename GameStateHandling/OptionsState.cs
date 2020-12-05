using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class OptionsState : IGameStates
    {
        private static readonly OptionsState instance = new OptionsState();
        public StateId Id { get; } = StateId.Options;
        public IGameStates Parent { get; set; }
        public static OptionsState Instance
        {
            get
            {
                return instance;
            }
        }
        private OptionsState()
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
            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);
            OptionsScreen.Instance.Draw(game, gameTime);
        }
    }
}
