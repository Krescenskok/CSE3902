using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3
{
    public class SwordBeam : IItems
    {
        private Vector2 location;
        private ProjectileCollider collider;
        private bool isExpired = false;
        public bool expired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }
        private ISprite item;
        private string direction;
        private int drawnFrame;
        private IItemsState state;

        private float xPos;
        private float yPos;
        private float initialY;

        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        public SwordBeam(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            xPos = location.X;
            yPos = location.Y;
            initialY = yPos;
            this.item = item;
            state = new SwordBeamState(this, location, direction);
            this.direction = direction;

            if (direction == "Down")
            {
                collider = new ProjectileCollider((item as DownBeamSprite).Hitbox, this, this.state, "SwordBeam");
            }
            else if (direction == "Up")
            {
                collider = new ProjectileCollider((item as UpBeamSprite).Hitbox, this, this.state, "SwordBeam");
            }
            else if (direction == "Left")
            {
                collider = new ProjectileCollider((item as LeftBeamSprite).Hitbox, this, this.state, "SwordBeam");
            } else if (direction == "Right")
            {
                collider = new ProjectileCollider((item as RightBeamSprite).Hitbox, this, this.state, "SwordBeam");
            }

        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Update()
        {
            state.Update();
        }

        public void SwordImpact()
        {
            state = new BeamImpactState(this);
            Update();
        }

        public void Expire()
        {
            state.Expire();
        }

        public void Collect()
        {
            state.Collected();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }

    }
}
