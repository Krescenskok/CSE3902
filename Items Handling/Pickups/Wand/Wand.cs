using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint4.Items
{
    public class Wand : IItems
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


        public ICollider Collider { get => collider;}

        public Vector2 Location { get => location;}

        public IItemsState State { get => state;}

        public Wand(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new WandState(this, location);
            collider = new ItemCollider((item as WandSprite).Hitbox, this, this.state);
        }
        public Wand(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new WandState(this, location);
            collider = new ItemCollider((item as WandSprite).Hitbox, this, this.state);
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
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
