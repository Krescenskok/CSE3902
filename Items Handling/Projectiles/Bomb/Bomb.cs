using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Items
{
    public class Bomb : IItems
    {
        private Vector2 location;
        private BombCollider collider;
        private ISprite item;
        private IItemsState state;
        private int drawnFrame;
        private bool isExploding = false;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        public bool Exploding { get => isExploding; }

        public Bomb(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BombState(this, location);
            Sounds.Instance.Play("BombDrop");
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Exploded()
        {
            isExploding = true;
            state = new BombExplosionState(this);
            collider = new BombCollider((item as ExplosionSprite).Hitbox, this, this.state);
            Update();

            Sounds.Instance.Play("BombExplode");
        }

        public void Update()
        {
            state.Update();
            if (isExploding)
            {
                collider.Update(this, this.state);
            }
            
        }

        public void Expire()
        {
            IsExpired = true; 

            CollisionHandler.Instance.RemoveCollider(collider);

            UpdateSprite(ItemsFactory.Instance.EraseSprite());
        }

        public void Collect()
        {
            state.Collected();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
