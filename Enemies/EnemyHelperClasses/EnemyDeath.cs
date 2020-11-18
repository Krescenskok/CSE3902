using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sprint4
{
    public class EnemyDeath
    {
        private EnemyDeathSprite sprite;
        private Vector2 location;
        public EnemyDeath(Vector2 location)
        {
            this.location = location;
            sprite = (EnemyDeathSprite)EnemySpriteFactory.Instance.CreateDyingSprite();
        }


        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }

        public void Update()
        {
            if (sprite.Done()) RoomEnemies.Instance.Destroy(this);
        }
            
    }
}
