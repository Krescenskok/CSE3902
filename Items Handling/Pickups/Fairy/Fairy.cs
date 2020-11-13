using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace Sprint4.Items
{
    public class Fairy : IItems
    {
        private Vector2 location;
        private XElement saveInfo;
        private ItemCollider collider;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public Fairy(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new FairyState(this, location);
            collider = new ItemCollider((item as FairySprite).Hitbox, this, this.state);
        }

        public Fairy(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new FairyState(this, location);
            collider = new ItemCollider((item as FairySprite).Hitbox, this, this.state);
            saveInfo = xml;
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
            saveInfo.SetElementValue("Alive", "false");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
