using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Sprint5
{
    public interface IController
    {
        public void HandleInput(Game1 game);

        public Keys getKey();
    }
}
