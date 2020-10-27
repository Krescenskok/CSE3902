using Microsoft.Xna.Framework;
using Sprint3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3.Blocks
{
    public class BlockCollider : ICollider
    {
        private Rectangle bounds;
        private IBlock block;

        public string Name => throw new NotImplementedException();

        
        public Layer layer { get; set; }

        public BlockCollider(Rectangle rect, IBlock block)
        {
            bounds = rect;

            this.block = block;

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
            if (col.CompareTag("Player") && block.GetMoveable())
            {
                block.move(collision.From().ToString());
                col.SendMessage("Special Block", null);
            }
        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {

        }

        public void SendMessage(string msg, object value)
        {
            
        }

        public void Update()
        {
            bounds = block.getDestination();
        }
    }
}
