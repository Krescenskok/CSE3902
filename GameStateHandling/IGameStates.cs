using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public interface IGameStates
    {


        public void Draw(SpriteFont font, Game1 game, GameTime gameTime);

        public void Update(Game1 game, GameTime gameTime);

        StateId Id { get;}

        IGameStates Parent { get; set; }
    }
}
