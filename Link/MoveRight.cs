using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveRight : Movement
    {


        private double lastTime;
        int MOVEMENT = 10;
        int TIME = 100;
        int PICKUP = 300;
        int X_LOCATION = 780;


        public MoveRight(LinkPlayer link) : base(link)
        {

        }



        public override Vector2 HandleShield(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                if(!link.isWalkingInPlace)
                {
                    location.X += MOVEMENT;
                }
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                if(link.LargeShield)
                {
                    if (currentFrame == 12)
                    {
                        currentFrame = 13;
                    }
                    else
                        currentFrame = 12;
                }
                else
                {
                    if (currentFrame == 2)
                    {
                        currentFrame = 3;
                    }
                    else
                        currentFrame = 2;
                }
                if (location.X >= X_LOCATION)
                    location.X = X_LOCATION;

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
                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 12:
                        case 13: currentFrame = 27; break;
                        case 27: currentFrame = 26; break;
                        case 26: currentFrame = 25; break;
                        case 25: currentFrame = 24; break;
                        case 24:
                            currentFrame = 12;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 2:
                        case 3: currentFrame = 27; break;
                        case 27: currentFrame = 26; break;
                        case 26: currentFrame = 25; break;
                        case 25: currentFrame = 24; break;
                        case 24:
                            currentFrame = 2;
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
                        case 12:
                        case 13: currentFrame = 49; break;
                        case 49: currentFrame = 48; break;
                        case 48: currentFrame = 47; break;
                        case 47: currentFrame = 46; break;
                        case 46:
                            currentFrame = 12;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 2:
                        case 3: currentFrame = 49; break;
                        case 49: currentFrame = 48; break;
                        case 48: currentFrame = 47; break;
                        case 47: currentFrame = 46; break;
                        case 46:
                            currentFrame = 2;
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
                        case 12:
                        case 13: currentFrame = 81; break;
                        case 81: currentFrame = 80; break;
                        case 80: currentFrame = 79; break;
                        case 79: currentFrame = 78; break;
                        case 78:
                            currentFrame = 12;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 2:
                        case 3: currentFrame = 81; break;
                        case 81: currentFrame = 80; break;
                        case 80: currentFrame = 79; break;
                        case 79: currentFrame = 78; break;
                        case 78:
                            currentFrame = 2;
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
            if(gameTime.TotalGameTime.TotalMilliseconds-lastTime > PICKUP)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 12:
                        case 13: currentFrame = 8; break;
                        case 8: currentFrame = 9; break;
                        case 9:
                            currentFrame = 12;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 2:
                        case 3: currentFrame = 8; break;
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
                        case 12:
                        case 13: currentFrame = 17; break;
                        case 17:
                            currentFrame = 12;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 2:
                        case 3: currentFrame = 17; break;
                        case 17:
                            currentFrame = 2;
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
