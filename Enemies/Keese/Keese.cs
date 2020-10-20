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

        private Game game;
        

        private EnemyCollider collider;

        private const int ATTACK = 5;


        public Keese(Game game, Vector2 location)
        {
            this.game = game;
            this.location = location;
            state = new EnemySpawnState(this, game);

            collider = new EnemyCollider();
            
        }

        public void Spawn()
        {
            
            state = new KeeseMoveState(this, location);
            KeeseMoveSprite kSprite = (KeeseMoveSprite)sprite;
            collider = new EnemyCollider(kSprite.GetRectangle(), state,ATTACK);
        }
        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
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
        }

        public void Update()
        {
            
            state.Update();

        }



    }
}
