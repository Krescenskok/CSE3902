using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveUp: Movement
    {
        private double lastTime;
        int MOVEMENT = -10;
        int TIME = 100;
        int PICKUP = 300;
        int Y_LOCATION = 0;


        public MoveUp(LinkPlayer link) : base(link)
        {

        }

        public override Vector2 HandleShield(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                if (!link.isWalkingInPlace)
                {
                    location.Y += MOVEMENT;
                }
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;


                if (currentFrame == 6)
                {
                    currentFrame = 7;
                }
                else
                    currentFrame = 6;
                if (location.Y <= Y_LOCATION)
                    location.Y = Y_LOCATION;
            }
            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
        }


        public override Vector2 HandleWoodenSword(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 6:
                    case 7: currentFrame = 35; break;
                    case 35: currentFrame = 34; break;
                    case 34: currentFrame = 33; break;
                    case 33: currentFrame = 32; break;
                    case 32:
                        currentFrame = 6;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

     
    public override Vector2 HandleSword(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 6:
                    case 7: currentFrame = 57; break;
                    case 57: currentFrame = 56; break;
                    case 56: currentFrame = 55; break;
                    case 55: currentFrame = 54; break;
                    case 54:
                        currentFrame = 6;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

    public override Vector2 HandleMagicalRod(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 6:
                    case 7: currentFrame = 89; break;
                    case 89: currentFrame = 88; break;
                    case 88: currentFrame = 87; break;
                    case 87: currentFrame = 86; break;
                    case 86:
                        currentFrame = 6;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > PICKUP)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 6:
                    case 7: currentFrame = 8; break;
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

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > PICKUP)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 6:
                    case 7: currentFrame = 19; break;
                    case 19:
                        currentFrame = 6;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

    }
}
