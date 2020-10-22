using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Final.Items.States
{
    public class RedRing : IItemsState
    {
        private Vector2 spriteLocation;
        private ISprite item;
        private int SHEET_LOCATION = 10;
        
        private int drawnFrame; 

        public RedRing(ISprite item)
        {
            spriteLocation = new Vector2(100, 50);
            this.item = item;
            
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
           
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            item.Draw(spriteBatch, spriteLocation, drawnFrame, Color.White);

        }

        public Vector2 GetLocation()
        {
            return spriteLocation;
        }

    }
}
