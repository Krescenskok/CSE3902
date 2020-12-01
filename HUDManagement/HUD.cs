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
        private HUDStorage storage = HUDStorage.Instance;
        private HUDInitializer initializer = HUDInitializer.Instance;
        private SlotManagement slotManager = SlotManagement.Instance;
        private HeartManagement heartManager = HeartManagement.Instance;
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
            drawSize.Y /= 3;
            HUDMap.Instance.LoadHUDMap(game, drawSize);
            sprite = new HUDSprite(HUDTexture, drawSize);
            storage.BottomAdjust = game.Window.ClientBounds.Height - drawSize.Y;
            HUDfont = game.Content.Load<SpriteFont>("HUDandInv/HUDText");

            initializer.InitializeHUD(drawSize);
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
            HUDMap.Instance.Draw(spriteBatch, 1);
            if (storage.BSlotItemsBottom.ContainsKey(storage.BSlot))
            {
                storage.BSlotItemsBottom[storage.BSlot].Draw(spriteBatch);
            }
            storage.ASlotItemsBottom[storage.ASlot].Draw(spriteBatch); ;
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
            HUDMap.Instance.Reset();
        }

    }
}
