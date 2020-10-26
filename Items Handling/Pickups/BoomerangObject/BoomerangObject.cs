using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Link;

namespace Sprint3.Items
{
    public class BoomerangObject : IItems
    {
        private XElement saveInfo;
        private ItemCollider collider;
        private Vector2 location;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private bool throwing;
        private bool returning;
        private LinkPlayer link;
        private string direction;

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public BoomerangObject(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BoomerangObjectState(this, location);
            throwing = true;

            returning = false;
            collider = new ItemCollider((item as BoomerangObjectSprite).Hitbox, this, this.state);
        }
        public BoomerangObject(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new BoomerangObjectState(this, location);
            throwing = true;
            returning = false;
            collider = new ItemCollider((item as BoomerangObjectSprite).Hitbox, this, this.state);
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
            state.Collected();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
