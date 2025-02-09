﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint5
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class BladeTrap : IEnemy
    {

        private Vector2 location;
        public Vector2 Location { get => location; }
        public int HP { get; private set; }


        
        public IEnemyState state;
        public IEnemyState State { get => state; }

        public List<ICollider> Colliders { get => new List<ICollider> { collider }; }

        private EnemySprite sprite;
        private BladeTrapSprite bladeSprite;

        private EnemyCollider collider;

        private string direction1, direction2;

        public BladeTrap(Game game, Vector2 location, string dir1, string dir2)
        {
            direction1 = dir1;
            direction2 = dir2;

            state = new EnemySpawnState(this,game);

            this.location = location;

            collider = new EnemyCollider();
        }

        public void Spawn()
        {
            state = new BladeTrapRestState(location,this);
            
            bladeSprite = (BladeTrapSprite)sprite;
            Rectangle objectSize = bladeSprite.GetRectangle();
            objectSize.Location = location.ToPoint();
            collider = new EnemyCollider(HitboxAdjuster.Instance.AdjustHitbox(objectSize, .6f), this, HPAmount.HalfHeart, "Trap");


            int horizontalAttackRange = GridGenerator.Instance.GetGridWidth();
            int verticalAttackRange = GridGenerator.Instance.GetGridHeight();

            int attackRange1, attackRange2;
            

            if (direction1.Equals("right") || direction1.Equals("left")) attackRange1 = horizontalAttackRange;
            else attackRange1 = verticalAttackRange;

            if (direction2.Equals("right") || direction2.Equals("left")) attackRange2 = horizontalAttackRange;
            else attackRange2 = verticalAttackRange;


            CollisionHandler.Instance.AddCollider(new TrapCollider(objectSize, this, direction1, attackRange1), Layers.Trigger);
            CollisionHandler.Instance.AddCollider(new TrapCollider(objectSize, this, direction2, attackRange2), Layers.Trigger);
        }
        

        public void Update()
        {
            state.Update();
            sprite.Update();
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }

        public void SetSprite(EnemySprite sprite)
        {
            this.sprite = sprite;
        }


        public EnemyCollider GetCollider()
        {
            return collider;
        }

        public void TakeDamage(Direction dir, int amount)
        {
            //cannot be damaged
        }

        public void ObstacleCollision(Collision collision)
        {
            state.MoveAwayFromCollision(collision);
        }

        public void Stun()
        {
            //cannot be stunned
        }

        public void Attack()
        {
          
        }
    }
}
