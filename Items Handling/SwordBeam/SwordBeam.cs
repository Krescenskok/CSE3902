using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2
{
    public class SwordBeam : IItems
    {
        private Vector2 location;
        private ISprite item;
        private string direction;
        private int drawnFrame;
        private IItemsState state;
        
        private float xPos;
        private float yPos;
        private float initialY;

        public SwordBeam(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            xPos = location.X;
            yPos = location.Y;
            initialY = yPos;
            this.item = item;
            state = new SwordBeamState(this, location, direction);
            this.direction = direction;
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Update()
        {
            state.Update();
        }

        public void SwordImpact()
        {
            state = new BeamImpactState(this, location);
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
