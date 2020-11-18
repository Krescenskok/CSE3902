using System;
using Microsoft.Xna.Framework;

namespace Sprint4
{
    interface IController
    {
        public ICommand HandleInput(Game1 game);

    }
}
