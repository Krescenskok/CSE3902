using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2
{
    public interface IItemsState
    {
        void Update();

        void Expire();

        void Collected();

    }
}
