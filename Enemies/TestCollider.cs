using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class TestCollider : ICollider
    {

        private Rectangle bounds;

        private ColliderVisualSprite visual;
        

        public TestCollider(Point location, Point size, Game game)
        {
            bounds = new Rectangle(location, size);
            CollisionHandler.Instance.AddCollider(this);
            visual = new ColliderVisualSprite(game, size.ToVector2());
        }

        public void Draw(SpriteBatch batch)
        {
            visual.Draw(batch, bounds.Location.ToVector2());
        }
        
        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Wall";
        }

        
        public bool Equals(ICollider col)
        {

            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            //do nothing
        }

        public void SendMessage(string msg, object value)
        {
            //do nothing
        }
    }
}
