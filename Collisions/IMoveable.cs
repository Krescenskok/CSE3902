using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    /// <summary>
    /// Object that changes its location.
    /// </summary>
    public interface IMoveable
    {
        public Vector2 Location { get; }
    }
}
