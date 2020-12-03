using System;
using System.Collections.Generic;
using System.Text;
using static Sprint5.LinkInventory;

namespace Sprint5.Inventory
{
    public class InventoryItemsStorage
    {
        private static readonly InventoryItemsStorage instance = new InventoryItemsStorage();
        public static InventoryItemsStorage Instance
        {
            get { return instance; }
        }
        public InventoryItemsStorage() {        }

        private IItems prevItem = null;
        private SecondaryItem secondSlotItem;
        private Dictionary<SecondaryItem, IItems> secondItems = new Dictionary<SecondaryItem, IItems>();
        private Dictionary<SecondaryItem, Boolean> secondInInventory = new Dictionary<SecondaryItem, Boolean>();
        private Dictionary<SecondaryItem, IItems> currentSecondItems = new Dictionary<SecondaryItem, IItems>();

        private PrimaryItem firstSlotItem;
        private Dictionary<PrimaryItem, IItems> firstItems = new Dictionary<PrimaryItem, IItems>();
        private Dictionary<PrimaryItem, Boolean> firstInInventory = new Dictionary<PrimaryItem, Boolean>();
        private Dictionary<PrimaryItem, IItems> currentFirstItems = new Dictionary<PrimaryItem, IItems>();

        private int cursorPosition = 0;
        private Dictionary<int, ISprite> cursorLocation = new Dictionary<int, ISprite>();
        
        public Dictionary<SecondaryItem, IItems> SecondItems { get => secondItems; set => secondItems = value; }
        public Dictionary<SecondaryItem, Boolean> SecondInInventory { get => secondInInventory; set => secondInInventory = value; }
        public Dictionary<SecondaryItem, IItems> CurrentSecondItems { get => currentSecondItems; set => currentSecondItems = value; }
        public Dictionary<PrimaryItem, IItems> FirstItems { get => firstItems; set => firstItems = value; }
        public Dictionary<PrimaryItem, Boolean> FirstInInventory { get => firstInInventory; set => firstInInventory = value; }
        public Dictionary<PrimaryItem, IItems> CurrentFirstItems { get => currentFirstItems; set => currentFirstItems = value; }
        public SecondaryItem SecondSlotItem { get => secondSlotItem; set => secondSlotItem = value; }
        public PrimaryItem FirstSlotItem { get => firstSlotItem; set => firstSlotItem = value; }
        public IItems PreviousItem { get => prevItem; set => prevItem = value; }
        public Dictionary<int, ISprite> CursorLocation { get => cursorLocation; set => cursorLocation = value; }
        public int CursorPosition { get => cursorPosition; set => cursorPosition = value; }
    }
}
