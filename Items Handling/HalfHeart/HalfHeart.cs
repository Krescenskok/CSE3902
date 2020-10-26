using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class HalfHeart : IItems
    {
        private ISprite item;
        private int drawnFrame;

        private Vector2 location;

        private IItemsState state;

        public Vector2 Location { get => location; set => location = value; }

        public IItemsState State { get => state; set => state = value; }

        public HalfHeart(ISprite item, Vector2 location)
        {
            Location = location;
            this.item = item;
            drawnFrame = 0;
            State = new HalfHeartState(this, location);
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Update()
        {
            State.Update();
        }

        public void Expire()
        {
            State.Expire();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, Location, drawnFrame, Color.White);
        }
    }
}
