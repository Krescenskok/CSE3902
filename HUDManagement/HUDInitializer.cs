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
        private const int TEXT_GAP = 20;
        private const int HEARTLOCX = 7;

        private HUDStorage storage = HUDStorage.Instance;
        private SlotManagement slotManager = SlotManagement.Instance;
        private HeartManagement heartManager = HeartManagement.Instance;
        private static readonly HUDInitializer instance = new HUDInitializer();
        public static HUDInitializer Instance { get => instance; }
        public HUDInitializer() { }

        public void InitializeHUD(Point drawSize)
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

        

    }
}
