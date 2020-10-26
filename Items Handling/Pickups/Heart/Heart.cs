using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    //this is the heart that link can collect to replenish hearts
    public class Heart : IItems
    {

        private ItemCollider heartCollider;
        private ISprite item;
        private int drawnFrame;

        private Vector2 location;

        private IItemsState state;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public Heart(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new HeartState(this, location);
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void Update()
        {
            state.Update();
        }

        public void Expire()
        {
            state.Expire();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
