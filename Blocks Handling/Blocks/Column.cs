using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Blocks
{
    public class Column : IBlock
    {
        private Vector2 spriteLocation;
        private BlocksSprite block;
        private Boolean moveable;
        private int SHEET_LOCATION = 1;
        private int currentFrame;
        private int drawnFrame; 
        private BlockCollider collider;

        public Column(BlocksSprite block, Vector2 location)
        {
            spriteLocation = location;
            this.block = block;
            moveable = false;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;
            collider = new BlockCollider(block.getDestination(location), this);
        }

        public void Update()
        {
           
        }

        public void move(string compare)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            block.Draw(spriteBatch, spriteLocation, drawnFrame);

        }

        public void setLocation(Vector2 location)
        {
            spriteLocation = location;
        }

        public Vector2 GetLocation()
        {
            return spriteLocation;
        }

        public Boolean GetMoveable()
        {
            return moveable;
        }

        public Rectangle getDestination()
        {
            return this.block.getDestination();
        }

        public Boolean getMoveable()
        {
            return moveable;
        }

        public BlocksSprite getBlockSprite()
        {
            return block;
        }
    }
}
