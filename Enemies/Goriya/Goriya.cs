using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Monster that throws boomerangs</para>
    /// </summary>
    public class Goriya : IEnemy
    {

        private Vector2 location;
        private Vector2 boomerangLocation;

        public IEnemyState state;
        private ISprite sprite;
        private ISprite boomerang;
        private bool throwBoomerang;

        private Vector2 size;

        private Game game;

        public enum direction { left , right , up, down };
        List<direction> availableDirections;
        direction currentDirection;

        private const int randomMovementMultiplier = 1000;

        public void SetSprite(ISprite sprite)
        {

            this.sprite = sprite;

        }

        public void SetBoomerang(bool set)
        {
            throwBoomerang = set;
            
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
            boomerangLocation = location;
        }

        public void UpdateBoomerangLocation(Vector2 location)
        {
            boomerangLocation = location;
        }



        public Goriya(Game game, Vector2 location)
        {
            this.game = game;
            state = new GoriyaMoveState(this, location);

            this.location = location;
            boomerangLocation = location;

            size.X = 100;
            size.Y = 100;
            
            currentDirection = direction.right;

            boomerang = EnemySpriteFactory.Instance.CreateBoomerangSprite();
          
            throwBoomerang = false;
        }

        public void Attack()
        {
            state.Attack();
        }

        public void ChangeDirection()
        {
            state.ChangeDirection();
        }

        public void Die()
        {
            state.Die();
        }

        public void TakeDamage()
        {
            state.TakeDamage();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
            if (throwBoomerang) boomerang.Draw(spriteBatch, boomerangLocation, 0, Color.White);
        }

        public void Update()
        {
            int xMax = (int)(location.X + size.X);
            int yMax = (int)(location.Y + size.Y);
            int xMin = (int)location.X;
            int yMin = (int)location.Y;

            int boundX = game.GraphicsDevice.Viewport.Width;
            int boundY = game.GraphicsDevice.Viewport.Height;

            bool blockedOnRight = xMax >= boundX && currentDirection.Equals(direction.right);
            bool blockedOnLeft = xMin <= 0 && currentDirection.Equals(direction.left);
            bool blockedAbove = yMin <= 0 && currentDirection.Equals(direction.up);
            bool blockedBelow = yMax >= boundY && currentDirection.Equals(direction.down);


            if (blockedAbove || blockedBelow || blockedOnLeft || blockedOnRight)
            {
                availableDirections = new List<direction> { direction.left, direction.right, direction.up, direction.down };

                if (blockedOnRight) availableDirections.Remove(direction.right);
                if (blockedOnLeft) availableDirections.Remove(direction.left);
                if (blockedAbove) availableDirections.Remove(direction.up);
                if (blockedBelow) availableDirections.Remove(direction.down);


                ChangeDirection();


            }
            else //move randomly
            {
                Random r = new Random();
                int randNum = r.Next(0, randomMovementMultiplier);
                availableDirections = new List<direction> { direction.left, direction.right, direction.up, direction.down };

                if (randNum == 0 && !throwBoomerang)
                {
                    ChangeDirection();
                }
                else if(randNum < 5)
                {
                    Attack();
                }
            }

            state.Update();

        }


        public List<direction> GetDirections()
        {
            return availableDirections;
        }



        public void UpdateDirection(direction dir)
        {
            currentDirection = dir;
        }

    
    }
}
