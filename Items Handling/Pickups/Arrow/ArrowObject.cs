using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3
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

        public ICollider Collider { get => collider; }

        public Vector2 Location { get => location; }

        public IItemsState State { get => state; }

        public ArrowObject(ISprite item, Vector2 location)
        {
            Location = location;
            state = new ArrowState(this);
            arrowCollider = new ItemCollider(flameSprite.getRectangle(initialPos));
        }

        public Vector2 Location // read-write instance property
        {
            get => _name;
            set => _name = value;
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
