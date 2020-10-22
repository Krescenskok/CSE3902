using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public interface INPC
    {
        void Update();

        void Draw(SpriteBatch batch, GameTime time);
    }
}
