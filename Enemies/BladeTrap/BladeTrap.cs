using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;
using System.Collections.Generic;

using System.Text;

namespace Sprint3
{
    /// <summary>
    /// Author: JT Thrash
    /// </summary>
    public class BladeTrap : IEnemyNPC
    {

        private Vector2 location;
        
        public IEnemyState state;
        private ISprite sprite;

        public BladeTrap(Vector2 location, Vector2 target)
        {
            
            state = new BladeTrapRestState(location,target,this);
            sprite = EnemySpriteFactory.Instance.CreateBladeTrapSprite();
        }

        public void Update()
        {
            state.Update();
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Draw(SpriteBatch batch, GameTime time)
        {
            sprite.Draw(batch, location, 0, Color.White);
        }
    }
}
