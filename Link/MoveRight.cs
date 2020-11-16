using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;

namespace Sprint4
{
    public class MoveRight : Movement
    {


        private double lastTime;
        int MOVEMENT = 10;
        int TIME = 100;
        int PICKUP = 300;
        int X_LOCATION = 660;


        public MoveRight(LinkPlayer link) : base(link)
        {

        }



        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME)
            {
                if (link.possibleDirections.Contains(Direction.right))
                {
                    location.X += MOVEMENT;
                }
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (link.LargeShield)
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
                        case 12:
                        case 13: currentFrame = 39; break;
                        case 39: currentFrame = 38; break;
                        case 38: currentFrame = 25; break;
                        case 25: currentFrame = 24; break;
                        case 24:
                            currentFrame = 12;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
                            break;
                    }
                }

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
                        case 12:
                        case 13: currentFrame = 71; break;
                        case 71: currentFrame = 70; break;
                        case 70: currentFrame = 47; break;
                        case 47: currentFrame = 46; break;
                        case 46:
                            currentFrame = 12;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
                            break;
                    }
                }

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
                        case 12:
                        case 13: currentFrame = 93; break;
                        case 93: currentFrame = 92; break;
                        case 92: currentFrame = 79; break;
                        case 79: currentFrame = 78; break;
                        case 78:
                            currentFrame = 12;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
                            break;
                    }

                }

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
                        case 12:
                        case 13: currentFrame = 8; break;
                        case 8: currentFrame = 9; break;
                        case 9:
                            currentFrame = 12;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
                            break;
                    }
                }

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
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
                            break;
                    }
                }

            }

            return location;
        }

        public override Rectangle Bounds()
        {
            if (link.CurrentWeapon == ItemForLink.Shield)
            {
                return new Rectangle((int)link.currentLocation.X + 5, (int)link.currentLocation.Y + 5, 2*13, 2*13);
            }
            else if (link.CurrentWeapon == ItemForLink.WoodenSword && link.IsAttacking)
            {
                return new Rectangle((int)link.currentLocation.X + 2, (int)link.currentLocation.Y + 8, 2*24, 2*16);
            }
            else if (link.CurrentWeapon == ItemForLink.Sword && link.IsAttacking)
            {
                return new Rectangle((int)link.currentLocation.X + 3, (int)link.currentLocation.Y + 5, 2*26, 2*16);
            }
            else if (link.CurrentWeapon == ItemForLink.MagicalRod && link.IsAttacking)
            {
                return new Rectangle((int)link.currentLocation.X + 2, (int)link.currentLocation.Y + 10, 2*24, 2*16);
            }

            return new Rectangle((int)link.currentLocation.X + 5, (int)link.currentLocation.Y + 5, 2 * 13, 2 * 13);
        }

    }
}
