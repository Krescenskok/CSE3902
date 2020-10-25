using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;

namespace Sprint3.Link
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
        public ProjectilesCommand()
        {

        }

        public void ArrowBow(string direction)
        {
            if (!arrowMade)
            {
                arrowMade = true;
                item = new Arrow(ItemsFactory.Instance.CreateArrowSprite(direction), link.CurrentLocation, direction);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void SwordBeam(string direction)
        {
            if (link.Health == link.FullHealth && !beamMade)
            {
                beamMade = true;
                if (direction.Equals("Down"))
                {
                    item = new SwordBeam(ItemsFactory.Instance.CreateDownBeamSprite(), link.CurrentLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals("Right"))
                {
                    item = new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), link.CurrentLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals("Left"))
                {
                    item = new SwordBeam(ItemsFactory.Instance.CreateLeftBeamSprite(), link.CurrentLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else
                {
                    item = new SwordBeam(ItemsFactory.Instance.CreateUpBeamSprite(), link.CurrentLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
            }
            ExpireCheck();
        }

        public void WandBeam(string direction)
        {
            if (!wandMade)
            {
                wandMade = true;
                item = new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), link.CurrentLocation, direction);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void BoomerangThrow(string direction)
        {
            if (!boomerangMade)
            {
                boomerangMade = true;
                item = new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), link.CurrentLocation, direction, link);
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
                {
                    loc.Y -= buffer;
                }
                else if (direction.Equals("Down"))
                {
                    loc.Y += buffer;
                }
                else if (direction.Equals("Right"))
                {
                    loc.X += buffer;
                }
                else
                {
                    loc.X -= buffer;
                }

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
                bombMade = true;
                loc.X += 10;
                if (direction.Equals("Up"))
                {
                    loc.Y -= buffer;
                }
                else if (direction.Equals("Down"))
                {
                    loc.Y += buffer;
                }
                else if (direction.Equals("Right"))
                {
                    loc.X += buffer;
                }
                else
                {
                    loc.X -= buffer;
                }

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
                if (item is SwordBeam && (((SwordBeam)item).expired == true))
                {
                        beamMade = false;
                        list.Add(item);
                }
                else if (item is Arrow && ((Arrow)item).expired == true)
                {
                    arrowMade = false;
                    list.Add(item);
                }
                else if (item is WandBeam && ((WandBeam)item).expired == true)
                {
                    wandMade = false;
                    list.Add(item);
                }
                else if (item is Boomerang && ((Boomerang)item).returned == true)
                {
                    boomerangMade = false;
                    list.Add(item);
                }
                else if (item is CandleFire && ((CandleFire)item).expired == true)
                {
                    candleMade = false;
                    list.Add(item);
                }
                else if (item is Bomb && ((Bomb)item).expired == true)
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
            if (gameTime.TotalGameTime.TotalMilliseconds - lastTime > 100)
            {
                placedItems = link.itemsPlacedByLink;
                foreach (IItems projectile in placedItems)
                {
                    projectile.Update();
                }
               
            }
        }
    }
}
