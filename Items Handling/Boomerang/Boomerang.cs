using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items
{
    public class Boomerang : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int currentFrame = 0;
        private int drawnFrame;
        private IItemsState state;
        private bool throwing;

        public Boomerang(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BoomerangState(this, location, direction);
            throwing = false;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void ToggleBoomerangThrow()
        {
            throwing = !throwing;
        }

        public void Update()
        {
            ToggleBoomerangThrow();
            state.Update();
            ToggleBoomerangThrow();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
