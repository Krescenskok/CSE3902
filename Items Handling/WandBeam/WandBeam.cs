using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class WandBeam : IItems
    {
        private Vector2 location;
        public bool expired = false;
        private ISprite item;

        private int drawnFrame;
        private IItemsState state;
        private string direction;

        public WandBeam(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            this.direction = direction;
            this.item = item;
            drawnFrame = 0;
            state = new WandBeamState(this, location, direction);
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
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
            expired = true;
            state.Expire();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }


    }
}
