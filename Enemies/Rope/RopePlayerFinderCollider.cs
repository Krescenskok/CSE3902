using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint3
{
    public class RopePlayerFinderCollider : ICollider
    {
        public string Name { get => "RopeEyes"; }
        public Layer layer { get; set; }

        private Rectangle bounds;
        private Rope rope;
        private RopeMoveState state;
        private Rectangle ropeSize;
        private string currentDirection;

        private int verticalReach;
        private int horizontalReach;

        private Vector2 locationOffset;

        public RopePlayerFinderCollider(Rectangle ropeRect, Rope rope, Game game)
        {

            this.rope = rope;
            currentDirection = "right";

            ropeSize = ropeRect;

            verticalReach = game.Window.ClientBounds.Height;
            horizontalReach = game.Window.ClientBounds.Width;

            locationOffset = Vector2.Zero;

            state = (RopeMoveState)rope.State;

            CollisionHandler.Instance.AddCollider(this);
        }

        public RopePlayerFinderCollider(Rope rope)
        {
            this.rope = rope;
        }
        public Rectangle Bounds()
        {
            return bounds;
        }

        public void Update(string dir)
        {
            if (!dir.Equals(currentDirection)) CalculateBounds(dir);
            bounds.Location = (rope.Location - locationOffset).ToPoint();
           
        }

        private void CalculateBounds(string dir)
        {
            if (dir.Equals("right"))
            {
                bounds.Height = ropeSize.Height;
                bounds.Width = horizontalReach;
                bounds.Location = rope.Location.ToPoint();
            }else if (dir.Equals("left"))
            {
                bounds.Height = ropeSize.Height;
                bounds.Width = horizontalReach;
                bounds.Location = rope.Location.ToPoint();
                bounds.X = bounds.X - horizontalReach;
            }else if (dir.Equals("up"))
            {
                bounds.Height = verticalReach;
                bounds.Width = ropeSize.Width;
                bounds.Location = rope.Location.ToPoint();
                bounds.Y = bounds.Y - verticalReach;
            }
            else
            {
                bounds.Height = verticalReach;
                bounds.Width = ropeSize.Width;
                bounds.Location = rope.Location.ToPoint();
            }

            locationOffset = rope.Location - bounds.Location.ToVector2();

            currentDirection = dir;
        }

        public bool CompareTag(string tag)
        {
            return tag == Name;
        }

        public bool Equals(ICollider col)
        {
            return col == this;
        }

        public void HandleCollision(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") || col.Name.Equals("gel")) state.Attack();


        }

        public void HandleCollisionEnter(ICollider col, Collision collision)
        {
            if (col.CompareTag("Player") || col.Name.Equals("gel")) state.Attack();
           
        }

        public void SendMessage(string msg, object value)
        {
           //does not handle messages
        }
    }
}
