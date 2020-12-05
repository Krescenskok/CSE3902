using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class CreditsState : IGameStates
    {
        private static readonly CreditsState instance = new CreditsState();
        public static CreditsState Instance
        {
            get
            {
                return instance;
            }
        }

        public StateId Id { get; } = StateId.Credits;
        public IGameStates Parent { get; set; }

        private CreditsState()
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
            CreditsScreen.Instance.Draw(game, gameTime);
        }
    }
}
