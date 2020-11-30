using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static Sprint5.LinkInventory;

namespace Sprint5.Inventory
{
    public class InventoryItemsInitializer
    {
        private const string DIRECTION = "Up";
        private const int CURSORMAX = 8;
        private const int ITEMS_GAP = 60;
        private const int CURSOR_SIZE = 40;
        private const int FIVE = 5;
        private const int THREE = 3;
        private const int TWO = 2;
        private const int FIFTY = 50;
        private const int THIRTY = 30;
        private const int TEN = 10;

        private const int BOMB = 5;
        private const int KEY = 1;
        private readonly InventoryItemsStorage itemStorage = InventoryItemsStorage.Instance;

        private static readonly InventoryItemsInitializer instance = new InventoryItemsInitializer();
        public static InventoryItemsInitializer Instance
        {
            get { return instance; }
        }

        public InventoryItemsInitializer() {        }

        public void InitializeItems(Point bgSize, Texture2D cursorText)
        {
            Vector2 row1 = new Vector2(bgSize.X / TWO - THREE + FIFTY, bgSize.Y / FIVE + FIVE * TWO + TWO);
            Point cursor = new Point((int)row1.X - FIFTY, (int)row1.Y - THIRTY);
            Vector2 item = new Vector2((float)cursor.X + (CURSOR_SIZE / TWO), (float)cursor.Y + TEN);
            float startX = item.X;
            Vector2 currentItemLoc = new Vector2(row1.X / TWO - TEN + TWO, item.Y + FIVE);
            InitializeCursor(cursor, cursorText);

            itemStorage.SecondItems.Add(SecondaryItem.Boomerang, new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), item));
            item.X += ITEMS_GAP;
            itemStorage.SecondItems.Add(SecondaryItem.Bomb, new BombObject(ItemsFactory.Instance.CreateBombSprite(), item));
            item.X += ITEMS_GAP;
            itemStorage.SecondItems.Add(SecondaryItem.Candle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), item));
            item.X += ITEMS_GAP;
            itemStorage.SecondItems.Add(SecondaryItem.Arrow, new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(DIRECTION), item));
            itemStorage.SecondItems.Add(SecondaryItem.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), item));
            item.X = startX;
            item.Y += CURSOR_SIZE;
            itemStorage.SecondItems.Add(SecondaryItem.Potion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), item));
            item.X += ITEMS_GAP;
            itemStorage.FirstItems.Add(PrimaryItem.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), item));
            item.X += ITEMS_GAP;
            itemStorage.FirstItems.Add(PrimaryItem.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), item));
            item.X += ITEMS_GAP;
            itemStorage.FirstItems.Add(PrimaryItem.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), item));

            itemStorage.SecondInInventory.Add(SecondaryItem.Boomerang, false);
            itemStorage.SecondInInventory.Add(SecondaryItem.Bomb, true);
            itemStorage.SecondInInventory.Add(SecondaryItem.Arrow, true);
            itemStorage.SecondInInventory.Add(SecondaryItem.Bow, false);
            itemStorage.SecondInInventory.Add(SecondaryItem.Candle, true);
            itemStorage.SecondInInventory.Add(SecondaryItem.Potion, true);
            itemStorage.FirstInInventory.Add(PrimaryItem.WoodenSword, true);
            itemStorage.FirstInInventory.Add(PrimaryItem.SilverSword, true);
            itemStorage.FirstInInventory.Add(PrimaryItem.Wand, true);

            itemStorage.CurrentSecondItems.Add(SecondaryItem.Boomerang, new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), currentItemLoc)); ;
            itemStorage.CurrentSecondItems.Add(SecondaryItem.Bomb, new BombObject(ItemsFactory.Instance.CreateBombSprite(), currentItemLoc));
            itemStorage.CurrentSecondItems.Add(SecondaryItem.Arrow, new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(DIRECTION), currentItemLoc));
            itemStorage.CurrentSecondItems.Add(SecondaryItem.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), currentItemLoc));
            itemStorage.CurrentSecondItems.Add(SecondaryItem.Candle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), currentItemLoc));
            itemStorage.CurrentSecondItems.Add(SecondaryItem.Potion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), currentItemLoc));
            itemStorage.CurrentFirstItems.Add(PrimaryItem.WoodenSword, new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), currentItemLoc));
            itemStorage.CurrentFirstItems.Add(PrimaryItem.SilverSword, new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), currentItemLoc));
            itemStorage.CurrentFirstItems.Add(PrimaryItem.Wand, new Wand(ItemsFactory.Instance.CreateWandSprite(), currentItemLoc));

            itemStorage.SecondSlotItem = SecondaryItem.Bomb;
            itemStorage.FirstSlotItem = PrimaryItem.WoodenSword;
            HUD.Instance.SetBSlotItem(SecondaryItem.Arrow);
            HUD.Instance.SetASlotItem(PrimaryItem.WoodenSword);

            InitializeCounts();
        }

        private void InitializeCursor(Point cursor, Texture2D cursorTexture)
        {
            Point cursorSize = new Point(CURSOR_SIZE, CURSOR_SIZE);
            cursor.X += FIVE;
            cursor.Y += TWO;
            int startX = cursor.X;
            int i;
            for (i = 0; i < CURSORMAX / 2; i++)
            {
                itemStorage.CursorLocation.Add(i, new CursorSprite(cursorTexture, cursorSize, cursor));
                cursor.X += ITEMS_GAP;
            }

            cursor.X = startX;
            cursor.Y += CURSOR_SIZE;
            for (i = CURSORMAX / 2; i <= CURSORMAX; i++)
            {
                itemStorage.CursorLocation.Add(i, new CursorSprite(cursorTexture, cursorSize, cursor));
                cursor.X += ITEMS_GAP;
            }

            itemStorage.CursorPosition = 0;
        }

        private void InitializeCounts()
        {
            LinkInventory.Instance.RupeeCount = 0;
            LinkInventory.Instance.BombCount = BOMB;
            LinkInventory.Instance.KeyCount = KEY;
            LinkInventory.Instance.PotionCount = KEY;
        }

        public void Reset()
        {
            InitializeCounts();

            itemStorage.SecondInInventory.Clear();
            itemStorage.SecondInInventory.Add(SecondaryItem.Boomerang, false);
            itemStorage.SecondInInventory.Add(SecondaryItem.Bomb, true);
            itemStorage.SecondInInventory.Add(SecondaryItem.Arrow, true);
            itemStorage.SecondInInventory.Add(SecondaryItem.Bow, false);
            itemStorage.SecondInInventory.Add(SecondaryItem.Candle, true);
            itemStorage.SecondInInventory.Add(SecondaryItem.Potion, true);

            itemStorage.SecondSlotItem = SecondaryItem.Boomerang;
            itemStorage.FirstSlotItem = PrimaryItem.WoodenSword;
            HUD.Instance.SetBSlotItem(SecondaryItem.Arrow);
            HUD.Instance.SetASlotItem(PrimaryItem.WoodenSword);

            itemStorage.CursorPosition = 0;

            InventoryMap.Instance.Reset();
        }

    }
}
