using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public interface IEnemy
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
