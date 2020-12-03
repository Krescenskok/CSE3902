using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;

namespace Sprint5.Items
{
    //class for the arrow that link shoots, AKA the moving arrow
    public class Arrow : IItems
    {
        private Vector2 location;
        private ProjectileCollider collider;
        public bool expired = false;
        private ISprite item;
        private int drawnFrame = 0;
        private IItemsState state;
        private bool isExpired = false;
        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        public Arrow(ISprite item, Vector2 location, string direction)
        {
            this.location = location;
            this.item = item;
            state = new ArrowFlyingState(this, location, direction);

            if (direction == "Down")
            {
                collider = new ProjectileCollider((item as ArrowDownSprite).Hitbox, this, this.state, "Arrow");
            }
            else if (direction == "Up")
            {
                collider = new ProjectileCollider((item as ArrowUpSprite).Hitbox, this, this.state, "Arrow");
            }
            else if (direction == "Left")
            {
                collider = new ProjectileCollider((item as ArrowLeftSprite).Hitbox, this, this.state, "Arrow");
            }
            else if (direction == "Right")
            {
                collider = new ProjectileCollider((item as ArrowRightSprite).Hitbox, this, this.state, "Arrow");
            }
            Sounds.Instance.PlaySoundEffect("ArrowBoomerang");
        }

        public void UpdateLocation(Vector2 location)
        {
            this.location = location;
        }

        public void UpdateSprite(ISprite sprite)
        {
            this.item = sprite;
        }

        public void Impact()
        {
            state = new ArrowImpactState(this);
        }

        public void Update()
        {
            state.Update();
            collider.Update(this, this.state);
        }

        public void Collect()
        {
            state.Collected();
        }

        public void Expire()
        {
            state.Expire();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
        }
        
    }
}
