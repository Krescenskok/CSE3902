﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3
{
    public class TestCollider : ICollider
    {

        private Rectangle bounds;

        private ColliderVisualSprite visual;
        private Vector2 location;

        private int attack;

        public string Name { get => "wall"; }

        public TestCollider(Point location, Point size, Game game, int attack)
        {
            bounds = new Rectangle(location, size);
            CollisionHandler.Instance.AddCollider(this);
            visual = new ColliderVisualSprite(game, size.ToVector2());
            this.location = location.ToVector2();
            this.attack = attack;
        }

        public TestCollider(Rectangle rect, Game game)
        {
            bounds = rect;
            visual = new ColliderVisualSprite(game, rect.Size.ToVector2());
            this.location = rect.Location.ToVector2();
            attack = 0;

            CollisionHandler.Instance.AddCollider(this);
        }

        public TestCollider()
        {

        }

        public void Draw(SpriteBatch batch)
        {
            visual.Draw(batch, location);
        }

        public void UpdateLocation(Vector2 loc)
        {
            location = loc;
        }
        
        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "Wal" || tag == "wal";
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

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("enemy"))
            {
                col.SendMessage("TakeDamage", attack);
            }
        }
    }
}
