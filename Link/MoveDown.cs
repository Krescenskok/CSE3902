using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5
{
    public class MoveDown : Movement
    {

        private double lastTime;
        int MOVEMENT = 10;
        int TIME = 100;
        int Y_LOCATION = 375;
        int PICKUP = 300;

        public MoveDown(LinkPlayer link, LinkSprite sprite) : base(link)
        {
            linkSprite = sprite;
        }
        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {

            if (link.PossibleDirections.Contains(Direction.down))
            {
                location.Y += MOVEMENT;
            }


            if (link.LargeShield)
                currentFrame = currentFrame == 10 ? 11 : 10;
            else
                currentFrame = currentFrame == 0 ? 1 : 0;

            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
   
        }

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

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 10, 11, 8, 9 };
            int[] FRAMES = { 0, 1, 8, 9 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            link.state = new Stationary(link, linkSprite);

            return location;
        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 10, 11, 16 };
            int[] FRAMES = { 0, 1, 16 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);


            return location;
        }

        

    }

}
