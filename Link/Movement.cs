using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Final.Link
{
    public abstract class Movement: ILinkState
    {
        protected LinkPlayer link;

        protected ISprite linkSprite;
        Color[] colors = { Color.Yellow, Color.Pink, Color.Green, Color.Gold, Color.Blue, Color.IndianRed, Color.Indigo, Color.Ivory };

        protected int currentFrame;
       
        int i = 0;

        public Movement()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {
            if (linkSprite == null)
                linkSprite = SpriteFactory.Instance.CreateLinkSprite();


            if (link.IsDamaged)
            {

                if (link.DamageStartTime == 0)
                    link.DamageStartTime =  gameTime.TotalGameTime.TotalMilliseconds;
                else if ( gameTime.TotalGameTime.TotalMilliseconds - link.DamageStartTime < 1000)
                {
                    Color col = colors[i];
                    i++;
                    linkSprite.Draw(spriteBatch,location, currentFrame, col);

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
                linkSprite.Draw(spriteBatch,  location, currentFrame, Color.White);
            }
        }

        public virtual Vector2 Update(GameTime gameTime, Vector2 location)
        {
            if (link.IsStopped)
                return location;

            if (link.IsAttacking)
            {
                if (link.CurrentWeapon == Weapon.WoodenSword)
                {
                    return HandleWoodenSword(gameTime, location);
                }
                else if (link.CurrentWeapon == Weapon.Sword)
                {
                    return HandleSword(gameTime, location);
                }
                else if (link.CurrentWeapon == Weapon.MagicalRod)
                {
                    return HandleMagicalRod(gameTime, location);
                }

            }

            return HandleShield(gameTime, location);
        }

        public virtual void MovingDown()
        {
            link.state = new MoveDown(link);
        }

        public virtual void MovingRight()
        {
            link.state = new MoveRight(link);
        }

        public virtual void MovingUp()
        {
            link.state = new MoveUp(link);
        }

        public virtual void Stationary()
        {
            link.state = new Stationary(link);
        }

        public virtual void MovingLeft()
        {
            link.state = new MoveLeft(link);
        }

        public abstract Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleSword(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleShield(GameTime gameTime, Vector2 location);

     

        public void Draw(Game game, SpriteBatch spriteBatch, GameTime gameTime )
        {
        }
    }
}
