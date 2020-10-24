using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3.Link
{
    public abstract class Movement : ILinkState
    {
        protected LinkPlayer link;

        protected ISprite linkSprite;
        Color[] colors = { Color.Yellow, Color.Pink, Color.Green, Color.Gold, Color.Blue, Color.IndianRed, Color.Indigo, Color.Ivory };

        protected int currentFrame;

        int i = 0;

        public Movement(LinkPlayer link)
        {
            this.link = link;
            link.isWalkingInPlace = false;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {
            if (linkSprite == null)
                linkSprite = SpriteFactory.Instance.CreateLinkSprite();


            if (link.IsDamaged)
            {

                if (link.DamageStartTime == 0)
                    link.DamageStartTime = gameTime.TotalGameTime.TotalMilliseconds;
                else if (gameTime.TotalGameTime.TotalMilliseconds - link.DamageStartTime < 1000)
                {
                    Color col = colors[i];
                    i++;
                    linkSprite.Draw(spriteBatch, location, currentFrame, col);

                    if (i == colors.Length - 1)
                    {
                        i = 0;
                    }
                }
                else
                {
                    link.IsDamaged = false;
                }

            }
            else
            {
                if (link.UseRing)
                {
                    System.Diagnostics.Debug.WriteLine("Should be true: " + link.UseRing);
                    linkSprite.Draw(spriteBatch, location, currentFrame, Color.MediumAquamarine);
                }
                else
                {
                    linkSprite.Draw(spriteBatch, location, currentFrame, Color.White);
                }
            }
        }

        public virtual Vector2 Update(GameTime gameTime, Vector2 location)
        {
          

            if (link.IsStopped)
            {
                if (location.X <= 0)
                    location.X = 0;
                else if (location.X >= 780)
                        location.X = 780;
                else if (location.Y <= 0)
                    location.Y = 0;
                else if (location.Y >= 445)
                    location.Y = 445;

                return location;
            }

            if (link.IsAttacking)
            {
                System.Diagnostics.Debug.WriteLine(link.CurrentWeapon);

                if (link.CurrentWeapon == ItemForLink.WoodenSword)
                {
                    return HandleWoodenSword(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.Sword)
                {
                    return HandleSword(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.MagicalRod)
                {
                    return HandleMagicalRod(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.ArrowBow)
                {
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.BlueRing)
                {
                    link.UseRing = true;
                }

            }
            if(link.IsPickingUpItem)
            {
                return HandlePickUpItem(gameTime, location);
            }

            return HandleShield(gameTime, location);
        }

        

        public abstract Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleSword(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleShield(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleArrowBow(GameTime gameTime, Vector2 location);



        public void Draw(Game game, SpriteBatch spriteBatch, GameTime gameTime)
        {
        }

        
    }
}
