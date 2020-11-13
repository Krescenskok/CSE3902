using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint4.Blocks
{
    public class MoveableRight : IBlock
    {
        private Vector2 spriteLocation;
        private Vector2 newLocation;
        private BlocksSprite block;
        private int SHEET_LOCATION = 0;
        private int currentFrame;
        private Boolean moveable;
        private int drawnFrame;
        private int shift;
        private BlockCollider collider;

        public MoveableRight(BlocksSprite block, Vector2 location)
        {
            spriteLocation = location;
            this.block = block;
            moveable = true;
            shift = 0;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;
            collider = new BlockCollider(block.getDestination(location), this);
        }

        public void Update()
        {
            if (shift > 0) {
                if (shift >= 5) {
                    newLocation = new Vector2(spriteLocation.X + 5, spriteLocation.Y);
                    spriteLocation = newLocation;
                    shift -= 5;
                }
                else
                {
                    newLocation = new Vector2(spriteLocation.X + shift, spriteLocation.Y);
                    spriteLocation = newLocation;
                    shift = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            block.Draw(spriteBatch, spriteLocation, drawnFrame);

        }

        public void setLocation(Vector2 location)
        {
            spriteLocation = location;
        }

        public void move(string compare)
        {
            if (moveable && compare.Equals("left"))
            {
                moveable = false;
                shift = this.block.blockDimensionX;
            }
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
