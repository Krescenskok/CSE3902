using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5
{
    public class MoveLeft : Movement
    {

        int MOVEMENT = -10;

        public MoveLeft(LinkPlayer link, LinkSprite sprite) : base(link)
        {
            linkSprite = sprite;
        }


        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {
            if (link.PossibleDirections.Contains(Direction.left))
            {
                location.X += MOVEMENT;
            }

            if (link.LargeShield)
                currentFrame = currentFrame == 14 ? 15 : 14;
            else
                currentFrame = currentFrame == 4 ? 5 : 4;

            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
        }

        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {
            int[] LARGE_SHIELD_FRAMES = {14,15,40,41,30,31};
            int[] FRAMES = { 4,5,28,29,30,31 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);
            

            return location;
        }

        
        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 14, 15, 72, 73, 52, 53 };
            int[] FRAMES = { 4, 5, 50, 51, 52, 53 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);
            return location;  
        }

        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 14, 15, 94, 95, 84, 85 };
            int[] FRAMES = { 4, 5, 82, 83, 84, 85 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);
            return location;

        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            int[] LARGE_SHIELD_FRAMES = { 14, 15, 8, 9, 0 };
            int[] FRAMES = { 4, 5, 8, 9, 0 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);

            link.state = new Stationary(link, linkSprite);


            return location;

        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {

            int[] LARGE_SHIELD_FRAMES = { 14, 15, 18 };
            int[] FRAMES = { 4, 5, 18 };

            int[] currentFrames = link.LargeShield ? LARGE_SHIELD_FRAMES : FRAMES;

            SwitchFrames(currentFrames);
            return location;

        }

    }
}
