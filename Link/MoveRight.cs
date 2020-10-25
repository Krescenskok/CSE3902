using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public class MoveRight : Movement
    {


        private double lastTime;
        int MOVEMENT = 10;


        public MoveRight(LinkPlayer link) : base(link)
        {

        }



        public override Vector2 HandleShield(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                location.X += MOVEMENT;
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

                if (currentFrame == 2)
                {
                    currentFrame = 3;
                }
                else
                    currentFrame = 2;
                if (location.X >= 780)
                    location.X = 780;
            }
            link.IsAttacking = false;
            link.IsStopped = true;
            return location;
        }
      
       
    public override Vector2 HandleWoodenSword(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;
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

            return location;
        }

    
    public override Vector2 HandleSword(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

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

            return location;
        }

    
    public override Vector2 HandleMagicalRod(GameTime gameTime,Vector2 location)
        {
            if ( gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                lastTime =  gameTime.TotalGameTime.TotalMilliseconds;

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

            return location;
        }

        public override Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds-lastTime > 300)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

                switch (currentFrame)
                {
                    case 2:
                    case 3: currentFrame = 8; break;
                    case 8: currentFrame = 9; break;
                    case 9: currentFrame = 0;
                        link.IsStopped = true;
                        link.IsPickingUpItem = false;
                        break;
                }
            }

            return location;
        }

        public override Vector2 HandleArrowBow(GameTime gameTime, Vector2 location)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > 300)
            {
                lastTime = gameTime.TotalGameTime.TotalMilliseconds;

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

            return location;
        }




    }
}
