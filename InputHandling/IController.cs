using System;
using Microsoft.Xna.Framework;

namespace Sprint5
{
    interface IController
    {
        public ICommand HandleInput(Game1 game);

    }
}
