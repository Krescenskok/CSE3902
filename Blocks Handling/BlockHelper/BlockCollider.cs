using Microsoft.Xna.Framework;
using Sprint5;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5.Blocks
{
    public class BlockCollider : ICollider
    {
        private Rectangle bounds;
        private IBlock block;

        public string Name => "Block";

        public Layer layer { get; set; }

        public BlockCollider(Rectangle rect, IBlock block)
        {
            bounds = rect;

            this.block = block;

            CollisionHandler.Instance.AddCollider(this, Layers.Block);

        }

        public BlockCollider(Vector2 location)
        {
            bounds = new Rectangle(location.ToPoint(), GridGenerator.Instance.GetTileSize());
            CollisionHandler.Instance.AddCollider(this, Layers.Block);
        }

        public Rectangle Bounds()
        {
            return bounds;
        }

        public bool CompareTag(string tag)
        {
            return tag == "block" || tag == "Block";
        }

        public bool Equals(ICollider col)
        {
            return this == col;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") && block != null && block.GetMoveable())
            {
                block.move(collision.From.ToString());
                col.SendMessage("Special Block", null);
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

        }

        public void HandleCollisionExit(ICollider col, Collision collision)
        {
        }


        public void SendMessage(string msg, object value)
        {
            
        }

        public void Update()
        {
            if(block != null) bounds.Location = block.getDestination().Location;
           
        }
    }
}
