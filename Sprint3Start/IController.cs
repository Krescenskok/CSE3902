using System;
using Microsoft.Xna.Framework;

namespace Sprint2
{
    interface IController
    {
        public ICommand HandleInput(Game1 game);

    }
}
