using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveDown: Movement
    {

        private double lastTime;
        int MOVEMENT = 10;


        public MoveDown(LinkPlayer link) : base(link)
        {

        }


        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
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

        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 0:
                    case 1: currentFrame = 23; break;
                    case 23: currentFrame = 22; break;
                    case 22: currentFrame = 21; break;
                    case 21: currentFrame = 20; break;
                    case 20: currentFrame = 1;
                    link.IsAttacking = false;
                    link.IsStopped = true;
                    break;
                }
            }

            return location;
        }

        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 0:
                    case 1: currentFrame = 45; break;
                    case 45: currentFrame = 44; break;
                    case 44: currentFrame = 43; break;
                    case 43: currentFrame = 42; break;
                    case 42: currentFrame = 1;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

        
        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 0:
                    case 1: currentFrame = 77; break;
                    case 77: currentFrame = 76; break;
                    case 76: currentFrame = 75; break;
                    case 75: currentFrame = 74; break;
                    case 74:
                        currentFrame = 1;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 0:
                    case 1: currentFrame = 8; break;
                    case 8: currentFrame = 9; break;
                    case 9:
                        currentFrame = 0;
                        link.IsStopped = true;
                        link.IsPickingUpItem = false;
                        break;
                }
            }

            return location;
        }


    }
}
