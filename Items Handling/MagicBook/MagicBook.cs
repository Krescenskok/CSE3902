using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2.Items
{
    public class MagicBook : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;

        public MagicBook(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new MagicBookState(this, location);
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void Update()
        {
            state.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
