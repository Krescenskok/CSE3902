using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class ChangeDirection: ILinkState
    {
        public LinkPlayer link;

        public ChangeDirection(LinkPlayer link)
        {
            this.link = link;
        }

        public Vector2 MoveDown(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                location.Y += MOVEMENT;
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (currentFrame == 0)
                {
                    currentFrame = 1;
                }
                else
                    currentFrame = 0;
                if (location.Y >= 445)
                    location.Y = 445;
            }
            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
        }

        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {
            throw new NotImplementedException();
        }

        public Vector2 Update(GameTime gameTime, Vector2 location)
        {
            throw new NotImplementedException();
        }

        void ILinkState.ChangeDirection()
        {
            throw new NotImplementedException();
        }
    }
}
