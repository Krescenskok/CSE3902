using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveDown : Movement
    {

        private double lastTime;
        int MOVEMENT = 10;
        int TIME = 100;
        int Y_LOCATION = 375;
        int PICKUP = 300;

        public MoveDown(LinkPlayer link) : base(link)
        {

        }


        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                if (!link.isWalkingInPlace)
                {
                    location.Y += MOVEMENT;
                }
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;


                if (link.LargeShield)
                {
                    if (currentFrame == 10)
                    {
                        currentFrame = 11;
                    }
                    else
                        currentFrame = 10;
                }
                else
                {
                    if (currentFrame == 0)
                    {
                        currentFrame = 1;
                    }
                    else
                        currentFrame = 0;
                }

                if (location.Y >= Y_LOCATION)
                    location.Y = Y_LOCATION;
            }
            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
        }

        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 10:
                        case 11: currentFrame = 23; break;
                        case 23: currentFrame = 22; break;
                        case 22: currentFrame = 21; break;
                        case 21: currentFrame = 20; break;
                        case 20:
                            currentFrame = 11;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 0:
                        case 1: currentFrame = 23; break;
                        case 23: currentFrame = 22; break;
                        case 22: currentFrame = 21; break;
                        case 21: currentFrame = 20; break;
                        case 20:
                            currentFrame = 1;
                            break;
                    }

                }

                link.IsAttacking = false;
                link.IsStopped = true;
            }

            return location;
        }

        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 10:
                        case 11: currentFrame = 45; break;
                        case 45: currentFrame = 44; break;
                        case 44: currentFrame = 43; break;
                        case 43: currentFrame = 42; break;
                        case 42:
                            currentFrame = 11;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 0:
                        case 1: currentFrame = 45; break;
                        case 45: currentFrame = 44; break;
                        case 44: currentFrame = 43; break;
                        case 43: currentFrame = 42; break;
                        case 42:
                            currentFrame = 1;
                            break;
                    }
                }


                link.IsAttacking = false;
                link.IsStopped = true;
            }

            return location;
        }


        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
                {
                    switch (currentFrame)
                    {
                        case 10:
                        case 11: currentFrame = 77; break;
                        case 77: currentFrame = 76; break;
                        case 76: currentFrame = 75; break;
                        case 75: currentFrame = 74; break;
                        case 74:
                            currentFrame = 11;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 0:
                        case 1: currentFrame = 77; break;
                        case 77: currentFrame = 76; break;
                        case 76: currentFrame = 75; break;
                        case 75: currentFrame = 74; break;
                        case 74:
                            currentFrame = 1;
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
                        case 10:
                        case 11: currentFrame = 8; break;
                        case 8: currentFrame = 9; break;
                        case 9:
                            currentFrame = 11;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 0:
                        case 1: currentFrame = 8; break;
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
                        case 10:
                        case 11: currentFrame = 16; break;
                        case 16:
                            currentFrame = 11;
                            break;
                    }
                }
                else
                {
                    switch (currentFrame)
                    {
                        case 0:
                        case 1: currentFrame = 16; break;
                        case 16:
                            currentFrame = 0;
                            break;
                    }
                }

                link.IsAttacking = false;
                link.IsStopped = true;

            }

            return location;
        }

        public override Rectangle Bounds()
        {
            if(link.CurrentWeapon == ItemForLink.WoodenSword)
            {
                return new Rectangle((int)link.currentLocation.X + 8, (int)link.currentLocation.Y + 8, 10, 10);
            }

            return new Rectangle((int)link.CurrentLocation.X, (int)link.CurrentLocation.Y, 32, 32);
        }

    }

    
}
