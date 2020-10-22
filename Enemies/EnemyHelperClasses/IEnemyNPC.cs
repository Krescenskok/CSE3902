using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public interface IEnemyNPC
    {

        void Update();
        void Draw(SpriteBatch spriteBatch, GameTime time);
    }
}
