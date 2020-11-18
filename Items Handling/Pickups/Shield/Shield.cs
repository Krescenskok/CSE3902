using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Items
{
    public class Shield : IItems
    {
        private Vector2 location;
        private ItemCollider collider;
        private ISprite item;
        private IItemsState state;
        private XElement saveInfo;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        private int drawnFrame;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location;}

        public IItemsState State { get => state;}

        public Shield(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new ShieldState(this, location);
            collider = new ItemCollider((item as ShieldSprite).Hitbox, this, this.state);
        }
        public Shield(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new ShieldState(this, location);
            collider = new ItemCollider((item as ShieldSprite).Hitbox, this, this.state);
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
        
        public void Collect()
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
