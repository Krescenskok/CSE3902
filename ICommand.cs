using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3
{
    public interface ICommand
    {
        void DoInit(Game game);

        void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch);

        void Update(GameTime gameTime);
    }
}
