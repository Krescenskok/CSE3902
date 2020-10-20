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
        private GoriyaWalkSprite gorSprite;
        private ISprite boomerang;
        private bool throwBoomerang;

        private GoriyaBoomerang boomy;

        private Game game;


        private EnemyCollider collider;

        private const int attack = 5;

        public void SetSprite(ISprite sprite)
        {

            this.sprite = sprite;

        }

        public void SetBoomerang(GoriyaBoomerang boomerang)
        {
            boomy = boomerang;
            
        }

        public GoriyaBoomerang GetBoomerang()
        {
            return boomy;
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

            boomerang = EnemySpriteFactory.Instance.CreateBoomerangSprite();

            collider = new EnemyCollider();
          
            throwBoomerang = false;
        }
        public void Spawn()
        {
            state = new GoriyaMoveState(this, location);
            gorSprite = (GoriyaWalkSprite)sprite;
            collider = new EnemyCollider(gorSprite.GetRectangle(), state, attack);
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
            if (boomy != null) boomy.Draw(spriteBatch);
        }

        public void Update()
        {
           
            state.Update();
            collider.Update(location.ToPoint());
            if (boomy != null) boomy.Update();
        }


        
    }
}
