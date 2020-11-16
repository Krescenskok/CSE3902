using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Items;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Sprint4
{
    public class HUD
    {
        private Texture2D HUDTexture;
        private ISprite sprite;
        private SpriteFont HUDfont;
        private LinkInventory.PrimaryItem aSlot;
        private LinkInventory.SecondaryItem bSlot;
        private Vector2 bSlotLocation;
        private Vector2 aSlotLocation;
        private Vector2 rupeeCountLocation;
        private Vector2 keyCountLocation;
        private Vector2 bombCountLocation;        
        private Vector2 bSlotBottomLocation;
        private Vector2 aSlotBottomLocation;
        private Vector2 rupeeCountBottomLocation;
        private Vector2 keyCountBottomLocation;
        private Vector2 bombCountBottomLocation;
        private Vector2 bottomCorner;
        private Dictionary<LinkInventory.SecondaryItem, IItems> bSlotItems = new Dictionary<LinkInventory.SecondaryItem, IItems>();
        private Dictionary<LinkInventory.SecondaryItem, IItems> bSlotItemsBottom = new Dictionary<LinkInventory.SecondaryItem, IItems>();
        private Dictionary<LinkInventory.PrimaryItem, IItems> aSlotItems = new Dictionary<LinkInventory.PrimaryItem, IItems>();
        private Dictionary<LinkInventory.PrimaryItem, IItems> aSlotItemsBottom = new Dictionary<LinkInventory.PrimaryItem, IItems>();
        private const string direction = "Up";
        private Dictionary<String, IItems> hearts = new Dictionary<String, IItems>();
        private List<IItems> drawnHearts = new List<IItems>();
        private List<IItems> drawnHeartsBottom = new List<IItems>();
        private Vector2 firstHeartLoc;
        private Vector2 firstBottomHeartLoc;
        private int prevHealth = -1;
        private int maxHearts = 3;
        int emptyCount = 0;
        int fullCount = 0;
        int halfCount = 0;
        int rupeeCount = 0;
        int keyCount = 0; 
        int bombCount = 0;

        private const int BBUFFER = 9;
        private const int ABUFFER = 83;
        private const int THIRD = 3;
        private const int FIVE = 5;
        private const int INVENTORY_GAP = 40;
        private const int HEART_GAP = 30;
        private const int FULL_HEART = 20;
        private const int HALF_HEART = 10;
        private const string FULL = "Full";
        private const string HALF = "Half";
        private const string EMPTY = "Empty";
        private const string TOP = "Top";
        private const string BOTTOM = "Bottom";
        private const int HEARTLOCX = 7;
        private const int HEARTLOCY = 9;
        private const int HUD_HEIGHT = 159;
        private int bottomAdjust;


        private static readonly HUD instance = new HUD();

        public static HUD Instance
        {
            get { return instance; }
        }

        public HUD()
        {
        }

        public void LoadHUD(Game1 game)
        {
            HUDTexture = game.Content.Load<Texture2D>("HUDandInv/FullInventory");
            Point drawSize = Camera.Instance.HUDArea.Size;
            HUDMap.Instance.LoadHUDMap(game, drawSize);
            sprite = new HUDSprite(HUDTexture, drawSize);
            bottomAdjust = game.Window.ClientBounds.Height - HUD_HEIGHT;
            HUDfont = game.Content.Load<SpriteFont>("HUDandInv/HUDText");

            rupeeCountLocation = new Vector2(drawSize.X / THIRD + (ABUFFER/ 2), game.Window.ClientBounds.Height / (FIVE + THIRD));
            rupeeCountBottomLocation = new Vector2(rupeeCountLocation.X, rupeeCountLocation.Y + bottomAdjust + 10);
            keyCountLocation = new Vector2(rupeeCountLocation.X, rupeeCountLocation.Y + INVENTORY_GAP);
            keyCountBottomLocation = new Vector2(keyCountLocation.X, keyCountLocation.Y + bottomAdjust + 10);
            bombCountLocation = new Vector2(keyCountLocation.X, keyCountLocation.Y + (INVENTORY_GAP / 2));
            bombCountBottomLocation = new Vector2(bombCountLocation.X, bombCountLocation.Y + bottomAdjust + 10);

            bSlotLocation = new Vector2(drawSize.X / 2 + BBUFFER, drawSize.Y * THIRD / FIVE);
            bSlotBottomLocation = new Vector2(bSlotLocation.X, bSlotLocation.Y + bottomAdjust);
            aSlotLocation = new Vector2(drawSize.X / 2 + ABUFFER, drawSize.Y * THIRD / FIVE);
            aSlotBottomLocation = new Vector2(aSlotLocation.X, aSlotLocation.Y + bottomAdjust);
            InitializeSlotItems();

            firstHeartLoc = new Vector2(game.Window.ClientBounds.Width * FIVE / HEARTLOCX, game.Window.ClientBounds.Height * 2 / HEARTLOCY);
            firstBottomHeartLoc = new Vector2(firstHeartLoc.X, firstHeartLoc.Y + bottomAdjust);
            InitializeHearts();

            bottomCorner = new Vector2(0, bottomAdjust);
        }

        private void InitializeHearts()
        {
            int i;
            for (i = 0; i < maxHearts; i++)
            {
                drawnHearts.Add(CreateHeart(FULL, i, TOP));
                drawnHeartsBottom.Add(CreateHeart(FULL, i, BOTTOM));
            }
        }

        public IItems CreateHeart(string type, int position, string place)
        {
            Vector2 location;
            if (place is TOP)
            {
                location = new Vector2(firstHeartLoc.X + position * HEART_GAP, firstHeartLoc.Y);
            }
            else
            {
                location = new Vector2(firstBottomHeartLoc.X + position * HEART_GAP, firstBottomHeartLoc.Y);
            }

            if (type is FULL)
            {
                return new FullHeart(ItemsFactory.Instance.CreateFullHeartSprite(), location);
            }
            else if (type is HALF)
            {
                return new HalfHeart(ItemsFactory.Instance.CreateHalfHeartSprite(), location);
            }
            else
            {
                return new EmptyHeart(ItemsFactory.Instance.CreateEmptyHeartSprite(), location);
            }           
            
        }
        private void InitializeSlotItems()
        {
            aSlot = LinkInventory.PrimaryItem.WoodenSword;
            aSlotItems.Add(aSlot, CreateAItem(aSlot, TOP));
            aSlotItemsBottom.Add(aSlot, CreateAItem(aSlot, BOTTOM));

            bSlot = LinkInventory.SecondaryItem.Arrow;
            bSlotItems.Add(bSlot, CreateBItem(bSlot, TOP));
            bSlotItemsBottom.Add(bSlot, CreateBItem(bSlot, BOTTOM));
        }

        public void IncreaseMaxHeartNumber()
        {
            maxHearts++;
        }

        public void UpdateHearts(LinkPlayer link)
        {
            if (prevHealth < 0)
            {
                prevHealth = (int) link.Health;
            }

            if (prevHealth == (int)link.Health)
            {
                return;
            }

            fullCount = 0;
            emptyCount = 0;
            halfCount = 0;
            int healthLost = (int)(link.FullHealth - link.Health);
            if (healthLost > 0)
            {
                if (healthLost % FULL_HEART == 0)
                {
                    emptyCount = healthLost / FULL_HEART;
                }
                else if (healthLost % HALF_HEART == 0)
                {
                    halfCount = (healthLost / HALF_HEART) % 2;
                    emptyCount = (healthLost - HALF_HEART) / FULL_HEART;
                }
            }
            fullCount = maxHearts - halfCount - emptyCount;

            foreach (IItems item in drawnHearts)
            {
                item.Expire();
            }
            drawnHearts.Clear();
            drawnHeartsBottom.Clear();

            int i;
            for (i=0; i<maxHearts; i++)
            {
                if (fullCount > 0)
                {
                    drawnHearts.Add(CreateHeart(FULL, i, TOP));
                    drawnHeartsBottom.Add(CreateHeart(FULL, i, BOTTOM));
                    fullCount--;
                }
                else if (halfCount > 0)
                {
                    drawnHearts.Add(CreateHeart(HALF, i, TOP));
                    drawnHeartsBottom.Add(CreateHeart(HALF, i, BOTTOM));
                    halfCount--;
                }
                else
                {
                    drawnHearts.Add(CreateHeart(EMPTY, i, TOP));
                    drawnHeartsBottom.Add(CreateHeart(EMPTY, i, BOTTOM));
                    emptyCount--;
                }
            }

        }

        public void SetBSlotItem(LinkInventory.SecondaryItem item)
        {
            if (!bSlotItems.ContainsKey(item))
            {
                bSlotItems.Add(item, CreateBItem(item, TOP));
            }
            if (!bSlotItemsBottom.ContainsKey(item))
            {
                bSlotItemsBottom.Add(item, CreateBItem(item, BOTTOM));
            }

            bSlot = item;

        }

        private IItems CreateBItem(LinkInventory.SecondaryItem item, string place)
        {
            Vector2 loc;
            if (place is TOP)
            {
                loc = bSlotLocation;
            }
            else
            {
                loc = bSlotBottomLocation;
            }

            switch (item)
            {
                case LinkInventory.SecondaryItem.Candle:
                    return new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), loc);
                    break;

                case LinkInventory.SecondaryItem.Arrow:
                    return new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(direction), loc);
                    break;

                case LinkInventory.SecondaryItem.Bomb:
                    return new BombObject(ItemsFactory.Instance.CreateBombSprite(), loc);
                    break;

                case LinkInventory.SecondaryItem.Boomerang:
                    return new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), loc);
                    break;

                case LinkInventory.SecondaryItem.Bow:
                    return new Bow(ItemsFactory.Instance.CreateBowSprite(), loc);
                    break;

                default: //potion
                    return new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), loc);
                    break;
            }
        }

        public void SetASlotItem(LinkInventory.PrimaryItem item)
        {          
            
            if (!aSlotItems.ContainsKey(item))
            {
                aSlotItems.Add(item, CreateAItem(item, TOP));
            }
            if (!aSlotItemsBottom.ContainsKey(item))
            {
                aSlotItemsBottom.Add(item, CreateAItem(item, BOTTOM));
            }

            aSlot = item;
        }

        private IItems CreateAItem(LinkInventory.PrimaryItem item, string place)
        {
            Vector2 loc;
            if (place is TOP)
            {
                loc = aSlotLocation;
            }
            else
            {
                loc = aSlotBottomLocation;
            }

            switch (item)
            {
                case LinkInventory.PrimaryItem.WoodenSword:
                    return new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), loc);
                    break;

                case LinkInventory.PrimaryItem.SilverSword:
                    return new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), loc);
                    break;

                default: //wand
                    return new Wand(ItemsFactory.Instance.CreateWandSprite(), loc);
                    break;
            }
        }


        public void UpdateInventoryCounts(int rupeeCount, int keyCount, int bombCount)
        {
            this.rupeeCount = rupeeCount;
            this.keyCount = keyCount;
            this.bombCount = bombCount;
        }

        public void DrawTop(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Vector2.Zero, 0, Color.White);
            HUDMap.Instance.Draw(spriteBatch, 0);
            if (bSlotItems.ContainsKey(bSlot))
            {
                bSlotItems[bSlot].Draw(spriteBatch);
            }
            aSlotItems[aSlot].Draw(spriteBatch);
            spriteBatch.DrawString(HUDfont, "X" + rupeeCount, rupeeCountLocation, Color.White);
            spriteBatch.DrawString(HUDfont, "X" + keyCount, keyCountLocation, Color.White);
            spriteBatch.DrawString(HUDfont, "X" + bombCount, bombCountLocation, Color.White);
            foreach (IItems heart in drawnHearts)
            {
                heart.Draw(spriteBatch);
            }
        }

        public void DrawBottom(SpriteBatch spriteBatch)
        {
            HUDMap.Instance.Draw(spriteBatch, 1);
            if (bSlotItemsBottom.ContainsKey(bSlot))
            {
                bSlotItemsBottom[bSlot].Draw(spriteBatch);
            }
            aSlotItemsBottom[aSlot].Draw(spriteBatch); ;
            spriteBatch.DrawString(HUDfont, "X" + rupeeCount, rupeeCountBottomLocation, Color.White);
            spriteBatch.DrawString(HUDfont, "X" + keyCount, keyCountBottomLocation, Color.White);
            spriteBatch.DrawString(HUDfont, "X" + bombCount, bombCountBottomLocation, Color.White);
            foreach (IItems heart in drawnHeartsBottom)
            {
                heart.Draw(spriteBatch);
            }
        }


        public void Reset()
        {
            UpdateInventoryCounts(0, 0, 0);

            aSlotItems.Clear();
            bSlotItems.Clear();
            aSlotItemsBottom.Clear();
            bSlotItemsBottom.Clear();
            InitializeSlotItems();

            maxHearts = THIRD;
            drawnHearts.Clear();
            drawnHeartsBottom.Clear();
            InitializeHearts();

            HUDMap.Instance.Reset();
        }

    }
}
