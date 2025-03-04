﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.DifficultyHandling;

namespace Sprint5
{
    public interface IEnemy : IMoveable
    {

        void Update();
        void Draw(SpriteBatch spriteBatch);

        void SetSprite(EnemySprite sprite);

        void Spawn();


        
        void TakeDamage(Direction dir, int amount);
        void ObstacleCollision(Collision collision);

        void Stun();

       
        List<ICollider> Colliders { get; }
        

        new Vector2 Location { get; }

        IEnemyState State { get; }

        int HP { get; }

    }
}
