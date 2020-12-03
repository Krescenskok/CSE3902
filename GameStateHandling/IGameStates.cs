using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public interface IGameStates
    {
        public enum Type { MainMenu, GameOver, Inventory, Pause, Gameplay }

        public void Draw(SpriteFont font, Game1 game, GameTime gameTime);

        public void Update(Game1 game, GameTime gameTime);
    }
}
