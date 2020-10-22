using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// <para>Big hand that lurks behind walls, then comes out to grab Link.</para>
    /// </summary>
    public class WallMaster : IEnemy
    {
        public Vector2 location { get; set; }

        public Vector2 Location => throw new NotImplementedException();

        public IEnemyState State => throw new NotImplementedException();

        public IEnemyState state;
        private ISprite sprite;

        public WallMaster(Game game, Vector2 location)
        {
            this.location = location;
            
            state = new WallMasterMoveState(location,game, this);
        }

        public void SetSprite(ISprite sprite)
        {
            this.sprite = sprite;
        }
        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Draw(SpriteBatch batch)
        {
            sprite.Draw(batch, location, 0 , Color.White);
        }

        public void Update()
        {
            state.Update();
        }

        public void Spawn()
        {
            throw new NotImplementedException();
        }

        public EnemyCollider GetCollider()
        {
            throw new NotImplementedException();
        }
    }
}
