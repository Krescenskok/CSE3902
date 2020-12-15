using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5.Items
{
    public class BoomerangImpact : IItems
    {
        private const int ADJUSTX = 10;
        private const int ADJUSTY = 5;
        private Vector2 linkLocation;
        private Vector2 location;


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

        public BoomerangImpact(ISprite item, Vector2 location, Rectangle rectangle)
        {
            this.location = location;
            AdjustLocation(rectangle);
            this.item = item;
            drawnFrame = 0;
            state = new BoomerangImpactState(this, this.location);
        }

        private void AdjustLocation(Rectangle rectangle)
        {
            location.X = rectangle.Location.X + ADJUSTX;
            location.Y = rectangle.Location.Y + ADJUSTY;
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
