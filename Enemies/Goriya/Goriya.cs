using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint2Final
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Monster that throws boomerangs</para>
    /// </summary>
    public class Goriya : IEnemy
    {

        private Vector2 location;
       

        public IEnemyState state;
        private ISprite sprite;
        private GoriyaWalkSprite gorSprite;
        

        private GoriyaBoomerang boomy;

        private Game game;


        private EnemyCollider collider;

        private const int attack = 5;
        private int HP = 15;

        public Vector2 Location => throw new NotImplementedException();

        public IEnemyState State => throw new NotImplementedException();

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

        public EnemyCollider Collider()
        {
            return collider;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
            
        }


        public Goriya(Game game, Vector2 location)
        {
            this.game = game;
            this.location = location;
            state = new EnemySpawnState(this,game);
            
            collider = new EnemyCollider();
          
        }
        public void Spawn()
        {
            state = new GoriyaMoveState(this, location);
            gorSprite = (GoriyaWalkSprite)sprite;
            collider = new EnemyCollider(gorSprite.GetRectangle(), state, attack);
        }


        public void Die()
        {
            RoomEnemies.Instance.Destroy(this,location);
        }

        public void TakeDamage(int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
            
            if (boomy != null) boomy.Draw(spriteBatch);
        }

        public void Update()
        {
            state.Update();
            collider.Update(this);
            if (boomy != null) boomy.Update();
        }

        public EnemyCollider GetCollider()
        {
            throw new NotImplementedException();
        }
    }
}
