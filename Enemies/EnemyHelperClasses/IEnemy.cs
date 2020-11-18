using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public interface IEnemy : IMoveable
    {

        void Update();
        void Draw(SpriteBatch spriteBatch);

        void SetSprite(ISprite sprite);

        void Spawn();

        
        void TakeDamage(Direction dir, int amount);
        void ObstacleCollision(Collision collision);

        void Stun();

       
        List<ICollider> Colliders { get; }
        

        new Vector2 Location { get; }

        IEnemyState State { get; }

    }
}
