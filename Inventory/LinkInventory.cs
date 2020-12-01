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
using Sprint5.HUDManagement;

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

        private InventoryItemsInitializer linkItemStart = InventoryItemsInitializer.Instance;
        private InventoryItemManagement linkItemManagement = InventoryItemManagement.Instance;
        private readonly CursorManagement cursorManagement = CursorManagement.Instance;
        private InventoryItemsStorage itemsStorage = InventoryItemsStorage.Instance;
        private static readonly LinkInventory instance = new LinkInventory();

        public static LinkInventory Instance
        {
            get { return instance; }
        }

        public LinkInventory() {         }

        public void InitializeInventory(Game1 game)
        {
            emptyBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_Empty");
            compBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_Comp"); ;
            mapBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_Map"); ;
            fullBG = game.Content.Load<Texture2D>("HUDandInv/FullInv_CompMap"); ;
            cursorTexture = game.Content.Load<Texture2D>("HUDandInv/cursor");
            ringTexture = game.Content.Load<Texture2D>("HUDandInv/InventoryBlueRing");

            Point dimensions = Camera.Instance.playArea.Size;
            defaultInvSprite = new InventorySprite(emptyBG, dimensions);
            compassInvSprite = new InventorySprite(compBG, dimensions);
            mapInvSprite = new InventorySprite(mapBG, dimensions);
            fullInvSprite = new InventorySprite(fullBG, dimensions);
            drawnSprite = defaultInvSprite;
            blueRingSprite = new InventorySprite(ringTexture, dimensions);

            Point bgSize = ((InventorySprite)defaultInvSprite).InventorySize;

            Inventory.InventoryMap.Instance.LoadInventoryMap(game, bgSize);
            linkItemStart.InitializeItems(bgSize, cursorTexture);
        }

        public bool ShowInventory { get => showInventory; set => showInventory = value; }
        public bool ShowBlueRing { get => showBlueRing; set => showBlueRing = value; }
        public int RupeeCount { get => rupeeCount; set => rupeeCount = value; }
        public int BombCount { get => bombCount; set => bombCount = value; }
        public int KeyCount { get => keyCount; set => keyCount = value; }
        public int PotionCount { get => potionCount; set => potionCount = value; }
        public bool HasBow { get => itemsStorage.SecondInInventory[SecondaryItem.Bow]; }
        public bool HasCandle { get => itemsStorage.SecondInInventory[SecondaryItem.Candle]; }
        public bool HasBoomerang { get => itemsStorage.SecondInInventory[SecondaryItem.Boomerang]; }
        public IItems GetSecondItem { get => itemsStorage.SecondItems[itemsStorage.SecondSlotItem]; }

        public void Reset()
        {
            linkItemStart.Reset();            
        }

        public void PickUpItem(IItems item, LinkPlayer link)
        {

            linkItemManagement.PickUpItem(item, link);

        }

        public void ConsumeItem(LinkPlayer link)
        {
            linkItemManagement.ConsumeItem(link);
        }

        public void UseItem(IItems item)
        {
            linkItemManagement.UseItem(item);
        }

        public void MoveCursor(bool goingRight)
        {
            cursorManagement.MoveCursor(goingRight);
        }

        public void UpdateLinkWeapons(LinkPlayer link)
        {
            linkItemManagement.UpdateLinkWeapons(link);
        }

        private void UpdateBGSprite()
        {
            if (HUDMap.Instance.HasCompass && HUDMap.Instance.HasMap) drawnSprite = fullInvSprite; 
            else if (HUDMap.Instance.HasCompass) drawnSprite = compassInvSprite; 
            else if (HUDMap.Instance.HasMap) drawnSprite = mapInvSprite; 
            else  drawnSprite = defaultInvSprite; 
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            UpdateBGSprite();
            drawnSprite.Draw(spriteBatch, Vector2.Zero, 0, Color.White);
            InventoryDrawingLogic.Instance.DrawInventory(spriteBatch); if (ShowBlueRing)
            {
                blueRingSprite.Draw(spriteBatch, Vector2.Zero, 0, Color.White);
            }
        }
    }
}
