using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Blocks
{
    public interface IBlock
    {
        Boolean getMoveable();

        BlocksSprite getBlockSprite();

        void Update();

        void Draw(SpriteBatch spriteBatch);

        void move(string compare);

        Rectangle getDestination();

        Boolean GetMoveable();
        

    }
}