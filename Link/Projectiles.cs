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
        IItems item;
        private List<IItems> placedItems = new List<IItems>();
        private List<IItems> itemsPlaced = new List<IItems>();

        private Vector2 itemLocation;

        private static ProjectilesCommand instance = new ProjectilesCommand();

        public List<IItems> itemsPlacedByLink { get => itemsPlaced; set => itemsPlaced = value; }
        public LinkPlayer Link { get => link; set => link = value; }
        public double LastTime { get => lastTime; set => lastTime = value; }
        public static ProjectilesCommand Instance { get => instance; }
        public List<IItems> ItemsPlaced { get => itemsPlaced; set => itemsPlaced = value; }

        public ProjectilesCommand()
        {
        }

        public void ArrowBow(string direction)
        {
            itemLocation = link.CurrentLocation;
            var it = itemsPlacedByLink.Find(item => (item is Arrow));
            if (it == null && LinkInventory.Instance.HasBow)
                itemsPlacedByLink.Add(ProjectilesFactory.CreateArrowBow(direction, link));
            else
                Link.IsShootingProjectile = false;
        }

        public void SwordBeam(string direction)
        {
            itemLocation = link.CurrentLocation;
            var it = itemsPlacedByLink.Find(item => (item is SwordBeam));
            if (it == null && link.Health == link.FullHealth)
                itemsPlacedByLink.Add(ProjectilesFactory.CreateSwordBeam(direction, link));
            else
                Link.IsShootingProjectile = false;
        }

        public void WandBeam(string direction)
        {
            itemLocation = link.CurrentLocation;
            var it = itemsPlacedByLink.Find(item => (item is WandBeam));
            if(it == null)
                itemsPlacedByLink.Add(ProjectilesFactory.CreateWandBeam(direction, link));
            else
                Link.IsShootingProjectile = false;
        }

        public void BoomerangThrow(string direction)
        {
            itemLocation = link.CurrentLocation;
            var it = itemsPlacedByLink.Find(item => (item is Boomerang));
            if (it == null && LinkInventory.Instance.HasBoomerang)
                itemsPlacedByLink.Add(ProjectilesFactory.CreateBoomerangThrow(direction, link));
            else
                Link.IsShootingProjectile = false;
        }

        public void CandleBurn(string direction)
        {
            Vector2 loc = link.CurrentLocation;
            var it = itemsPlacedByLink.Find(item => (item is CandleFire));
            if (it == null && LinkInventory.Instance.HasCandle)
                itemsPlacedByLink.Add(ProjectilesFactory.CreateCandleFire(direction, link));
            else
                Link.IsShootingProjectile = false;
        }

        public void SpawnBomb(string direction)
        {
            Vector2 loc = link.CurrentLocation;
            var it = itemsPlacedByLink.Find(item => (item is Bomb));
            if (it==null && LinkInventory.Instance.BombCount > 0)
            {
                itemsPlacedByLink.Add(ProjectilesFactory.CreateSpawnBomb(direction, link));
            }
            else
                Link.IsShootingProjectile = false;
        }

        public void ExpireCheck()
        {
            List<IItems> list = new List<IItems>();
            foreach (IItems item in itemsPlacedByLink)
            {
                if (item is SwordBeam && item.IsExpired)
                    list.Add(item);
                else if (item is Arrow && item.IsExpired)
                    list.Add(item);
                else if (item is WandBeam && item.IsExpired)
                    list.Add(item);
                else if (item is Boomerang && item.IsExpired)
                    list.Add(item);
                else if (item is CandleFire && item.IsExpired)
                    list.Add(item);
                else if (item is Bomb && item.IsExpired)
                    list.Add(item);
            }
            foreach (IItems item in list)
            {
                RemovePlacedItem(item);
            }
        }

        private void RemovePlacedItem(IItems item)
        {
            if (itemsPlacedByLink.Contains(item) && item.IsExpired)
                itemsPlacedByLink.Remove(item);
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