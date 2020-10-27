using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Blocks
{
    public class BirdLeft : IBlock
    {
        private Vector2 spriteLocation;
        private BlocksSprite block;
        private Boolean moveable;
        private int SHEET_LOCATION = 2;
        private int currentFrame;
        private int drawnFrame;
        private BlockCollider collider;

        

        public BirdLeft(BlocksSprite block, Vector2 location)
        {
            spriteLocation = location;
            this.block = block;
            
            moveable = false;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;


        }


        public void Update()
        {

        }

        public void move(string compare)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch, spriteLocation, drawnFrame);
            if (collider == null)
            {
                collider = new BlockCollider(block.getDestination(spriteLocation), this);
                
            }
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
