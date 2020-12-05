using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Blocks
{
    public class MoveableUp : IBlock
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

        XElement saveFile;
        
        public MoveableUp(BlocksSprite block, Vector2 location, XElement file)
        {
            spriteLocation = location;
            this.block = block;
            saveFile = file;
            moveable = file.Element("Moved").Value == "false";
            if (!moveable) spriteLocation = new Vector2(spriteLocation.X, spriteLocation.Y - this.block.blockDimensionY);
            shift = 0;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;
            collider = new BlockCollider(block.getDestination(location), this);
        }

        public void Update()
        {
            if (shift > 0) {
                if (shift >= 5) {
                    newLocation = new Vector2(spriteLocation.X, spriteLocation.Y - 5);
                    spriteLocation = newLocation;
                    shift -= 5;
                }
                else
                {
                    newLocation = new Vector2(spriteLocation.X, spriteLocation.Y - shift);
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
            if (moveable && compare.Equals("down"))
            {
                moveable = false;
                saveFile.SetElementValue("Moved", "true");
                shift = block.blockDimensionY;
            }
        }


        public Boolean GetMoveable()
        {
            return moveable;
        }

        public Rectangle getDestination()
        {
            return block.getDestination();
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
