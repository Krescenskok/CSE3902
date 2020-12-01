using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.HUDManagement;
using Sprint5.Items;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Sprint5
{
    public class HUD
    {
        private Texture2D HUDTexture;
        private ISprite sprite;
        private SpriteFont HUDfont;

        private const int FIRST = 1;
        private const int THIRD = 3;
        private const int FIVE = 5;
        private const int TEN = 10;
        private const int BBUFFER = 5;
        private const int ABUFFER = 60;
        private const int INVENTORY_GAP = 30;
        private const int TEXT_GAP = 20;
        private const int HEARTLOCX = 7;

        private HUDStorage storage = HUDStorage.Instance;
        private SlotManagement slotManager = SlotManagement.Instance;
        private HeartManagement heartManager = HeartManagement.Instance;
        private HUDMap map = HUDMap.Instance;
        private static readonly HUD instance = new HUD();

        public static HUD Instance { get { return instance; } }

        public HUD() { }

        public void LoadHUD(Game1 game)
        {
            HUDTexture = game.Content.Load<Texture2D>("HUDandInv/FullInventory");
            Point drawSize = Camera.Instance.HUDArea.Size;
            drawSize.Y /= 3;
            map.LoadHUDMap(game, drawSize);
            sprite = new HUDSprite(HUDTexture, drawSize);
            storage.BottomAdjust = game.Window.ClientBounds.Height - drawSize.Y;
            HUDfont = game.Content.Load<SpriteFont>("HUDandInv/HUDText");

            InitializeHUD(drawSize);
        }

        private void InitializeHUD(Point drawSize)
        {
            storage.RupeeCountBottomLocation = new Vector2(drawSize.X / THIRD + INVENTORY_GAP, (drawSize.Y / THIRD + TEN) + storage.BottomAdjust + TEN);
            storage.KeyCountBottomLocation = new Vector2(storage.RupeeCountBottomLocation.X, (storage.RupeeCountBottomLocation.Y + INVENTORY_GAP + THIRD));
            storage.BombCountBottomLocation = new Vector2(storage.RupeeCountBottomLocation.X, (storage.KeyCountBottomLocation.Y + TEXT_GAP));

            storage.BSlotBottomLocation = new Vector2(drawSize.X / 2 + BBUFFER, (drawSize.Y * THIRD / FIVE + FIVE) + storage.BottomAdjust);
            storage.ASlotBottomLocation = new Vector2(storage.BSlotBottomLocation.X + ABUFFER, storage.BSlotBottomLocation.Y);
            slotManager.InitializeSlotItems();

            storage.FirstBottomHeartLoc = new Vector2(drawSize.X * FIVE / HEARTLOCX, (drawSize.Y * 2 / THIRD) + storage.BottomAdjust);
            heartManager.InitializeHearts();
        }

        public void IncreaseMaxHeartNumber()
        {
            heartManager.IncreaseMaxHeartNumber();
        }

        public void UpdateHearts(LinkPlayer link)
        {
            heartManager.UpdateHearts(link);
        }

        public void SetBSlotItem(LinkInventory.SecondaryItem item)
        {
            slotManager.SetBSlotItem(item);
        }

        public void SetASlotItem(LinkInventory.PrimaryItem item)
        {
            slotManager.SetASlotItem(item);
        }

        public void DrawBottom(SpriteBatch spriteBatch)
        {
            map.Draw(spriteBatch, FIRST);
            if (storage.BSlotItemsBottom.ContainsKey(storage.BSlot))
            {
                storage.BSlotItemsBottom[storage.BSlot].Draw(spriteBatch);
            }
            storage.ASlotItemsBottom[storage.ASlot].Draw(spriteBatch);

            spriteBatch.DrawString(HUDfont, "X" + LinkInventory.Instance.RupeeCount, storage.RupeeCountBottomLocation, Color.White);
            spriteBatch.DrawString(HUDfont, "X" + LinkInventory.Instance.KeyCount, storage.KeyCountBottomLocation, Color.White);
            spriteBatch.DrawString(HUDfont, "X" + LinkInventory.Instance.BombCount, storage.BombCountBottomLocation, Color.White);
            
            foreach (IItems heart in storage.DrawnHeartsBottom)
            {
                heart.Draw(spriteBatch);
            }
        }


        public void Reset()
        {
            heartManager.Reset();
            slotManager.Reset();
            map.Reset();
        }

    }
}
