using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public interface ILinkState
    {

        public Vector2 Update(GameTime gameTime, Vector2 location);
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location);
    }
}
