using System;
using Microsoft.Xna.Framework;

namespace Sprint0
{
    interface IController
    {
        public ICommand HandleInput(Game game);
        
    }
}
