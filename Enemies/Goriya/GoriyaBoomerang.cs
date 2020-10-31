using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class GoriyaBoomerang
    {
        GoriyaBoomerangSprite sprite;
        private Vector2 location;
        private Vector2 direction;
        private string directionStr;
        

        private int timeSinceThrown;
        private const int boomerangThrowTime = 50;
        private bool returning;
        private int moveSpeed;
        private bool finished;

        private GoriyaBoomerangCollider collider;
        public GoriyaBoomerang(Vector2 location, string direction, int goriyaSpeed)
        {
            this.location = location;
            this.moveSpeed = goriyaSpeed;

            if (direction.Equals("left")) this.direction = new Vector2(-1, 0);
            if (direction.Equals("right")) this.direction = new Vector2(1, 0);
            if (direction.Equals("down")) this.direction = new Vector2(0, 1);
            if (direction.Equals("up")) this.direction = new Vector2(0, -1);
            directionStr = direction;
           
            sprite = (GoriyaBoomerangSprite)EnemySpriteFactory.Instance.CreateBoomerangSprite();
            collider = new GoriyaBoomerangCollider(this,sprite.GetRectangle(), HPAmount.OneHeart);

            timeSinceThrown = 0;
            returning = false;
        }

        public void Update()
        {
            
            bool boomerangIsLeaving = timeSinceThrown < boomerangThrowTime && !returning;
            bool boomerangComingBack = timeSinceThrown > 0 && returning;
            bool boomerangAtEndOfThrow = timeSinceThrown == boomerangThrowTime && !returning;
            bool boomerangDone = timeSinceThrown == 0 && returning;

            if (boomerangIsLeaving)
            {
                location.X += direction.X * moveSpeed * 5;
                location.Y += direction.Y * moveSpeed * 5;
                
                timeSinceThrown++;
            }
            else if (boomerangAtEndOfThrow)
            {
                returning = true;
            }
            else if (boomerangComingBack)
            {
                location.X -= direction.X * moveSpeed * 5;
                location.Y -= direction.Y * moveSpeed * 5;
                
                timeSinceThrown--;
            }
            else if (boomerangDone)
            {
                finished = true;
                
            }


            collider.Update(location.ToPoint());
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }

        public bool Finished()
        {
            return finished;
        }

        public void BounceOff(Collision collision)
        {
            if(collision.From.ToString().Equals(directionStr))
                returning = true;
        }
    }
}
