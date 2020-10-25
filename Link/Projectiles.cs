using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;

namespace Sprint3.Link
{
    public class ProjectilesCommand
    {
        private double lastTime;
        LinkPlayer link;
        IItems item;
        private List<IItems> placedItems = new List<IItems>();
        private bool beamMade = false;
        private bool arrowMade = false;
        private bool wandMade = false;
        private bool boomerangMade = false;
        private bool candleFire = false;
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
            itemLocation = link.CurrentLocation;
        
            if (link.Health == link.FullHealth && !beamMade)
            {
                beamMade = true;
                if (direction.Equals("Down"))
                {
                    itemLocation.X += 12;

                    item = new SwordBeam(ItemsFactory.Instance.CreateDownBeamSprite(), itemLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals("Right"))
                {
                    itemLocation.Y += 8;
              
                    item = new SwordBeam(ItemsFactory.Instance.CreateRightBeamSprite(), itemLocation, direction);
                    link.itemsPlacedByLink.Add(item);
                }
                else if (direction.Equals("Left"))
                {
                    itemLocation.Y += 8;
                  
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
        
            if (!wandMade)
            {
                if(direction.Equals("Down")) {
                    itemLocation.Y += 10;
                    itemLocation.X += 10;
                }
                else if (direction.Equals("Left"))
                {
                    itemLocation.Y += 10;
                    itemLocation.X -= 10;
                }
                else if (direction.Equals("Right"))
                {
                    itemLocation.Y += 10;
                    itemLocation.X += 10;
                }
                else
                {
                    itemLocation.Y -= 10;
                    itemLocation.X += 6;
                }
                wandMade = true;
                item = new WandBeam(ItemsFactory.Instance.CreateWandBeamSprite(direction), itemLocation, direction);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void BoomerangThrow(string direction)
        {
            itemLocation = link.CurrentLocation;
            itemLocation.Y -= 10;
            if (!boomerangMade)
            {
                boomerangMade = true;
                item = new Boomerang(ItemsFactory.Instance.CreateBoomerangSprite(), itemLocation, direction, link);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();
        }

        public void BlueCandle(string direction)
        {
            itemLocation = link.CurrentLocation;
            itemLocation.Y -= 10;
            if(!candleFire)
            {
                candleFire = true;
                item = new CandleFire(ItemsFactory.Instance.CreateCandleFireSprite(), itemLocation);
                link.itemsPlacedByLink.Add(item);
            }
            ExpireCheck();


        }

        public void Bomb(string direction)
        {

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
                    candleFire = false;
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
