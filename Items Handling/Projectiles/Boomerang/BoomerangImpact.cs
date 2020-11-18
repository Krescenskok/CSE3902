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
        private const int ADJUST = 20;
        private bool returning;
        private Vector2 linkLocation;
        private Vector2 loc;
        public Vector2 location
        {
            get { return loc; }
            set { loc = value; }
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

        public BoomerangImpact(ISprite item, Vector2 location, string direction, bool returnState, LinkPlayer link)
        {
            linkLocation = link.CurrentLocation;
            this.location = location;
            AdjustLocation(direction);
            this.item = item;
            returning = returnState;
            drawnFrame = 0;
            state = new BoomerangImpactState(this, this.location);
        }

        private void AdjustLocation(string direction)
        {
            if (returning)
            {
                AdjustReturnLocation();
                return;
            }
            if (direction is "Up")
            {
                loc.Y -= ADJUST;
            }
            else if (direction is "Down")
            {
                loc.Y += ADJUST;
            }
            else if (direction is "Right")
            {
                loc.X += ADJUST;
            }
            else
            {
                loc.X -= ADJUST;
            }
        }
        private void AdjustReturnLocation()
        {
            if (location.X < linkLocation.X)
            {
                loc.X += ADJUST;
            }
            else
            {
                loc.X -= ADJUST;
            }

            if (location.Y < linkLocation.Y)
            {
                loc.Y += ADJUST;
            }
            else
            {
                loc.Y -= ADJUST;
            }
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
