using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Small gelatinous creature that moves randomly between square spaces on the map.</para>
    /// </summary>
    public class Gel : IEnemy
    {
        private Game game;
        private Vector2 location;
        public IEnemyState state;
        private ISprite sprite;

        private EnemyCollider collider;
        private const int ATTACK_POWER = 5;

       
        public Gel(Game game, Vector2 location)
        {
            this.location = location;
            this.game = game;
            state = new EnemySpawnState(this, game);
            collider = new EnemyCollider();

        }

        public void Spawn()
        {
            state = new GelMoveState(this, location, game);
            GelMoveSprite gSprite = (GelMoveSprite)sprite;
            collider = new EnemyCollider(gSprite.GetRectangle(), state, ATTACK_POWER);
            
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }


        public void Update()
        {
            state.Update();
            collider.Update(location.ToPoint());
            
        }

        public void TakeDamage(int amount)
        {
            state.TakeDamage(amount);
        }

        public void Die()
        {
            state.Die();
        }

        public void Draw(SpriteBatch batch)
        {
            
            sprite.Draw(batch, location, 0, Color.White);
            
        }

        

        public EnemyCollider GetCollider()
        {
            throw new NotImplementedException();
        }
    }
}
