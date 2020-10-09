using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Link
{
    public interface ILinkState
    {
        void Stationary();
        void MovingLeft();
        void MovingRight();
        void MovingUp();
        void MovingDown();


        public Vector2 Update(GameTime gameTime, Vector2 location);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location);
    }
}
