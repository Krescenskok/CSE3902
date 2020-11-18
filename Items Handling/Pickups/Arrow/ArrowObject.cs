using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    //class for the arrow object drawn in the shop/Link's inventory
    public class ArrowObject : IItems
    {
   
        private ItemCollider collider;
        private XElement saveInfo;
        private Vector2 location;
        private ISprite item;
        private int drawnFrame = 0;
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

        public ArrowObject(ISprite item, Vector2 location)
        {
            this.location = location;
            state = new ArrowState(this);
            collider = new ItemCollider((item as ArrowUpSprite).Hitbox, this, this.state);
        }
        public ArrowObject(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            state = new ArrowState(this);
            collider = new ItemCollider((item as ArrowSprite).Hitbox, this, this.state);
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

        public void Expire()
        {
            state.Expire();
        }

        public void Update()
        {
            saveInfo.SetElementValue("Alive", "false");
        }

        public void Collect()
        {
            state.Collected();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (item is null)
                item = ItemsFactory.Instance.CreateArrowSprite("Up");
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
        
    }
}
