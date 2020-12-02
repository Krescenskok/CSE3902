using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.HUDManagement
{
    public class HUDInitializer
    {
        private const int THIRD = 3;
        private const int FIVE = 5;
        private const int TEN = 10;
        private const int BBUFFER = 5;
        private const int ABUFFER = 60;
        private const int INVENTORY_GAP = 30;
        private const int HEARTLOCX = 7;

        private HUDStorage storage = HUDStorage.Instance;
        private SlotManagement slotManager = SlotManagement.Instance;
        private HeartManagement heartManager = HeartManagement.Instance;
        private static readonly HUDInitializer instance = new HUDInitializer();
        public static HUDInitializer Instance { get => instance; }
        public HUDInitializer() { }

        public void InitializeHUD(Point drawSize)
        {
            storage.RupeeCountLocation = new Vector2(drawSize.X / THIRD + INVENTORY_GAP, drawSize.Y / THIRD + TEN);
            storage.RupeeCountBottomLocation = new Vector2(storage.RupeeCountLocation.X, storage.RupeeCountLocation.Y + storage.BottomAdjust + TEN);
            storage.KeyCountLocation = new Vector2(storage.RupeeCountLocation.X, storage.RupeeCountLocation.Y + INVENTORY_GAP);
            storage.KeyCountBottomLocation = new Vector2(storage.KeyCountLocation.X, storage.KeyCountLocation.Y + storage.BottomAdjust + TEN);
            storage.BombCountLocation = new Vector2(storage.KeyCountLocation.X, storage.KeyCountLocation.Y + (INVENTORY_GAP * 2 / THIRD));
            storage.BombCountBottomLocation = new Vector2(storage.BombCountLocation.X, storage.BombCountLocation.Y + storage.BottomAdjust + TEN);

            storage.BSlotLocation = new Vector2(drawSize.X / 2 + BBUFFER, drawSize.Y * THIRD / FIVE + FIVE);
            storage.BSlotBottomLocation = new Vector2(storage.BSlotLocation.X, storage.BSlotLocation.Y + storage.BottomAdjust);
            storage.ASlotLocation = new Vector2(storage.BSlotLocation.X + ABUFFER, storage.BSlotLocation.Y);
            storage.ASlotBottomLocation = new Vector2(storage.ASlotLocation.X, storage.BSlotBottomLocation.Y);
            slotManager.InitializeSlotItems();

            storage.FirstHeartLoc = new Vector2(drawSize.X * FIVE / HEARTLOCX, drawSize.Y * 2 / THIRD);
            storage.FirstBottomHeartLoc = new Vector2(storage.FirstHeartLoc.X, storage.FirstHeartLoc.Y + storage.BottomAdjust);
            heartManager.InitializeHearts();
        }

        

    }
}
