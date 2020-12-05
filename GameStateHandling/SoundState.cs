using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Menus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.GameStateHandling
{
    public class SoundState : IGameStates
    {
        private static readonly SoundState instance = new SoundState();
        public StateId Id { get; } = StateId.Sound;
        public IGameStates Parent { get; set; }
        public static SoundState Instance
        {
            get
            {
                return instance;
            }
        }
        private SoundState()
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
            SoundScreen.Instance.Draw(game.Spritebatch, game, font, gameTime);
        }
    }
}
