using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sprint2Final.Items
{
    public class Compass : IItemsState
    {
        private Vector2 spriteLocation;
        private ISprite item;
        private int SHEET_LOCATION = 22;
        
        private int drawnFrame; 

        public Compass(ISprite item)
        {
            spriteLocation = new Vector2(100, 50);
            this.item = item;
            
            drawnFrame = SHEET_LOCATION; 
        }

        public void Update()
        {
            //no animation - no update
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
