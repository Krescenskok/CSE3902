using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5
{
    public class MoveRight : Movement
    {


        private double lastTime;
        int MOVEMENT = 10;
        int TIME = 100;
        int PICKUP = 300;

        public MoveRight(LinkPlayer link, LinkSprite sprite) : base(link)
        {
            linkSprite = sprite;
        }

        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {

            if (link.possibleDirections.Contains(Direction.right))
            {
                location.X += MOVEMENT;
            }


            if (link.LargeShield)
                currentFrame = currentFrame == 12 ? 13 : 12;
            else
                currentFrame = currentFrame == 2 ? 3 : 2;

            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
       
        }


        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 12, 13, 39, 38, 25, 24 };
            int[] FRAMES = { 2, 3, 27, 26, 25, 24 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);


            return location;
           
        }


        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {
            int[] LARGE_SHIELD_FRAMES = { 12, 13, 71, 70, 47, 46 };
            int[] FRAMES = { 2, 3, 49, 48, 47, 46 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            return location;

        }


        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {
            int[] LARGE_SHIELD_FRAMES = { 12, 13, 93, 92, 79, 78 };
            int[] FRAMES = { 2, 3, 81, 80, 79, 78 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);


            return location;
         
        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 12, 13, 8, 9 };
            int[] FRAMES = { 2, 3, 8, 9 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            link.state = new Stationary(link, linkSprite);

            return location;

           
        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 12, 13, 17 };
            int[] FRAMES = { 2, 3, 17 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);


            return location;

           
        }

        

    }
}
