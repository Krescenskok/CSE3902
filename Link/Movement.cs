using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5.Link
{
    public abstract class Movement : LinkState
    {

        private double lastTime;

        public Movement(LinkPlayer linkPlayer) :base(linkPlayer)
        {

        }

        public override Vector2 Update(GameTime gameTime, Vector2 location)
        {
            ProjectilesCommand.Instance.Update(gameTime);

            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime < 100)
                return location;

            lastTime = gameTime.TotalGameTime.TotalMilliseconds;

            if (link.IsStopped)
                return location;
            if (link.IsAttacking)
            {
                if (link.IsSecondAttack)
                {
                    if (link.SecondaryWeapon == ItemForLink.Boomerang)
                    {
                        if (LinkInventory.Instance.HasBoomerang)
                        {
                            if (!link.IsShootingProjectile)
                            {
                                link.IsShootingProjectile = true;
                                ProjectilesCommand.Instance.BoomerangThrow(link.LinkDirection);
                            }
                        }
                        return HandleArrowBow(gameTime, location);
                    }
                    else if (link.SecondaryWeapon == ItemForLink.BlueCandle)
                    {
                        if (!link.IsShootingProjectile)
                        {
                            link.IsShootingProjectile = true;
                            ProjectilesCommand.Instance.CandleBurn(link.LinkDirection);
                        }
                        return HandleArrowBow(gameTime, location);
                    }
                    else if (link.SecondaryWeapon == ItemForLink.Bomb)
                    {
                        if (!link.IsShootingProjectile)
                        {
                            link.IsShootingProjectile = true;
                            ProjectilesCommand.Instance.SpawnBomb(link.LinkDirection);
                        }
                        return HandleShield(gameTime, location);
                    }
                    else if (link.SecondaryWeapon == ItemForLink.ArrowBow)
                    {
                        if (LinkInventory.Instance.HasBow)
                        {
                            if (!link.IsShootingProjectile)
                            {
                                link.IsShootingProjectile = true;
                                ProjectilesCommand.Instance.ArrowBow(link.LinkDirection);
                            }
                            return HandleArrowBow(gameTime, location);
                        }
                        return HandleShield(gameTime, location);
                    }

                }
                else
                {
                    if (link.CurrentWeapon == ItemForLink.WoodenSword || link.CurrentWeapon == ItemForLink.Shield)
                    {

                        if (!link.IsShootingProjectile)
                        {
                            link.IsShootingProjectile = true;
                            delayExecute(250, (sender, e) => ProjectilesCommand.Instance.SwordBeam(link.LinkDirection));
                        }
                        return HandleWoodenSword(gameTime, location);
                    }
                    else if (link.CurrentWeapon == ItemForLink.Sword)
                    {
                        
                        if (!link.IsShootingProjectile)
                        {
                            link.IsShootingProjectile = true;
                            delayExecute(250, (sender, e) => ProjectilesCommand.Instance.SwordBeam(link.LinkDirection));
                        }
                        return HandleSword(gameTime, location);
                    }
                    else if (link.CurrentWeapon == ItemForLink.MagicalRod)
                    {
                        Sounds.Instance.Play("MagicalRod");
                        if (!link.IsShootingProjectile)
                        {
                            link.IsShootingProjectile = true;
                            delayExecute(300, (sender, e) => ProjectilesCommand.Instance.WandBeam(link.LinkDirection));
                        }
                        return HandleMagicalRod(gameTime, location);
                    }
                    else if (link.CurrentWeapon == ItemForLink.BlueRing)
                    {
                        link.UseRing = true;
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

            }
            if (link.IsPickingUpItem)
                return HandlePickUpItem(gameTime, location);

            return HandleShield(gameTime, location);
        }

        public abstract Vector2 HandleWoodenSword(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleSword(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleMagicalRod(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleShield(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandlePickUpItem(GameTime gameTime, Vector2 location);
        public abstract Vector2 HandleArrowBow(GameTime gameTime, Vector2 location);

    }
}
