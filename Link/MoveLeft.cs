using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveLeft : Movement
    {
        private double lastTime;
        int MOVEMENT = -10;

        public MoveLeft(LinkPlayer link) : base(link)
        {

        }



        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                if(!link.isWalkingInPlace)
                {
                    location.X += MOVEMENT;
                }
                
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                if (currentFrame == 4)
                    currentFrame = 5;
                else
                    currentFrame = 4;

                if (location.X <= 0)
                    location.X = 0;
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
                    case 4:
                    case 5: currentFrame = 28; break;
                    case 28: currentFrame = 29; break;
                    case 29: currentFrame = 30; break;
                    case 30: currentFrame = 31; break;
                    case 31: currentFrame = 4;
                    link.IsAttacking = false;
                    link.IsStopped = true;
                    break;
                }
            }

            return location;
        }

      
        public override Vector2 HandleSword(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 4:
                    case 5: currentFrame = 50; break;
                    case 50: currentFrame = 51; break;
                    case 51: currentFrame = 52; break;
                    case 52: currentFrame = 53; break;
                    case 53:
                        currentFrame = 4;
                        link.IsAttacking = false;
                        link.IsStopped = true;
                        break;
                }
            }

            return location;
        }

        public override Vector2 HandleMagicalRod(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 4:
                    case 5: currentFrame = 82; break;
                    case 82: currentFrame = 83; break;
                    case 83: currentFrame = 84; break;
                    case 84: currentFrame = 85; break;
                    case 85:
                        currentFrame = 4;
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
                    case 4:
                    case 5: currentFrame = 8; break;
                    case 8: currentFrame = 9; break;
                    case 9:
                        currentFrame = 4;
                        link.IsStopped = true;
                        link.IsPickingUpItem = false;
                        break;
                }
            }

            return location;
        }

    }
}
