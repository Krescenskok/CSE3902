using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3
{
    public interface ISprite
    {
        public void Draw(SpriteBatch spriteBatch, Vector2 location, int currentFrame, Color color);

    }
}
