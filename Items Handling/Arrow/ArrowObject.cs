using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2
{
    //class for the arrow object drawn in the shop/Link's inventory
    public class ArrowObject : IItems
    {
        private Vector2 location;
        private ISprite item;
        private int drawnFrame = 0;
        private IItemsState state;

        public ArrowObject(ISprite item, Vector2 location)
        {
            this.location = location;
            state = new ArrowState(this);
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Expire()
        {
            state.Expire();
        }

        public void Update()
        {
            state.Update();
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
