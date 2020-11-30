using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;
using Sprint5.Menus;

namespace Sprint5
{
    public class LinkInventory
    {
        public enum SecondaryItem
        {
            Boomerang, Bomb, Arrow, Bow, Candle, Potion
        }

        public enum PrimaryItem
        {
            WoodenSword, SilverSword, Wand
        }

        private bool showInventory = false;
        private bool showBlueRing = false;
        private Texture2D emptyBG;
        private Texture2D compBG;
        private Texture2D mapBG;
        private Texture2D fullBG;
        private Texture2D cursorTexture;
        private Texture2D ringTexture;
        private int rupeeCount;
        private int bombCount;
        private int keyCount;
        private int potionCount;
        private ISprite defaultInvSprite;
        private ISprite compassInvSprite;
        private ISprite mapInvSprite;
        private ISprite fullInvSprite;
        private ISprite drawnSprite;
        private ISprite blueRingSprite;
        private IItems prevItem = null;
        private SecondaryItem secondSlotItem;
        private Dictionary<SecondaryItem, IItems> secondItems = new Dictionary<SecondaryItem, IItems>();
        private Dictionary<SecondaryItem, Boolean> secondInInventory = new Dictionary<SecondaryItem, Boolean>();
        private Dictionary<SecondaryItem, IItems> currentSecondItems = new Dictionary<SecondaryItem, IItems>();

        private PrimaryItem firstSlotItem;
        private Dictionary<PrimaryItem, IItems> firstItems = new Dictionary<PrimaryItem, IItems>();
        private Dictionary<PrimaryItem, Boolean> firstInInventory = new Dictionary<PrimaryItem, Boolean>();
        private Dictionary<PrimaryItem, IItems> currentFirstItems = new Dictionary<PrimaryItem, IItems>();

        private Dictionary<int, ISprite> cursorLocation = new Dictionary<int, ISprite>();

        private const string DIRECTION = "Up";
        private int cursorPosition = 0;
        private const int CURSORMAX = 8;
        private const int THREE = 3;
        private const int FIFTY = 50;
        private const int ITEMS_GAP = 60;
        private const int CURSOR_GAP = 75;
        private const int CURSOR_ADJUST = 30;
        private const int ITEM_ADJUST_X = 20;
        private const int ITEM_ADJUST_Y = 60/4;
        private const int TWO = 2;
        private const int FIVE = 5;
        private const int CURSOR_SIZE = 40;
        private const int BOMB = 5;
        private const int KEY = 1;

        private static readonly LinkInventory instance = new LinkInventory();


        public static LinkInventory Instance
        {
            get { return instance; }
        }

        public LinkInventory()
        {
        }

        public void InitializeInventory(Game1 game)
        {
            emptyBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_Empty");
            compBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_Comp"); ;
            mapBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_Map"); ;
            fullBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_CompMap"); ;
            cursorTexture = game.Content.Load<Texture2D>("HUDandInv/cursor");
            ringTexture = game.Content.Load<Texture2D>("HUDandInv/InventoryBlueRing");

            Point dimensions = new Point(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            dimensions = Camera.Instance.playArea.Size;
            defaultInvSprite = new InventorySprite(emptyBG, dimensions);
            compassInvSprite = new InventorySprite(compBG, dimensions);
            mapInvSprite = new InventorySprite(mapBG, dimensions);
            fullInvSprite = new InventorySprite(fullBG, dimensions);
            drawnSprite = defaultInvSprite;
            blueRingSprite = new InventorySprite(ringTexture, dimensions);

            Point bgSize = ((InventorySprite)defaultInvSprite).InventorySize;

            Inventory.InventoryMap.Instance.LoadInventoryMap(game, bgSize);

            InitializeItems(bgSize);
            InitializeCounts();

        }

        private void InitializeItems(Point bgSize)
        {
            Vector2 row1 = new Vector2(bgSize.X / 2 - THREE + FIFTY, bgSize.Y / FIVE + FIVE*TWO + TWO);
            Point cursor = new Point((int)row1.X - FIFTY, (int)row1.Y - CURSOR_ADJUST);

            InitializeCursor(cursor, bgSize);

            Vector2 item = new Vector2((float) cursor.X + (CURSOR_SIZE / 2), (float) cursor.Y + FIVE * TWO);
            float startX = item.X;
            Vector2 currentItemLoc = new Vector2(row1.X / 2 - (ITEM_ADJUST_X / 2) + TWO, item.Y + FIVE);

            secondItems.Add(SecondaryItem.Boomerang, new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), item));
            item.X += ITEMS_GAP;
            secondItems.Add(SecondaryItem.Bomb, new BombObject(ItemsFactory.Instance.CreateBombSprite(), item));
            item.X += ITEMS_GAP;
            secondItems.Add(SecondaryItem.Candle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), item));
            item.X += ITEMS_GAP;
            secondItems.Add(SecondaryItem.Arrow, new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(DIRECTION), item));
            secondItems.Add(SecondaryItem.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), item));
            item.X = startX;
            item.Y += CURSOR_SIZE;
            secondItems.Add(SecondaryItem.Potion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), item));
            item.X += ITEMS_GAP;
            firstItems.Add(PrimaryItem.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), item));
            item.X += ITEMS_GAP;
            firstItems.Add(PrimaryItem.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), item));
            item.X += ITEMS_GAP;
            firstItems.Add(PrimaryItem.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), item));

            secondInInventory.Add(SecondaryItem.Boomerang, false);
            secondInInventory.Add(SecondaryItem.Bomb, true);
            secondInInventory.Add(SecondaryItem.Arrow, true);
            secondInInventory.Add(SecondaryItem.Bow, false);
            secondInInventory.Add(SecondaryItem.Candle, true);
            secondInInventory.Add(SecondaryItem.Potion, true);
            firstInInventory.Add(PrimaryItem.WoodenSword, true);
            firstInInventory.Add(PrimaryItem.SilverSword, true);
            firstInInventory.Add(PrimaryItem.Wand, true);

            currentSecondItems.Add(SecondaryItem.Boomerang, new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), currentItemLoc)); ;
            currentSecondItems.Add(SecondaryItem.Bomb, new BombObject(ItemsFactory.Instance.CreateBombSprite(), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Arrow, new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(DIRECTION), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Candle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Potion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), currentItemLoc));
            currentFirstItems.Add(PrimaryItem.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), currentItemLoc));
            currentFirstItems.Add(PrimaryItem.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), currentItemLoc));
            currentFirstItems.Add(PrimaryItem.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), currentItemLoc));

            HUD.Instance.SetBSlotItem(SecondaryItem.Arrow);

            secondSlotItem = SecondaryItem.Bomb;
            firstSlotItem = PrimaryItem.WoodenSword;
        }

        private void InitializeCursor(Point cursor, Point inventoryBG)
        {
            Point cursorSize = new Point(CURSOR_SIZE, CURSOR_SIZE);
            cursor.X += FIVE;
            cursor.Y += TWO;
            int startX = cursor.X;
            int i;
            for (i=0; i < CURSORMAX/2; i++)
            {
                cursorLocation.Add(i, new CursorSprite(cursorTexture, cursorSize, cursor));
                cursor.X += ITEMS_GAP;
            }

            cursor.X = startX;
            cursor.Y += CURSOR_SIZE;
            for (i = CURSORMAX/2; i <= CURSORMAX; i++)
            {
                cursorLocation.Add(i, new CursorSprite(cursorTexture, cursorSize, cursor));
                cursor.X += ITEMS_GAP;
            }
        }

        private void InitializeCounts()
        {
            RupeeCount = 0;
            BombCount = BOMB;
            KeyCount = KEY;
            PotionCount = KEY;

        }

        public void Reset()
        {
            InitializeCounts();

            foreach (KeyValuePair<SecondaryItem, Boolean> item in secondInInventory)
            {
                secondInInventory[item.Key] = false;
            }

            secondSlotItem = SecondaryItem.Boomerang;
            firstSlotItem = PrimaryItem.WoodenSword;
        }

        public bool ShowInventory
        {
            get { return showInventory; }
            set { showInventory = value; }
        }

        public bool ShowBlueRing
        {
            get { return showBlueRing; }
            set { showBlueRing = value; }
        }

        public int RupeeCount
        {
            get { return rupeeCount; }
            set { rupeeCount = value; }
        }

        public int BombCount
        {
            get { return bombCount; }
            set { bombCount = value; }
        }

        public int KeyCount
        {
            get { return keyCount; }
            set { keyCount = value; }
        }

        public int PotionCount
        {
            get { return potionCount; }
            set { potionCount = value; }
        }

        public bool HasBow
        {
            get { return secondInInventory[SecondaryItem.Bow]; }
        }

        public bool HasCandle
        {
            get { return secondInInventory[SecondaryItem.Candle]; }
        }


        public bool HasBoomerang
        {
            get { return secondInInventory[SecondaryItem.Boomerang]; }
        }

        public IItems GetSecondItem
        {
            get { return secondItems[secondSlotItem]; }
        }

        public void PickUpItem(IItems item, LinkPlayer link)
        {

            if (!(prevItem is null) && prevItem.Equals(item))
            {
                return;
            }

            if (item is BoomerangObject)
            {
                secondInInventory[SecondaryItem.Boomerang] = true;
                Sounds.Instance.PlaySoundEffect("Fanfare");
            }
            else if (item is BombObject)
            {
                secondInInventory[SecondaryItem.Bomb] = true;
                Sounds.Instance.PlaySoundEffect("GetItem");
            }
            else if (item is Bow)
            {
                secondInInventory[SecondaryItem.Bow] = true;
                Sounds.Instance.PlaySoundEffect("Fanfare");
            }
            else if (item is BluePotion)
            {
                secondInInventory[SecondaryItem.Potion] = true;
                PotionCount++;
                Sounds.Instance.PlaySoundEffect("GetItem");
            }
            else if (item is BlueCandle)
            {
                secondInInventory[SecondaryItem.Candle] = true;
                Sounds.Instance.PlaySoundEffect("GetItem");
            }
            else if (item is ArrowObject)
            {
                secondInInventory[SecondaryItem.Arrow] = true;
                Sounds.Instance.PlaySoundEffect("GetItem");
            }
            else if (item is Rupee)
            {
                RupeeCount++;
                Sounds.Instance.PlaySoundEffect("GetRupee");
            }
            else if (item is BombObject)
            {
                BombCount++;
                Sounds.Instance.PlaySoundEffect("GetItem");
            }
            else if (item is Key)
            {
                KeyCount++;
                Sounds.Instance.PlaySoundEffect("GetHeart");
            }
            else if (item is Map)
            {
                HUDMap.Instance.HasMap = true;
                Sounds.Instance.PlaySoundEffect("Fanfare");
            }
            else if (item is Compass)
            {
                HUDMap.Instance.HasCompass = true;
                Sounds.Instance.PlaySoundEffect("Fanfare");
            }
            else if (item is Clock)
            {
                link.Clock = true;
                RoomEnemies.Instance.StunAllEnemies();
                Sounds.Instance.PlaySoundEffect("Fanfare");
            }
            else if (item is BlueRing)
            {
                link.UseRing = true;
                 ShowBlueRing = true;
                Sounds.Instance.PlaySoundEffect("Fanfare");
            }
            else if (item is Fairy)
            {
                Sounds.Instance.PlaySoundEffect("GetHeart");
            }
            prevItem = item;
        }

        public void ConsumeItem(LinkPlayer link)
        {
            if (!secondInInventory[secondSlotItem])
            {
                return;
            }

            if (secondSlotItem == SecondaryItem.Potion)
            {
                if (PotionCount > 0)
                {
                    link.Health = link.FullHealth;
                    HUD.Instance.UpdateHearts(link);
                    PotionCount--;
                    GameOverScreen.Instance.ItemsConsumed++;

                    if (PotionCount == 0)
                    {
                        secondInInventory[SecondaryItem.Potion] = false;
                    }
                }
                
            }
        }

        public void UseItem(IItems item)
        {
            
            if (item is Key)
            {
                KeyCount--;
                GameOverScreen.Instance.ItemsConsumed++;
            }
            if (item is BombObject)
            {
                BombCount--;
                GameOverScreen.Instance.ItemsConsumed++;
            }
        }


        public void MoveCursor(bool goingRight)
        {
            if (goingRight)
            {
                if (cursorPosition == CURSORMAX - 1)
                {
                    cursorPosition = 0;
                }
                else
                {
                    cursorPosition++;
                }
            }
            else
            {
                if (cursorPosition == 0)
                {
                    cursorPosition = CURSORMAX - 1;
                }
                else
                {
                    cursorPosition--;
                }
            }

            UpdateSlotItem();
        }

        private void UpdateSlotItem()
        {
            switch (cursorPosition)
            {
                case 0:
                    secondSlotItem = SecondaryItem.Boomerang;
                    break;
                case 1:
                    secondSlotItem = SecondaryItem.Bomb;
                    break;
                case 2:
                    secondSlotItem = SecondaryItem.Candle;
                    break;
                case 3:
                    if (secondInInventory[SecondaryItem.Bow])
                        secondSlotItem = SecondaryItem.Bow;
                    else
                        secondSlotItem = SecondaryItem.Arrow;
                    break;
                case 4:
                    secondSlotItem = SecondaryItem.Potion;
                    break;
                case 5:
                    firstSlotItem = PrimaryItem.WoodenSword;
                    break;
                case 6:
                    firstSlotItem = PrimaryItem.SilverSword;
                    break;
                default:
                    firstSlotItem = PrimaryItem.Wand;
                    break;
            }

            if (secondInInventory[secondSlotItem] && secondSlotItem != SecondaryItem.Potion)
            {
                HUD.Instance.SetBSlotItem(secondSlotItem);
            }

            if (firstInInventory[firstSlotItem])
            {
                HUD.Instance.SetASlotItem(firstSlotItem);
            }

        }

        public void UpdateLinkWeapons(LinkPlayer link)
        {
            switch(secondSlotItem)
            {
                case SecondaryItem.Arrow:
                case SecondaryItem.Bow:
                    link.SecondaryWeapon = ItemForLink.ArrowBow;
                    break;
                case SecondaryItem.Boomerang:
                    link.SecondaryWeapon = ItemForLink.Boomerang;
                    break;
                case SecondaryItem.Candle:
                    link.SecondaryWeapon = ItemForLink.BlueCandle;
                    break;
                case SecondaryItem.Bomb:
                    link.SecondaryWeapon = ItemForLink.Bomb;
                    break;
                default: //potion - do nothing
                    break;
            }

            switch (firstSlotItem)
            {
                case PrimaryItem.WoodenSword:
                    link.CurrentWeapon = ItemForLink.WoodenSword;
                    break;
                case PrimaryItem.SilverSword:
                    link.CurrentWeapon = ItemForLink.Sword;
                    break;
                default: //wand
                    link.CurrentWeapon = ItemForLink.MagicalRod;
                    break;
            }
        }


        private void UpdateBGSprite()
        {
            if (HUDMap.Instance.HasCompass && HUDMap.Instance.HasMap)
            {
                drawnSprite = fullInvSprite;
            }
            else if (HUDMap.Instance.HasCompass)
            {
                drawnSprite = compassInvSprite;
            }
            else if (HUDMap.Instance.HasMap)
            {
                drawnSprite = mapInvSprite;
            }
            else
            {
                drawnSprite = defaultInvSprite;
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            UpdateBGSprite();
            drawnSprite.Draw(spriteBatch, Vector2.Zero, 0, Color.White);

            InventoryMap.Instance.Draw(spriteBatch);
            
            foreach (KeyValuePair<SecondaryItem, Boolean> pair in secondInInventory)
            {
                if (pair.Value)
                {
                    secondItems[pair.Key].Draw(spriteBatch);
                }
            }

            foreach (KeyValuePair<PrimaryItem, Boolean> pair in firstInInventory)
            {
                if (pair.Value)
                {
                    firstItems[pair.Key].Draw(spriteBatch);
                }
            }

            if (cursorPosition < CURSORMAX / 2 + 1)
            {
                if (secondInInventory[secondSlotItem])
                {
                    currentSecondItems[secondSlotItem].Draw(spriteBatch);
                }
            }
            else
            {
                if (firstInInventory[firstSlotItem])
                {
                    currentFirstItems[firstSlotItem].Draw(spriteBatch);
                }
            }            

            cursorLocation[cursorPosition].Draw(spriteBatch, Vector2.Zero, 0, Color.White);
            
            if (ShowBlueRing)
            {
                blueRingSprite.Draw(spriteBatch, Vector2.Zero, 0, Color.White);
            }

        }

    }
}
