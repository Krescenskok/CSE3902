using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint4.Link
{
    public abstract class Movement : ILinkState
    {
        protected LinkPlayer link;


        protected LinkSprite linkSprite;
        Color[] colors = { Color.Yellow, Color.Pink, Color.Green, Color.Gold, Color.Blue, Color.IndianRed, Color.Indigo, Color.Ivory };
        Color[] clockColors = { Color.Blue, Color.White, Color.BlueViolet, Color.LightBlue, Color.Aquamarine, Color.Aqua };
        protected int currentFrame;

        int i = 0;
   

        private List<IItems> itemsPlacedByLink = new List<IItems>();

        public Movement(LinkPlayer link)
        {
            this.link = link;
            link.isWalkingInPlace = false;
            itemsPlacedByLink = link.itemsPlacedByLink;
            if (link.Delay <= 0)
            {
                link.isWalkingInPlace = false;

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {
            Color col;
            if (link.sprite == null)
            {
                if (link.UseRing)
                {
                    link.sprite = (LinkSprite)SpriteFactory.Instance.CreateBlueLinkSprite();
                   

                }
                else
                {
                    link.sprite = (LinkSprite)SpriteFactory.Instance.CreateLinkSprite();


              
                }
                linkSprite = link.sprite;
            }
                

            if (link.LargeShield && link.DrawShield)
            {

                if (link.state is MoveDown)
                {
                    currentFrame = 10;
                }
                else if (link.state is MoveLeft)
                {
                    currentFrame = 14;
                }
                else if (link.state is MoveRight)
                {
                    currentFrame = 12;
                }
                link.DrawShield = false;
            }


            if (link.IsDamaged || link.Clock)
            {

                if (link.DamageStartTime == 0)
                    link.DamageStartTime = gameTime.TotalGameTime.TotalMilliseconds;
                else if (gameTime.TotalGameTime.TotalMilliseconds - link.DamageStartTime < 1000)
                {
                    if (link.IsDamaged)
                    {
       
                        col = colors[i];

                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        i++;
                        if (i == colors.Length - 1)
                        {
                            i = 0;
                        }
                        link.currentLocation = location;

                    }
                    else if (link.Clock)
                    {
                        col = clockColors[i];
                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        i++;
                        if (i == clockColors.Length - 1)
                        {
                            i = 0;
                        }

                    }

                }
                else
                {
                    link.IsDamaged = false;
                    link.Clock = false;

                }

            }
            else
            {
                 
                  linkSprite.Draw(spriteBatch, location, currentFrame, Color.White);

            }

            foreach (IItems projectile in link.itemsPlacedByLink)
            {
                projectile.Draw(spriteBatch);
            }

        }


        public void delayExecute(double interval, ElapsedEventHandler handler)
        {
            Timer t = new Timer();
            t.Elapsed += handler;
            t.Interval = interval;
            t.AutoReset = false;
            t.Start();
        }


        public virtual Vector2 Update(GameTime gameTime, Vector2 location)
        {
            ProjectilesCommand.Instance.Update(gameTime);

            if (link.IsStopped)
            {

                return location;
            }
            if (link.IsAttacking)
            {

                if (link.CurrentWeapon == ItemForLink.WoodenSword || link.CurrentWeapon == ItemForLink.Shield)
                {
                    delayExecute(250, (sender, e) => ProjectilesCommand.Instance.SwordBeam(link.LinkDirection));
                    return HandleWoodenSword(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.Sword)
                {
                    delayExecute(250, (sender, e) => ProjectilesCommand.Instance.SwordBeam(link.LinkDirection));
                    return HandleSword(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.MagicalRod)
                {
                    delayExecute(300, (sender, e) => ProjectilesCommand.Instance.WandBeam(link.LinkDirection));
                    return HandleMagicalRod(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.ArrowBow)
                {
                    ProjectilesCommand.Instance.ArrowBow(link.LinkDirection);
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.BlueRing)
                {
                    link.UseRing = true;
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.Boomerang)
                {
                    ProjectilesCommand.Instance.BoomerangThrow(link.LinkDirection);
                    //animation to throw is same as bow
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.BlueCandle)
                {
                    ProjectilesCommand.Instance.CandleBurn(link.LinkDirection);
                    //animation to throw is same as bow
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.Bomb)
                {
                    ProjectilesCommand.Instance.SpawnBomb(link.LinkDirection);
                    //animation to throw is same as bow
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.CurrentWeapon == ItemForLink.Clock)
                {
                    link.Clock = true;
                    RoomEnemies.Instance.StunAllEnemies();
                    return HandleArrowBow(gameTime, location);
                }
                else if (link.LargeShield)
                {
                    return HandleShield(gameTime, location);
                }

            }
            if (link.IsPickingUpItem)
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

        public abstract Rectangle Bounds();

    }
}
