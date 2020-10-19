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
        public IEnemyState state;


        private Vector2 location;
        private ISprite sprite;
        private StalfosWalkingSprite stalfosSprite;
        private EnemyCollider collider;

        private int HP = 10;
        private const int attack = 5;

        public Stalfos(Game game, Vector2 location)
        {
            
            this.location = location;
            state = new EnemySpawnState(this, game);
            collider = new EnemyCollider();

        }

        public void Spawn()
        {
            state = new StalfosWalkingState(this, location);
            stalfosSprite = (StalfosWalkingSprite)sprite;
            collider = new EnemyCollider(stalfosSprite.GetRectangle(), state, attack);
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
        }


        
        /// <returns>true when stalfos HP is > 0</returns>
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

        public void SetSprite(ISprite sprite)
        {

            this.sprite = sprite;

        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }



    }
}
