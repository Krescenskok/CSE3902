using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint4.Items
{
    public class WoodenSword : IItems
    {
        private Vector2 location;
        private ItemCollider collider;
        private ISprite item;
        private XElement saveInfo;

        private int drawnFrame;
        private IItemsState state;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public Vector2 Location { get => location;}

        public ICollider Collider { get => collider;}

        public IItemsState State { get => state;}

        public WoodenSword(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new WoodenSwordState(this, location);
            collider = new ItemCollider((item as WoodenSwordSprite).Hitbox, this, this.state);
        }
        public WoodenSword(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new WoodenSwordState(this, location);
            collider = new ItemCollider((item as WoodenSwordSprite).Hitbox, this, this.state);
            saveInfo = xml;
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
            IsExpired = false;
            saveInfo.SetElementValue("Alive", "false");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, Location, drawnFrame, Color.White);
        }
    }
}
