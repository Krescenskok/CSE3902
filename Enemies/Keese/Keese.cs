using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
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

        private int HP = HPAmount.EnemyLevel1;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }

        private XElement saveData;

        public Keese(Game game, Vector2 location, XElement xml)
        {
            this.game = game;
            this.location = location;
            state = new EnemySpawnState(this, game);

            collider = new EnemyCollider();

            saveData = xml;
        }

        public void Spawn()
        {
            
            state = new KeeseMoveState(this, location);
            KeeseMoveSprite kSprite = (KeeseMoveSprite)sprite;
            collider = new EnemyCollider(kSprite.GetRectangle(), this,HPAmount.HalfHeart,"Keese");

        }
        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }
     

        public void Die()
        {
            RoomEnemies.Instance.Destroy(this, location);
            saveData.SetElementValue("Alive", "false");
        }

    

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, location, 0, Color.White);
        }

        public void Update()
        {
            
            state.Update();
            
        }

  

        public void TakeDamage(Direction dir, int amount)
        {
            HP -= amount;
            if (HP <= 0) Die();
        }

        public void ObstacleCollision(Collision collision)
        {
            state.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            Die();
        }
    }
}
