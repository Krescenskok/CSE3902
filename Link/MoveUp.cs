using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5
{
    public class MoveUp : Movement
    {
        private double lastTime;
        int MOVEMENT = -10;
        int TIME = 100;
        int PICKUP = 300;
  
        public MoveUp(LinkPlayer link, LinkSprite sprite) : base(link)
        {
            linkSprite = sprite;
        }

        public override Vector2 HandleShield(GameTime gameTime, Vector2 location)
        {

            if (link.PossibleDirections.Contains(Direction.up))
            {
                location.Y += MOVEMENT;
            }
            
            currentFrame = currentFrame == 6 ? 7 : 6;

            link.IsAttacking = false;
            link.IsStopped = true;

            return location;
           
        }


        public override Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location)
        {
            int[] FRAMES = { 6, 7, 35, 34, 33, 32 };

            int[] currentFrames = FRAMES;

            SwitchFrames(currentFrames);

            return location;

        }


        public override Vector2 HandleSword(GameTime gameTime, Vector2 location)
        {

            int[] FRAMES = { 6, 7, 67, 66, 65, 64 };
            int[] currentFrames = FRAMES;

            SwitchFrames(currentFrames);

            link.state = new Stationary(link, linkSprite);

            return location;

         
        }

        public override Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location)
        {
            int[] FRAMES = { 6, 7, 89, 88, 87, 86 };

            int[] currentFrames = FRAMES;

            SwitchFrames(currentFrames);


            return location;


        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            int[] FRAMES = { 6, 7, 8, 9 };

            int[] currentFrames = FRAMES;

            SwitchFrames(currentFrames);


            return location;

        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {

            int[] FRAMES = { 6, 7, 19 };

            int[] currentFrames = FRAMES;

            SwitchFrames(currentFrames);


            return location;

        }

    }
}
