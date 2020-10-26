using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class TriforcePiece : IItems
    {
        private Vector2 location;
        private ItemCollider collider;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private XElement saveInfo;

        public ICollider Collider { get => collider;}
        public Vector2 Location { get => location;}

        public IItemsState State { get => state;}

        public TriforcePiece(ISprite item, Vector2 location)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new TriforcePieceState(this, location);
            collider = new ItemCollider();
        }

        public TriforcePiece(ISprite item, Vector2 location, XElement xml)
        {
            this.location = location;
            this.item = item;
            drawnFrame = 0;
            state = new TriforcePieceState(this, location);
            collider = new ItemCollider();
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
            saveInfo.SetElementValue("Alive", "false");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
    }
}
