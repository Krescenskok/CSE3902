using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Link;

namespace Sprint3.Items
{
    public class BoomerangObject : IItems
    {

        private ItemCollider boomerangObjectCollider;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private bool throwing;
        private bool returning;
        private LinkPlayer link;
        private string direction;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public BoomerangObject(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BoomerangObjectState(this, location);
            throwing = true;
            returning = false;
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
