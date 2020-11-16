using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;

namespace Sprint4.Items
{
    public class BoomerangImpact : IItems
    {
        public Vector2 location
        {
            get { return location; }
            set { location = value; }
        }


        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public Vector2 Location => throw new NotImplementedException();

        public IItemsState State => throw new NotImplementedException();

        public ICollider Collider => throw new NotImplementedException();

        private ISprite item;
        private int drawnFrame;
        private IItemsState state;

        public BoomerangImpact(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BoomerangImpactState(this, location);
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
            IsExpired = true;
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
