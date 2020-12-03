using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;

namespace Sprint5.Link
{
    public class ProjectilesCommand
    {
        private double lastTime;
        LinkPlayer link;
        private const int buffer = 50;
        IItems item;
        private List<IItems> placedItems = new List<IItems>();
        private bool beamMade = false;
        private bool arrowMade = false;
        private bool wandMade = false;
        private bool boomerangMade = false;
        private bool candleMade = false;
        private bool bombMade = false;

        private readonly string UP = "Up";
        private readonly string DOWN = "Down";
        private readonly string LEFT = "Left";
        private readonly string RIGHT = "Right";

        private const int DISPLACEMENT = 10;
        private const int DISP_SWORD_LR = 8;

        private Vector2 itemLocation;
        private List<IItems> itemsPlaced = new List<IItems>();
        public List<IItems> itemsPlacedByLink
        {
            get { return itemsPlaced; }
            set { itemsPlaced = value; }
        }

        private static ProjectilesCommand instance = new ProjectilesCommand();

        public LinkPlayer Link
        {
            get { return link; }
            set { link = value; }
        }

        public static ProjectilesCommand Instance
        {
            get
            {
                return instance;
            }
        }

        public double LastTime { get => lastTime; set => lastTime = value; }

        public ProjectilesCommand()
        {

        }

        public void ArrowBow(string direction)
        {
            if (!arrowMade && LinkInventory.Instance.HasBow)
            {
                arrowMade = true;
                itemLocation = link.CurrentLocation;
                itemLocation.Y += 10;
                item = new Arrow(ItemsFactory.Instance.CreateArrowSprite(direction), itemLocation, direction);
                itemsPlacedByLink.Add(item);
            }
            else
            {
                Link.IsShootingProjectile = false;
            }
        }

        public void SwordBeam(string direction)
        {
            itemLocation = link.CurrentLocation;

            if (link.Health == link.FullHealth && !beamMade)
            {
                beamMade = true;
                if (direction.Equals(DOWN))
                {
                    itemLocation.X += (DISPLACEMENT + DISPLACEMENT / 2);

                    item = new SwordBeam(ItemsFactory.Instance.CreateDownBeamSprite(), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals(RIGHT))
                {
                    itemLocation.Y += DISP_SWORD_LR;

                    item = new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals(LEFT))
                {
                    itemLocation.Y += DISP_SWORD_LR;

                    item = new SwordBeam(ItemsFactory.Instance.CreateLeftBeamSprite(), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals(UP))
                {
                    itemLocation.X += DISPLACEMENT;

                    item = new SwordBeam(ItemsFactory.Instance.CreateUpBeamSprite(), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }

                Sounds.Instance.PlaySoundEffect("SwordShoot");
            }
            else
            {
                Link.IsShootingProjectile = false;
            }
        }

        public void WandBeam(string direction)
        {
            itemLocation = link.CurrentLocation;
            if (!wandMade)
            {
                wandMade = true;
                if (direction.Equals(DOWN) || link.state is Stationary)
                {
                    itemLocation.Y += DISPLACEMENT;
                    itemLocation.X += DISPLACEMENT;
                    item = new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals(LEFT))
                {
                    itemLocation.Y += DISPLACEMENT;
                    itemLocation.X -= buffer;
                    item = new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals(RIGHT))
                {
                    itemLocation.Y += DISPLACEMENT;
                    itemLocation.X += buffer;
                    item = new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals(UP))
                {
                    itemLocation.Y -= DISPLACEMENT;
                    itemLocation.X += DISPLACEMENT / 2;
                    item = new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);
                    itemsPlacedByLink.Add(item);
                }
            }
            else
            {
                Link.IsShootingProjectile = false;
            }
        }

        public void BoomerangThrow(string direction)
        {
            if (!boomerangMade && LinkInventory.Instance.HasBoomerang)
            {
                boomerangMade = true;
                itemLocation = link.CurrentLocation;
                itemLocation.Y += DISPLACEMENT;
                item = new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), itemLocation, direction, link);
                itemsPlacedByLink.Add(item);
            }
            else
            {
                Link.IsShootingProjectile = false;
            }
        }

        public void CandleBurn(string direction)
        {
            Vector2 loc = link.CurrentLocation;
            if (!candleMade && LinkInventory.Instance.HasCandle)
            {
                candleMade = true;
                loc.X += 10;
                if (direction.Equals(UP))
                {
                    loc.Y -= buffer;
                }
                else if (direction.Equals(DOWN))
                {
                    loc.Y += buffer;
                }
                else if (direction.Equals(RIGHT))
                {
                    loc.X += buffer;
                }
                else
                {
                    loc.X -= buffer;
                }

                item = new CandleFire(ItemsFactory.Instance.CreateCandleFireSprite(), loc);
                itemsPlacedByLink.Add(item);
            }
            else
            {
                Link.IsShootingProjectile = false;
            }
        }

        public void SpawnBomb(string direction)
        {
            Vector2 loc = link.CurrentLocation;
            if (!bombMade && LinkInventory.Instance.BombCount > 0)
            {
                LinkInventory.Instance.BombCount--;
                bombMade = true;
                if (direction.Equals(LEFT))
                {
                    loc.X -= buffer;
                }
                else if (direction.Equals(DOWN) || link.state is Stationary)
                {
                    loc.Y += buffer;
                }
                else if (direction.Equals(RIGHT))
                {
                    loc.X += buffer;
                }
                else
                {
                    loc.Y -= buffer;
                }
                item = new Bomb(ItemsFactory.Instance.CreateBombSprite(), loc);
                itemsPlacedByLink.Add(item);
            }
            else
            {
                Link.IsShootingProjectile = false;
            }
        }

        public void ExpireCheck()
        {
            List<IItems> list = new List<IItems>();
            foreach (IItems item in itemsPlacedByLink)
            {
                if (item is SwordBeam && item.IsExpired)
                {
                    beamMade = false;
                    list.Add(item);
                }
                else if (item is Arrow && item.IsExpired)
                {
                    arrowMade = false;
                    list.Add(item);
                }
                else if (item is WandBeam && item.IsExpired)
                {
                    wandMade = false;
                    list.Add(item);
                }
                else if (item is Boomerang && item.IsExpired)
                {
                    boomerangMade = false;
                    list.Add(item);
                }
                else if (item is CandleFire && item.IsExpired)
                {
                    candleMade = false;
                    list.Add(item);
                }
                else if (item is Bomb && item.IsExpired)
                {
                    bombMade = false;
                    list.Add(item);
                }
            }

            foreach (IItems item in list)
            {
                RemovePlacedItem(item);
            }

        }

        private void RemovePlacedItem(IItems item)
        {
            if (itemsPlacedByLink.Contains(item) && item.IsExpired)
            {
                itemsPlacedByLink.Remove(item);
            }
            Link.IsShootingProjectile = false;
        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            placedItems = itemsPlacedByLink;
            foreach (IItems projectile in placedItems)
            {
                projectile.Draw(spriteBatch);
            }

        }

        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - LastTime > 100)
            {
                placedItems = itemsPlacedByLink;
                foreach (IItems projectile in placedItems)
                {
                    projectile.Update();
                }
            }

            ExpireCheck();
        }
    }
}