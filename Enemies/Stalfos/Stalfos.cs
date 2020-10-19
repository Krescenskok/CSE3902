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
    /// <author>JT Thrash</author>
    /// Class for enemy named Stalfos (the skeleton). Must be initialized in LoadContent method in main Game1 class.
    /// </summary>
    public class Stalfos : IEnemy
    {
        
        public Vector2 location { get; set; }

        public IEnemyState state;
        private ISprite sprite;
        private StalfosWalkingSprite stalfosSprite;

        private Vector2 size;

        private Game game;

        public enum direction { left, right, up, down };
        List<direction> availableDirections;
        direction currentDirection;

       

        private int randomMovementMultiplier;

        EnemyCollider collider;

        private int HP = 10;
        private int attack = 5;

        
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
            state = new EnemySpawnState(this, game);
            collider = new EnemyCollider();

            size = new Vector2(100, 100);

            currentDirection = direction.right;

            randomMovementMultiplier = 1;
        }

        public void Spawn()
        {
            state = new StalfosWalkingState(this, location);
            stalfosSprite = (StalfosWalkingSprite)sprite;
            collider = new EnemyCollider(stalfosSprite.GetRectangle(), state, 5);
        }

        public void ChangeDirection()
        {
            state.ChangeDirection();
        }

        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
        }

        public bool SubtractHP(int amount)
        {
            HP -= amount;
            
            if (HP <= 0) { Die(); }

            return HP > 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            sprite.Draw(spriteBatch, location, 0, Color.White);
        }

        public void Update()
        {
          
            state.Update();
            collider.Update(location.ToPoint());

            
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
