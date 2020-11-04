using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveLeft : Movement
    {
        private double lastTime;
        int MOVEMENT = -10;
        int TIME = 100;
        int X_LOCATION = 0;
        int PICKUP = 300;

        public MoveLeft(LinkPlayer link) : base(link)
        {

        }



        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                if(!link.isWalkingInPlace)
                {
                    location.X += MOVEMENT;
                }
                
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    if (currentFrame == 14)
                    {
                        currentFrame = 15;
                    }
                    else
                        currentFrame = 14;
                }
                else
                {
                    if (currentFrame == 4)
                        currentFrame = 5;
                    else
                        currentFrame = 4;
                }
                
                if (location.X <= X_LOCATION)
                    location.X = X_LOCATION;
            }


            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
        }
  
        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                if(link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 14:
                        case 15: currentFrame = 28; break;
                        case 28: currentFrame = 29; break;
                        case 29: currentFrame = 30; break;
                        case 30: currentFrame = 31; break;
                        case 31:
                            currentFrame = 14;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 4:
                        case 5: currentFrame = 28; break;
                        case 28: currentFrame = 29; break;
                        case 29: currentFrame = 30; break;
                        case 30: currentFrame = 31; break;
                        case 31:
                            currentFrame = 4;      
                            break;
                    }
                }

                link.IsAttacking = false;
                link.IsStopped = true;

            }

            return location;
        }

      
        public override Vector2 HandleSword(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;


                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 14:
                        case 15: currentFrame = 50; break;
                        case 50: currentFrame = 51; break;
                        case 51: currentFrame = 52; break;
                        case 52: currentFrame = 53; break;
                        case 53:
                            currentFrame = 14;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 4:
                        case 5: currentFrame = 50; break;
                        case 50: currentFrame = 51; break;
                        case 51: currentFrame = 52; break;
                        case 52: currentFrame = 53; break;
                        case 53:
                            currentFrame = 4;
                            break;
                    }
                }

                link.IsAttacking = false;
                link.IsStopped = true;

            }

            return location;
        }

        public override Vector2 HandleMagicalRod(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 14:
                        case 55: currentFrame = 82; break;
                        case 82: currentFrame = 83; break;
                        case 83: currentFrame = 84; break;
                        case 84: currentFrame = 85; break;
                        case 85:
                            currentFrame = 14;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 4:
                        case 5: currentFrame = 82; break;
                        case 82: currentFrame = 83; break;
                        case 83: currentFrame = 84; break;
                        case 84: currentFrame = 85; break;
                        case 85:
                            currentFrame = 4;                       
                            break;
                    }
                }

                link.IsAttacking = false;
                link.IsStopped = true;
            }

            return location;
        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > PICKUP)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 14:
                        case 55: currentFrame = 8; break;
                        case 8: currentFrame = 9; break;
                        case 9:
                            currentFrame = 14;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 4:
                        case 5: currentFrame = 8; break;
                        case 8: currentFrame = 9; break;
                        case 9:
                            currentFrame = 0;
                            break;
                    }
                }
                link.IsStopped = true;
                link.IsPickingUpItem = false;

            }

            return location;
        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > PICKUP)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;


                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 14:
                        case 15: currentFrame = 18; break;
                        case 18:
                            currentFrame = 14;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 4:
                        case 5: currentFrame = 18; break;
                        case 18:
                            currentFrame = 4;
                            break;
                    }
                }
                link.IsAttacking = false;
                link.IsStopped = true;

            }

            return location;
        }

    }
}
