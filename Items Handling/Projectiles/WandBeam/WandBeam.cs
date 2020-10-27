using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Items
{
    public class WandBeam : IItems
    {
        private Vector2 location;
        private ProjectileCollider collider;
        private bool isExpired = false;
        public bool expired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }
        private ISprite itemSprite;

        private int drawnFrame;
        private IItemsState state;
        private string direction;

        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        public WandBeam(ISprite itemSprite, Vector2 location, string direction)
        {
            this.location = location;
            this.direction = direction;
            this.itemSprite = itemSprite;
            drawnFrame = 0;
            state = new WandBeamState(this, location, direction);
        

            if (direction == "Down")
            {
                collider = new ProjectileCollider((itemSprite as WandBeamDownSprite).Hitbox, this, this.state, "WandBeam");
            }
            else if (direction == "Up")
            {
                collider = new ProjectileCollider((itemSprite as WandBeamUpSprite).Hitbox, this, this.state, "WandBeam");
            }
            else if (direction == "Left")
            {
                collider = new ProjectileCollider((itemSprite as WandBeamLeftSprite).Hitbox, this, this.state, "WandBeam");
            }
            else if (direction == "Right")
            {
                collider = new ProjectileCollider((itemSprite as WandBeamRightSprite).Hitbox, this, this.state, "WandBeam");
            }
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.itemSprite = sprite;
        }

        public void UpdateFrame(int frame)
        {
            this.drawnFrame = frame;
        }

        public void Update()
        {
            state.Update();
        }

        public void Expire()
        {
            expired = true;
            state.Expire();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            itemSprite.Draw(spriteBatch, location, drawnFrame, Color.White);
        }


    }
}
