using System;
using System.Collections.Generic;
using System.Text;
using static Sprint5.LinkInventory;

namespace Sprint5.Inventory
{
    public class CursorManagement
    {
        private const int CURSORMAX = 8;
        private readonly InventoryItemsStorage itemStorage = InventoryItemsStorage.Instance;
        private static readonly CursorManagement instance = new CursorManagement();
        public static CursorManagement Instance
        {
            get { return instance; }
        }
        public CursorManagement() { }

        public void MoveCursor(bool goingRight)
        {
            if (goingRight)
            {
                if (itemStorage.CursorPosition == CURSORMAX - 1)
                {
                    itemStorage.CursorPosition = 0;
                }
                else
                {
                    itemStorage.CursorPosition++;
                }
            }
            else
            {
                if (itemStorage.CursorPosition == 0)
                {
                    itemStorage.CursorPosition = CURSORMAX - 1;
                }
                else
                {
                    itemStorage.CursorPosition--;
                }
            }

            UpdateSlotItem();
        }

        private void UpdateSlotItem()
        {
            switch (itemStorage.CursorPosition)
            {
                case 0:
                    itemStorage.SecondSlotItem = SecondaryItem.Boomerang;
                    break;
                case 1:
                    itemStorage.SecondSlotItem = SecondaryItem.Bomb;
                    break;
                case 2:
                    itemStorage.SecondSlotItem = SecondaryItem.Candle;
                    break;
                case 3:
                    if (itemStorage.SecondInInventory[SecondaryItem.Bow])
                        itemStorage.SecondSlotItem = SecondaryItem.Bow;
                    else
                        itemStorage.SecondSlotItem = SecondaryItem.Arrow;
                    break;
                case 4:
                    itemStorage.SecondSlotItem = SecondaryItem.Potion;
                    break;
                case 5:
                    itemStorage.FirstSlotItem = PrimaryItem.WoodenSword;
                    break;
                case 6:
                    itemStorage.FirstSlotItem = PrimaryItem.SilverSword;
                    break;
                default:
                    itemStorage.FirstSlotItem = PrimaryItem.Wand;
                    break;
            }

            if (itemStorage.SecondInInventory[itemStorage.SecondSlotItem] && itemStorage.SecondSlotItem != SecondaryItem.Potion)
            {
                HUD.Instance.SetBSlotItem(itemStorage.SecondSlotItem);
            }

            if (itemStorage.FirstInInventory[itemStorage.FirstSlotItem])
            {
                HUD.Instance.SetASlotItem(itemStorage.FirstSlotItem);
            }

        }

    }
}
