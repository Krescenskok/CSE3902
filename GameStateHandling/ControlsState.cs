using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class ControlsState : IGameStates
    {
        private static readonly ControlsState instance = new ControlsState();
        public StateId Id { get; } = StateId.Controls;
        public IGameStates Parent { get; set; }
        public static ControlsState Instance
        {
            get
            {
                return instance;
            }
        }
        private ControlsState()
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
            ControlsScreen.Instance.Draw(game, gameTime);
        }
    }
}
