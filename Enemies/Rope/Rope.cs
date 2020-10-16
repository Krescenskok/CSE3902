using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final;
using Sprint2Final.Enemies;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sprint3
{
    public class Rope : IEnemy
    {

        private ISprite sprite;
        private RopeMoveSprite rp;
        private IEnemyState state;
        private Vector2 location;
       
        private EnemyCollider collider;

        private const float strength = 50;

        public Rope(Game game, Vector2 location)
        {
            this.location = location;
            state = new RopeMoveState(this, location,game);
            rp = (RopeMoveSprite)sprite;
            collider = new EnemyCollider(rp.GetRectangle(),state, strength);

        }

        public void Update()
        {
            state.Update();
            collider.Update(location.ToPoint());
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

    }
}
