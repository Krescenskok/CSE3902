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
 
        private bool boomerangMade = false;
        private bool candleMade = false;
        private bool bombMade = false;


  
        private Vector2 itemLocation;
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
            if (!arrowMade)
            {
                arrowMade = true;
                itemLocation = link.CurrentLocation;
                itemLocation.Y += 10;
                item = new Arrow(ItemsFactory.Instance.CreateArrowSprite(direction), itemLocation, direction);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void SwordBeam(string direction)
        {
            //itemLocation = link.CurrentLocation;
            //bool canAdd = !link.itemsPlacedByLink.Exists(v => v is SwordBeam);

            //if (canAdd)
            //{
            //    item = ProjectilesFactory.CreateSwordBeam(direction, link);
            //    link.itemsPlacedByLink.Add(item);
            //}
            //ExpireCheck();

            if (link.Health == link.FullHealth && !beamMade)
            {
                beamMade = true;
                if (direction.Equals("Down") || link.state is Stationary)
                {
                    itemLocation.X += 14;
                    item = new SwordBeam(ItemsFactory.Instance.CreateDownBeamSprite(), itemLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals("Right"))
                {
                    itemLocation.Y += 5;
                    item = new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), itemLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals("Left"))
                {
                    itemLocation.Y += 7;
                    item = new SwordBeam(ItemsFactory.Instance.CreateLeftBeamSprite(), itemLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else
                {
                    itemLocation.X += 8;
                    item = new SwordBeam(ItemsFactory.Instance.CreateUpBeamSprite(), itemLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
            }
            ExpireCheck();
        }

        public void WandBeam(string direction)
        {
            itemLocation = link.CurrentLocation;

            bool canAdd =  !link.itemsPlacedByLink.Exists(v => v is WandBeam);

            if (canAdd)
            { 
                item = ProjectilesFactory.CreateWandBeam(direction, link);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void BoomerangThrow(string direction)
        {
            if (!boomerangMade)
            {
                itemLocation = link.CurrentLocation;
                itemLocation.Y += 10;
                boomerangMade = true;
                item = new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), itemLocation, direction, link);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void CandleBurn(string direction)
        {
            Vector2 loc = link.CurrentLocation;
            if (!candleMade)
            {
                candleMade = true;
                loc.X += 10;
                if (direction.Equals("Up"))
                    loc.Y -= buffer;
                else if (direction.Equals("Down"))
                    loc.Y += buffer;
                else if (direction.Equals("Right"))
                    loc.X += buffer;
                else
                    loc.X -= buffer;

                item = new CandleFire(ItemsFactory.Instance.CreateCandleFireSprite(), loc);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void SpawnBomb(string direction)
        {
            Vector2 loc = link.CurrentLocation;
            if (!bombMade)
            {
                LinkInventory.Instance.BombCount--;
                bombMade = true;
                loc.X += 10;
                if (direction.Equals("Up"))
                    loc.Y -= buffer;
                if (direction.Equals("Down") || link.state is Stationary)
                    loc.Y += buffer;
                else if (direction.Equals("Right"))
                    loc.X += buffer;
                else
                    loc.X -= buffer;

                item = new Bomb(ItemsFactory.Instance.CreateBombSprite(), loc);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void ExpireCheck()
        {
            List<IItems> list = new List<IItems>();
            foreach (IItems item in link.itemsPlacedByLink)
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
                link.RemovePlacedItem(item);
            }

        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            placedItems = link.itemsPlacedByLink;
            foreach (IItems projectile in placedItems)
            {
                projectile.Draw(spriteBatch);
            }

        }
        public void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - LastTime > 100)
            {
                placedItems = link.itemsPlacedByLink;
                foreach (IItems projectile in placedItems)
                {
                    projectile.Update();
                }               
            }

            ExpireCheck();
        }
    }
}
