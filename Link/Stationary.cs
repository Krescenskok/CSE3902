using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;

namespace Sprint4
{
    public class Stationary : Movement
    {

        private double lastTime;
        int TIME_THOU = 1000;
        int TIME_HUN = 100;
        int TIME_THREE = 300;
        int TIME = 100;
        int PICKUP = 300;


        public Stationary(LinkPlayer link, LinkSprite sprite) : base(link)
        {
            linkSprite = sprite;
        }

        Color[] colors = { Color.Yellow, Color.Pink, Color.Green, Color.Gold, Color.Blue, Color.IndianRed, Color.Indigo, Color.Ivory };
        Color[] clockColors = { Color.Blue, Color.White, Color.BlueViolet, Color.LightBlue, Color.Aquamarine, Color.Aqua };

        int i = 0;
       

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
                        case 11: currentFrame = 37; break;
                        case 37: currentFrame = 36; break;
                        case 36: currentFrame = 21; break;
                        case 21: currentFrame = 20; break;
                        case 20:
                            currentFrame = 11;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                        case 10:
                        case 11: currentFrame = 39; break;
                        case 39: currentFrame = 38; break;
                        case 38: currentFrame = 43; break;
                        case 43: currentFrame = 42; break;
                        case 42:
                            currentFrame = 11;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                        case 10:
                        case 11: currentFrame = 91; break;
                        case 91: currentFrame = 90; break;
                        case 90: currentFrame = 75; break;
                        case 75: currentFrame = 74; break;
                        case 74:
                            currentFrame = 11;
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
                            break;
                    }
                }

            }

            return location;
        }

        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME_HUN)
            {

                lastTime = gameTime.TotalGameTime.TotalMilliseconds;



                if (currentFrame == 0)
                {
                    currentFrame = 1;
                }
                else
                    currentFrame = 0;

            }
            link.IsAttacking = false;
            link.IsStopped = true;
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
                            link.IsAttacking = false;
                            link.IsStopped = true;
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
            throw new NotImplementedException();
        }

        public override Rectangle Bounds()
        {


            //return new Rectangle((int)link.currentLocation.X, (int)link.currentLocation.Y + 4, 2 * 14, 2 * 14);

            return link.hitbox;
            // new Rectangle((int)link.currentLocation.X, (int)link.currentLocation.Y, , );


        }
    }
}
