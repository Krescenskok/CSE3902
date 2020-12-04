using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class GoriyaBoomerang
    {
        private GoriyaBoomerangSprite sprite;
        private Vector2 location;
        private Vector2 direction;
        private string directionStr;

        public Point Location { get => location.ToPoint(); }
        

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
            collider = new GoriyaBoomerangCollider(this,sprite.GetRectangle(), HPAmount.Full_Heart);

            timeSinceThrown = 0;
            returning = false;

            Sounds.Instance.AddLoopedSound("ArrowBoomerang", GetHashCode().ToString());
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
                Die();
                
            }

            sprite.Update();
           
        }

        public void Die()
        {
            CollisionHandler.Instance.RemoveCollider(collider);
            Sounds.Instance.StopLoopedSound(GetHashCode().ToString());
            finished = true;
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
        public void BounceOff(Direction dir)
        {
            if (dir.ToString().Equals(directionStr))
                returning = true;
        }
    }
}
