using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;

namespace Sprint5.Items
{
    public class Boomerang : IItems
    {
        private Vector2 location;
        private BoomerangCollider collider;
        public bool returned = false;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private bool throwing;
        private bool returning;
        private string direction;
        private LinkPlayer link;
        private List<IItems> impactList = new List<IItems>();
        private bool isExpired = false;


        public bool IsExpired
        {
            get { return isExpired; }
            set { isExpired = value; }
        }

        public Vector2 Location { get => location; }

        public ICollider Collider { get => collider; }

        public IItemsState State { get => state; }

        public Boomerang(ISprite item, Vector2 location, string direction, LinkPlayer link)
        {
            this.location = location;
            this.item = item;
            this.link = link;
            this.direction = direction;
            drawnFrame = 0;
            state = new ThrownBoomerangState(this, location, direction, link);
            throwing = true;
            returning = false;

            collider = new BoomerangCollider((item as BoomerangSprite).Hitbox, this, this.state);

            Sounds.Instance.AddLoopedSound("ArrowBoomerang", GetHashCode().ToString());
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

        public void ThrowBoomerang(bool throwing, bool returning)
        {
            this.throwing = throwing;
            this.returning = returning;
        }

        public void Returning()
        {
            state = new ReturningBoomerangState(this, location, link);
        }

        public void Impact()
        {
            impactList.Add(new BoomerangImpact(ItemsFactory.Instance.CreateProjectileImpactSprite(), this.location, this.direction, returning, link));
        }

        public void ReturnedToLink()
        {
            if (throwing && returning)
            {
                returned = true;
                state.Expire();
                Sounds.Instance.StopLoopedSound(GetHashCode().ToString());
            }
        }

        public void Update()
        {
            if (throwing)
            {
                state.Update();
            }

            foreach (IItems hit in impactList)
            {
                hit.Update();
            }

            int i;
            for (i = 0; i < impactList.Count; i++)
            {
                if (impactList[i].IsExpired)
                {
                    impactList.RemoveAt(i);
                }
            }

            collider.Update(this, this.state);
        }

        public void Expire()
        {
            if (returned)
                state.Expire();
        }

        public void Collect()
        {
            state.Collected();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            item.Draw(spriteBatch, location, drawnFrame, Color.White);
            foreach (IItems hit in impactList)
            {
                hit.Draw(spriteBatch);
            }
        }
    }
}
