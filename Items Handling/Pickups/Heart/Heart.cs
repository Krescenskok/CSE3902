using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Linq;

namespace Sprint5.Items
{
    //this is the heart that link can collect to replenish hearts
    public class Heart : IItems
    {

        private XElement saveInfo;
        private HealthCollider collider;
        private ISprite item;
        private int drawnFrame;

        private Vector2 location;

        private IItemsState state;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public Heart(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new HeartState(this, location);
            collider = new HealthCollider((item as HeartSprite).Hitbox, this, this.state, "Heart");
        }
        public Heart(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new HeartState(this, location);
            collider = new HealthCollider((item as HeartSprite).Hitbox, this, this.state, "Heart");

            saveInfo = xml;
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
            //saveInfo.SetElementValue("Alive", "false");
            state.Expire();
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
