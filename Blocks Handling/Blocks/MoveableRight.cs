﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Blocks
{
    public class MoveableRight : IBlock
    {
        private Vector2 spriteLocation;
        private Vector2 newLocation;
        private BlocksSprite block;
        private int SHEET_LOCATION = 1;
        private int currentFrame;
        private Boolean moveable;
        private int drawnFrame;
        private int shift;
        private BlockCollider collider;

        private bool shifting = false;

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

        public MoveableRight(BlocksSprite block, Vector2 location, Boolean moved)
        {
            spriteLocation = location;
            this.block = block;
            moveable = !moved;
            if (moved) spriteLocation = new Vector2(spriteLocation.X + this.block.blockDimensionX, spriteLocation.Y);
            shift = 0;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;
            collider = new BlockCollider(block.getDestination(location), this);
        }

        public void Update()
        {
            if (shift > 0) {
                shifting = true;
                
                newLocation = new Vector2(spriteLocation.X + 5, spriteLocation.Y);
                spriteLocation = newLocation;
                shift -= 5;
                
            }else if (shifting)
            {
                newLocation = new Vector2(spriteLocation.X + shift, spriteLocation.Y);
                spriteLocation = newLocation;
                shift = 0;

                RoomDoors.Instance.OpenDoor(9);

                shifting = false;
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
                RoomBlocks.Instance.movedRight = true;
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
