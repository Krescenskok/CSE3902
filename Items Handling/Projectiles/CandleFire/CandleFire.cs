using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class CandleFire : IItems
    {
        private Vector2 location;
        private ProjectileCollider collider;
        private bool isExpired = false;
        public bool expired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }
        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        private ISprite item;
        private IItemsState state;

        private int drawnFrame;

        public CandleFire(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new CandleFireState(this, location);
            collider = new ProjectileCollider((item as CandleFireSprite).Hitbox, this, this.state, "CandleFire");
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Update()
        {
            state.Update();
        }

        public void Expire()
        {
            state.Expire();
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
