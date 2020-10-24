using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class SilverSword : IItems
    {
        private Vector2 location;
        private ISprite item;
        private IItemsState state;

        private int drawnFrame;

        public Vector2 Location { get => location; set => location = value; }

        public SilverSword(ISprite item, Vector2 location)
        {
            this.Location = location;
            this.item = item;
            drawnFrame = 0;
            state = new SilverSwordState(this, location);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, Location, drawnFrame, Color.White);
        }
    }
}
