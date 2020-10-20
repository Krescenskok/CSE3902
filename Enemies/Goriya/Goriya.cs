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

        private EnemyCollider collider;

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
            state = new EnemySpawnState(this,game);

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

        public void TakeDamage(int amount)
        {
            state.TakeDamage(amount);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
            if (throwBoomerang) boomerang.Draw(spriteBatch, boomerangLocation, 0, Color.White);
        }

        public void Update()
        {
           
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

        public void Spawn()
        {
            state = new GoriyaMoveState(this, location);
        }
    }
}
