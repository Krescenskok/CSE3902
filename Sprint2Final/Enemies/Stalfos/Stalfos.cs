using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint2
{
    /// <summary>
    /// <author>JT Thrash</author>
    /// Class for enemy named Stalfos (the skeleton). Must be initialized in LoadContent method in main Game1 class.
    /// </summary>
    public class Stalfos : IEnemyNPC
    {
        
        public Vector2 location { get; set; }

        public IEnemyState state;
        private ISprite sprite;

        private Vector2 size;

        private Game game;

        public enum direction { left, right, up, down };
        List<direction> availableDirections;
        direction currentDirection;

       

        private int randomMovementMultiplier;

        
        public void SetSprite(ISprite sprite)
        {
            
            this.sprite = sprite;
            
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }



        public Stalfos(Game game, Vector2 location)
        {
            this.game = game;
            this.location = location;
            state = new StalfosWalkingState(this,location);

            size = new Vector2(100, 100);

            currentDirection = direction.right;

            randomMovementMultiplier = 1;
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

        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {            
            sprite.Draw(spriteBatch, location, 0, Color.White);
        }

        public void Update()
        {
            int xMax = (int)(location.X + size.X);
            int yMax = (int)(location.Y + size.Y);
            int xMin = (int)location.X;
            int yMin = (int)location.Y;

            int boundX = game.GraphicsDevice.Viewport.Width;
            int boundY = game.GraphicsDevice.Viewport.Height;

            bool blockedOnRight = xMax >= boundX && currentDirection == direction.right;
            bool blockedOnLeft = xMin <= 0 && currentDirection == direction.left;
            bool blockedAbove = yMax >= boundY && currentDirection == direction.up;
            bool blockedBelow = yMin <= 0 && currentDirection == direction.down;


            if (blockedAbove || blockedBelow || blockedOnLeft || blockedOnRight)
            {
                availableDirections = new List<direction>{ direction.left, direction.right, direction.up, direction.down};

                if (blockedOnRight) availableDirections.Remove(direction.right);
                if (blockedOnLeft) availableDirections.Remove(direction.left);
                if (blockedAbove) availableDirections.Remove(direction.up);
                if (blockedBelow) availableDirections.Remove(direction.down);

                ChangeDirection();
            }
            else //move randomly
            {
                Random r = new Random();
                int randNum = r.Next(0, 100 / randomMovementMultiplier);
                availableDirections = new List<direction> { direction.left, direction.right, direction.up, direction.down };

                if(randNum == 0)
                {
                    ChangeDirection();
                }
            }


            state.Update();
            
        }


        public List<direction> GetDirections()
        {
            return availableDirections;
        }

        public void UpdateDirection(direction newDirection)
        {
            currentDirection = newDirection;
        }

    }
}
