using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Inventory;
using Sprint4.Items;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Text;

namespace Sprint4
{
    public class LinkInventory
    {
        public enum SecondaryItem
        {
            Boomerang, Bomb, Arrow, Bow, Candle, Potion
        }

        private bool showInventory = false;
        private Texture2D inventoryBG;
        private Texture2D cursorTexture;
        private int rupeeCount;
        private int bombCount;
        private int keyCount;
        private int potionCount;
        private ISprite inventorySprite;
        private SecondaryItem secondSlotItem;
        private Dictionary<SecondaryItem, IItems> secondItems = new Dictionary<SecondaryItem, IItems>();
        private Dictionary<SecondaryItem, Boolean> secondInInventory = new Dictionary<SecondaryItem, Boolean>();
        private Dictionary<SecondaryItem, ISprite> cursorLocation = new Dictionary<SecondaryItem, ISprite>();
        private Dictionary<SecondaryItem, IItems> currentSecondItems = new Dictionary<SecondaryItem, IItems>();

        private const string DIRECTION = "Up";
        private const int ITEMS_GAP = 80;
        private const int CURSOR_GAP = 75;
        private const int CURSOR_ADJUST = 30;
        private const int ITEM_ADJUST_X = 20;
        private const int ITEM_ADJUST_Y = 60/4;
        private const int CURSOR_SIZE = 50;
        private const int TWO = 2;
        private const int FULL_HEART = 20;
        private const int HALF_HEART = 10;

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
            RupeeCount = 0;
            BombCount = 0;
            KeyCount = 0;
            PotionCount = 0;

            inventoryBG = game.Content.Load<Texture2D>("HUDandInv/FullInventory");
            cursorTexture = game.Content.Load<Texture2D>("HUDandInv/cursor");

            inventorySprite = new InventorySprite(inventoryBG, new Point(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height));
            Point bgSize = ((InventorySprite)inventorySprite).InventorySize;

            Vector2 row1 = new Vector2(bgSize.X / 2 + ITEMS_GAP, bgSize.Y / 3 + (CURSOR_ADJUST));
            Vector2 row2 = new Vector2(bgSize.X / 2 + ITEMS_GAP, row1.Y + ITEM_ADJUST_X * 2);

            Point cursor = new Point((int) row1.X - ITEMS_GAP, (int) row1.Y - ITEMS_GAP);
            Vector2 item = new Vector2((float) cursor.X + (ITEM_ADJUST_X), (float) cursor.Y + (ITEM_ADJUST_Y));
            Vector2 currentItemLoc = new Vector2(row1.X / 2 - ITEM_ADJUST_X, item.Y);
            
            secondItems.Add(SecondaryItem.Boomerang, new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), item));
            item.X += CURSOR_GAP;
            secondItems.Add(SecondaryItem.Bomb, new BombObject(ItemsFactory.Instance.CreateBombSprite(), item));
            item.X += CURSOR_GAP;
            secondItems.Add(SecondaryItem.Arrow, new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(DIRECTION), item));
            secondItems.Add(SecondaryItem.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), item));
            item.X += CURSOR_GAP;
            secondItems.Add(SecondaryItem.Candle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), item));
            item.X = row2.X - ITEMS_GAP + ITEM_ADJUST_X;
            item.Y = row2.Y - ITEMS_GAP + ITEM_ADJUST_X;
            secondItems.Add(SecondaryItem.Potion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), item));

            secondInInventory.Add(SecondaryItem.Boomerang, false);
            secondInInventory.Add(SecondaryItem.Bomb, false);
            secondInInventory.Add(SecondaryItem.Arrow, true);
            secondInInventory.Add(SecondaryItem.Bow, false);
            secondInInventory.Add(SecondaryItem.Candle, false);
            secondInInventory.Add(SecondaryItem.Potion, true);

            Point cursorSize = new Point(CURSOR_SIZE, CURSOR_SIZE);
            cursorLocation.Add(SecondaryItem.Boomerang, new CursorSprite(cursorTexture, cursorSize, cursor));
            cursor.X += CURSOR_GAP;
            cursorLocation.Add(SecondaryItem.Bomb, new CursorSprite(cursorTexture, cursorSize, cursor));
            cursor.X += CURSOR_GAP;
            cursorLocation.Add(SecondaryItem.Arrow, new CursorSprite(cursorTexture, cursorSize, cursor));
            cursorLocation.Add(SecondaryItem.Bow, new CursorSprite(cursorTexture, cursorSize, cursor));
            cursor.X += CURSOR_GAP;
            cursorLocation.Add(SecondaryItem.Candle, new CursorSprite(cursorTexture, cursorSize, cursor));
            cursor.X = (int) row2.X - ITEMS_GAP;
            cursor.Y = (int) row2.Y - CURSOR_GAP;
            cursorLocation.Add(SecondaryItem.Potion, new CursorSprite(cursorTexture, cursorSize, cursor));

            currentSecondItems.Add(SecondaryItem.Boomerang, new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), currentItemLoc)); ;
            currentSecondItems.Add(SecondaryItem.Bomb, new BombObject(ItemsFactory.Instance.CreateBombSprite(), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Arrow, new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(DIRECTION), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Bow, new Bow(ItemsFactory.Instance.CreateBowSprite(), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Candle, new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), currentItemLoc));
            currentSecondItems.Add(SecondaryItem.Potion, new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), currentItemLoc));

            HUD.Instance.SetBSlotItem(SecondaryItem.Arrow);

        }

        public void Reset()
        {
            RupeeCount = 0;
            BombCount = 0;
            KeyCount = 0;
            
            foreach (KeyValuePair<SecondaryItem, Boolean> item in secondInInventory)
            {
                secondInInventory[item.Key] = false;
            }
        }

        public bool ShowInventory
        {
            get { return showInventory; }
            set { showInventory = value; }
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

        public IItems GetSecondItem
        {
            get { return secondItems[secondSlotItem]; }
        }

        public void PickUpItem(IItems item, LinkPlayer link)
        {
            if (item is BoomerangObject)
            {
                secondInInventory[SecondaryItem.Boomerang] = true;
            }
            else if (item is BombObject)
            {
                secondInInventory[SecondaryItem.Bomb] = true;
            }
            else if (item is Bow)
            {
                secondInInventory[SecondaryItem.Bow] = true;
            }
            else if (item is BluePotion)
            {
                secondInInventory[SecondaryItem.Potion] = true;
                PotionCount++;
            }
            else if (item is BlueCandle)
            {
                secondInInventory[SecondaryItem.Candle] = true;
            }
            else if (item is ArrowObject)
            {
                secondInInventory[SecondaryItem.Arrow] = true;
            }
            else if (item is BombObject)
            {
                BombCount++;
            }
            else if (item is Key)
            {
                KeyCount++;
            }
            else if (item is Map)
            {
                HUDMap.Instance.HasMap = true;
            }
            else if (item is Compass)
            {
                HUDMap.Instance.HasCompass = true;
            }
        }

        public void ConsumeItem(LinkPlayer link)
        {
            if (!secondInInventory[secondSlotItem])
            {
                return;
            }

            if (secondSlotItem == SecondaryItem.Potion)
            {
                secondInInventory[SecondaryItem.Potion] = false;
                link.Health = link.FullHealth;
                HUD.Instance.UpdateHearts(link);
            }
        }


        public void MoveCursor(bool goingRight)
        {
            if (goingRight)
            {
                if (secondSlotItem == SecondaryItem.Potion)
                {
                    secondSlotItem = SecondaryItem.Boomerang;
                }
                else if(secondSlotItem == SecondaryItem.Arrow)
                {
                    secondSlotItem += TWO;
                }
                else
                {
                    secondSlotItem++;
                }
            }
            else
            {
                if (secondSlotItem == SecondaryItem.Boomerang)
                {
                    secondSlotItem = SecondaryItem.Potion;
                }
                else if (secondSlotItem == SecondaryItem.Bow)
                {
                    secondSlotItem -= TWO;
                }
                else
                {

                    secondSlotItem--;
                }
            }
            if (secondInInventory[secondSlotItem] && secondSlotItem != SecondaryItem.Potion)
            {
                HUD.Instance.SetBSlotItem(secondSlotItem);
            }
        }
       

        public void Draw(SpriteBatch spriteBatch)
        {
            inventorySprite.Draw(spriteBatch, Vector2.Zero, 0, Color.White);
            
            foreach (KeyValuePair<SecondaryItem, Boolean> pair in secondInInventory)
            {
                if (pair.Value)
                {
                    secondItems[pair.Key].Draw(spriteBatch);
                }
            }

            if (secondInInventory[secondSlotItem])
            {
                currentSecondItems[secondSlotItem].Draw(spriteBatch);
            }

            cursorLocation[secondSlotItem].Draw(spriteBatch, Vector2.Zero, 0, Color.White);

        }

    }
}
