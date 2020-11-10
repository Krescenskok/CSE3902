using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class Stationary : Movement
    {

        private double lastTime;
        int TIME_THOU = 1000;
        int TIME_HUN = 100;
        int TIME_THREE = 300;

        public Stationary(LinkPlayer link) : base(link)
        {

        }

        Color[] colors = { Color.Yellow, Color.Pink, Color.Green, Color.Gold, Color.Blue, Color.IndianRed, Color.Indigo, Color.Ivory };
        Color[] clockColors = { Color.Blue, Color.White, Color.BlueViolet, Color.LightBlue, Color.Aquamarine, Color.Aqua};

        int i = 0;

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime,Vector2 location)
        {
            Color col;
            if (link.IsDamaged || link.Clock)
            {

                if (link.DamageStartTime == 0)
                    link.DamageStartTime = gameTime.TotalGameTime.TotalMilliseconds;
                else if (gameTime.TotalGameTime.TotalMilliseconds - link.DamageStartTime < TIME_THOU)
                {
                    if (link.IsDamaged)
                    {
                        col = colors[i];

                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        i++;
                        if (i == colors.Length - 1)
                        {
                            i = 0;
                        }

                    }
                    else if (link.Clock)
                    {
                        col = clockColors[i];
                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        i++;
                        if (i == clockColors.Length - 1)
                        {
                            i = 0;
                        }

                    }



                }
                else
                {
                    link.IsDamaged = false;
                    link.Clock = false;

                }

            }
            else
            {

                if (linkSprite == null)
                {
                    linkSprite = SpriteFactory.Instance.CreateLinkSprite();
                }
                if (link.UseRing)
                {
                    linkSprite.Draw(spriteBatch, location, currentFrame, Color.MediumAquamarine);
                }
                else
                {
                    linkSprite.Draw(spriteBatch, location, currentFrame, Color.White);

                }
            }

        }

        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {

            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME_HUN)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

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

            return location;
        }

        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME_HUN)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

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

            return location;
        }


        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME_HUN)
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
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME_THREE)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

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

            return location;
        }


        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            throw new NotImplementedException();
        }

        public override Rectangle Bounds()
        {

            return new Rectangle((int)link.CurrentLocation.X, (int)link.CurrentLocation.Y, 32, 32);

        }
    }
}
