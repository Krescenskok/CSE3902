﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;



namespace Sprint5.Link
{
    public abstract class LinkState : ILinkState
    {
        protected LinkPlayer link;
        protected int currentFrame;
        private const int THOUSAND = 1000;



        private List<IItems> itemsPlacedByLink = new List<IItems>();

        protected LinkSprite linkSprite;
        readonly Color[] colors = { Color.Red, Color.Pink, Color.DarkRed, Color.DarkRed, Color.MediumVioletRed, Color.IndianRed, Color.OrangeRed, Color.PaleVioletRed };
        readonly Color[] clockColors = { Color.Blue, Color.White, Color.BlueViolet, Color.LightBlue, Color.Aquamarine, Color.Aqua };
        readonly Color[] invincibleColors = { Color.Blue, Color.Green, Color.Pink, Color.Yellow, Color.Ivory, Color.Purple, Color.Lavender, Color.LightBlue };
        
        int colorIndex = 0;

        public LinkState(LinkPlayer link)
        {
            this.link = link;
            link.isWalkingInPlace = false;
            itemsPlacedByLink = link.itemsPlacedByLink;
            if (link.Delay <= 0)
            {
                link.isWalkingInPlace = false;

            }
        }


        public Rectangle Bounds()
        {
            return link.hitbox;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 location)
        {
            Color col;
            if (link.sprite == null)
            {
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreateLinkSprite();
            }
            if (link.UseRing)
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreateBlueRingLinkSprite();
            linkSprite = link.sprite;

            if (link.LargeShield && link.DrawShield)
            {
                if (link.state is MoveDown)
                    currentFrame = 10;
                else if (link.state is MoveLeft)
                    currentFrame = 14;
                else if (link.state is MoveRight)
                    currentFrame = 12;
                link.DrawShield = false;
            }
            if (link.IsDamaged || link.Clock || link.IsInvincible)
            {
                if (link.DamageStartTime == 0)
                    link.DamageStartTime = gameTime.TotalGameTime.TotalMilliseconds;
                else if (gameTime.TotalGameTime.TotalMilliseconds - link.DamageStartTime < 2000)
                {
                    if (link.IsDamaged)
                    {
                        col = colors[colorIndex];
                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        colorIndex++;
                        if (colorIndex == colors.Length)
                            colorIndex = 0;
                        //link.currentLocation = location;
                    }
                    else if (link.Clock)
                    {
                        col = clockColors[colorIndex];
                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        colorIndex++;
                        if (colorIndex == clockColors.Length)
                            colorIndex = 0;
                        //link.currentLocation = location;
                    }
                    else if (link.IsInvincible)
                    {
                        col = invincibleColors[colorIndex];
                        linkSprite.Draw(spriteBatch, location, currentFrame, col);
                        colorIndex++;
                        if (colorIndex == invincibleColors.Length)
                            colorIndex = 0;
                        //link.currentLocation = location;
                    }
                    link.currentLocation = location;
                }

                else
                {
                    link.IsDamaged = false;
                    link.Clock = false;
                    link.IsInvincible = false;
                }

            }
            else
                linkSprite.Draw(spriteBatch, location, currentFrame, Color.White);

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

        public void SwitchFrames(int[] currentFrames)
        {
            for (int j = 0; j < currentFrames.Length; j++)
            {
                if (currentFrame == currentFrames[j])
                {
                    if (j == currentFrames.Length - 1)
                    {
                        currentFrame = currentFrames[0];
                        link.IsAttacking = false;
                        link.IsStopped = true;
                    }

                    else
                        currentFrame = currentFrames[j + 1];

                    break;
                }
            }
        }

        public virtual Vector2 Update(GameTime gameTime, Vector2 location)
        {
            throw new NotImplementedException();
        }
    }
}
