using System;
using Microsoft.Xna.Framework;

namespace Sprint3
{
    interface IController
    {
        public ICommand HandleInput(Game1 game);

    }
}
