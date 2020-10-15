using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class Keese : IEnemy
    {
        private Vector2 location;

        public IEnemyState state;
        private ISprite sprite;

        private Vector2 size =  new Vector2(100,100);

        private Game game;

        public enum direction { left = -1, right = 1, up = 2, down = -2};
        List<direction> availableDirections;
        List<direction> currentDirection;

        private int directionChangeCoolDown;

        private int randomMovementMultiplier;


        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }



        public Keese(Game game, Vector2 location)
        {
            this.game = game;
            this.location = location;
            state = new KeeseMoveState(this, location);

            currentDirection = new List<direction>() { direction.right, direction.left };

            directionChangeCoolDown = 0;
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

        public void Draw(SpriteBatch spriteBatch)
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

            bool blockedOnRight = xMax >= boundX && currentDirection.Contains(direction.right);
            bool blockedOnLeft = xMin <= 0 && currentDirection.Contains(direction.left);
            bool blockedAbove = yMax >= boundY && currentDirection.Contains(direction.up);
            bool blockedBelow = yMin <= 0 && currentDirection.Contains(direction.down);


            if (blockedAbove || blockedBelow || blockedOnLeft || blockedOnRight && directionChangeCoolDown == 0)
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
                int randNum = r.Next(0, 100 / randomMovementMultiplier);
                availableDirections = new List<direction> { direction.left, direction.right, direction.up, direction.down };

                if (randNum == 0)
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

      

        public void UpdateDirection(List<direction> dir)
        {
            currentDirection[0] = dir[0];
            currentDirection[1] = dir[1];
        }

        public int CoolDown(int var, bool resetCoolDown)
        {
            if (resetCoolDown) return 10;
            if (var == 0) return 0;
            return --var;

        }
    }
}
