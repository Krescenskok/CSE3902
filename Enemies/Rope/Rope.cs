using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Sprint2
{
    public class Rope
    {

        private ISprite sprite;
        private IEnemyState state;
        private Vector2 location;
        private Game game;


        public Rope(Game game, Vector2 location)
        {
            this.game = game;
            this.location = location;
        }

        public void Update()
        {
            state.Update();
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Draw(SpriteBatch batch)
        {

            sprite.Draw(batch, location, 0, Color.White);
        }

    }
}
