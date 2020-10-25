using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Link;

namespace Sprint3.Items
{
    public class Boomerang : IItems
    {
        private Vector2 location;
        public bool returned = false;
        private ISprite item;
        private int drawnFrame;
        private IItemsState state;
        private bool throwing;
        private bool returning;
        private LinkPlayer link;
        private string direction;
        private List<IItems> impactList = new List<IItems>();

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
            impactList.Add(new BoomerangImpact(ItemsFactory.Instance.CreateProjectileImpactSprite(), this.location));
        }

        public void ReturnedToLink()
        {
            if (throwing && returning) 
            {
                returned = true;
                state.Expire();
            }
        }

        public void Update()
        {
            if (throwing)
            {
                state.Update();
            }

            if (impactList.Count > 0)
            {
                foreach (IItems hit in impactList)
                {
                    hit.Update();

                    if (((BoomerangImpact) hit).isExpired)
                    {
                        impactList.Remove(hit);
                    }
                }
            }
        }

        public void Expire()
        {

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
