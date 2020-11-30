using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5
{
    public class Stationary : Movement
    {

        private double lastTime;
        int TIME_HUN = 100;
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

            int[] LARGE_SHIELD_FRAMES = { 10, 11, 37, 36, 21, 20 };
            int[] FRAMES = { 0, 1, 23, 22, 21, 20 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            return location;
           
        }

        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {
            int[] LARGE_SHIELD_FRAMES = { 10, 11, 39, 38, 43, 42 };
            int[] FRAMES = { 0, 1, 45, 44, 43, 42 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            return location;
        }

        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {
            int[] LARGE_SHIELD_FRAMES = { 10, 11, 91, 90, 75, 74 };
            int[] FRAMES = { 0, 1, 77, 76, 75, 74 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            return location;

        }

        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > TIME_HUN)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                if (currentFrame == 0)
                    currentFrame = 1;
                else
                    currentFrame = 0;
            }
            link.IsAttacking = false;
            link.IsStopped = true;
            return location;
        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 10, 11, 16 };
            int[] FRAMES = { 0, 1, 16};

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            return location;

        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            throw new NotImplementedException();
        }

        
    }
}
