using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Enemies;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint4
{
    public class TestCollider : ICollider
    {

        private Rectangle bounds;

        private ColliderVisualSprite visual;
        private Vector2 location;

        private int attack;

        public string Name { get; set; }
        public Layer layer { get; set; }

        PlayerCollider player;
        private EnemyCollider enemyCol;

        public TestCollider(Point location, Point size, Game game, int attack)
        {
            bounds = new Rectangle(location, size);
            CollisionHandler.Instance.AddCollider(this,Layers.Default);
            visual = new ColliderVisualSprite(game, size.ToVector2());
            this.location = location.ToVector2();
            this.attack = attack;
        }

        public TestCollider(Rectangle rect, Game game, EnemyCollider col)
        {
            enemyCol = col;
            bounds = col.Bounds();
            visual = new ColliderVisualSprite(game, col.Bounds().Size.ToVector2());
            this.location = col.Bounds().Location.ToVector2();
            attack = 0;
            this.Name = col.Name;

            CollisionHandler.Instance.AddCollider(this,Layers.Trigger);
        }

        public TestCollider(Game game, PlayerCollider player)
        {
            bounds = player.Bounds();
            visual = new ColliderVisualSprite(game, player.Bounds().Size.ToVector2());
            this.location = player.Bounds().Location.ToVector2();
            attack = 0;
            this.Name = "player";
            

            this.player = player;

            CollisionHandler.Instance.AddCollider(this, Layers.Trigger);

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
            return tag == "Wall" || tag == "wal";
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

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void Update()
        {
            if (player != null)
            {
                bounds = player.Bounds();
                location = bounds.Location.ToVector2();
              
            }
            if (enemyCol != null)
            {
                bounds = enemyCol.Bounds();
                location = bounds.Location.ToVector2();
            }

            
        }
    }
}
