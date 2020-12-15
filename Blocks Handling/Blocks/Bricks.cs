using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Blocks
{
    public class Bricks : IBlock
    {
        private Vector2 spriteLocation;
       
        private Boolean moveable;
        private int SHEET_LOCATION = 8;
        private int currentFrame;
        private int drawnFrame;
        private BlockCollider collider;

        public Bricks(BlocksSprite block, Vector2 location)
        {
            spriteLocation = location;
            
            moveable = false;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;

            collider = new BlockCollider(block.getDestination(location), this);
        }
        public Bricks(BlocksSprite block, Vector2 location, string orientation, int length)
        {
            spriteLocation = location;
            moveable = false;
            currentFrame = 0;
            drawnFrame = SHEET_LOCATION;
            Rectangle hitbox = block.getDestination(location);
            Point tileSize = GridGenerator.Instance.GetTileSize();
            int size = orientation == "Vertical" ? tileSize.Y : tileSize.X;
            if (orientation == "Vertical") hitbox.Height = size * length;
            else hitbox.Width = size * length;
            collider = new BlockCollider(hitbox, this);
        }
        

        public void Update()
        {

        }

        public void move(string compare)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
           

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
            return new Rectangle(spriteLocation.ToPoint(),new Point());
        }
        public Boolean getMoveable()
        {
            return moveable;
        }

        public BlocksSprite getBlockSprite()
        {
            return null;
        }
    }
}
