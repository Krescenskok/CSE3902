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

        EnemyCollider GetCollider();

        Vector2 Location { get; }

        IEnemyState State { get; }

        
        
    }
}
