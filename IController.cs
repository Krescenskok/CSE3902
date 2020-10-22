using System;
using Microsoft.Xna.Framework;

namespace Sprint2Final
{
    interface IController
    {
        public ICommand HandleInput(Game1 game);

    }
}
