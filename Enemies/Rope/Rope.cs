using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4;
using Sprint4.Enemies;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Sprint4
{
    public class Rope : IEnemy
    {

        private XElement saveInfo;

        private Game game;

        private ISprite sprite;
        private RopeMoveSprite rp;
        private IEnemyState state;
        private Vector2 location;
       
        private EnemyCollider collider;
        private RopePlayerFinderCollider finderCollider;
        private Direction currentDirection = Direction.right;
        public Direction direction { get => currentDirection; }

        private int HP = HPAmount.EnemyLevel1;

        public Vector2 Location { get => location; }

        public IEnemyState State { get => state; }
        public List<ICollider> Colliders { get => new List<ICollider> { collider,finderCollider }; }

        public Rope(Game game, Vector2 location, XElement xml)
        {
            this.location = location;
            this.game = game;
            state = new EnemySpawnState(this,game);

            collider = new EnemyCollider();
            finderCollider = new RopePlayerFinderCollider(this);

            saveInfo = xml;

            
        }

        public void Spawn()
        {
            state = new RopeMoveState(this, location, game);
            rp = (RopeMoveSprite)sprite;
            collider = new EnemyCollider(rp.GetRectangle(), this, HPAmount.HalfHeart);
            finderCollider = new RopePlayerFinderCollider(rp.GetRectangle(), this, game);
        }

        public void Update()
        {
            state.Update();
           
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
            
        }

        public void Draw(SpriteBatch batch)
        {

            sprite.Draw(batch, location, 0, Color.White);

            
        }



        public void Die()
        {
            RoomEnemies.Instance.Destroy(this,location);
            
            saveInfo.SetElementValue("Alive", "false");
            RoomItems.Instance.DropRandom(location);
        }


        public void UpdateDirection(Direction dir)
        {
            currentDirection = dir;
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
            state.Stun();
        }
    }
}
