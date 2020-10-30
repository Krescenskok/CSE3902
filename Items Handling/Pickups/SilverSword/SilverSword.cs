using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint4.Items
{
    public class SilverSword : IItems
    {
        private Vector2 location;
        private ItemCollider collider;
        private ISprite item;
        private IItemsState state;
        private XElement saveInfo;

        private int drawnFrame;

        public ICollider Collider { get => Collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state;}

        public SilverSword(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new SilverSwordState(this, location);
            collider = new ItemCollider((item as SilverSwordSprite).Hitbox, this, this.state);
        }
        public SilverSword(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new SilverSwordState(this, location);
            collider = new ItemCollider((item as SilverSwordSprite).Hitbox, this, this.state);
            saveInfo = xml;
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
            saveInfo.SetElementValue("Alive", "false");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, Location, drawnFrame, Color.White);
        }
    }
}
