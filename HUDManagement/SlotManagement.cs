using Microsoft.Xna.Framework;
using Sprint5.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.HUDManagement
{
    public class SlotManagement
    {
        private const string TOP = "Top";
        private const string BOTTOM = "Bottom";
        private const string direction = "Up";

        private HUDStorage storage = HUDStorage.Instance;
        private static readonly SlotManagement instance = new SlotManagement();
        public static SlotManagement Instance { get => instance; }
        public SlotManagement() { }

        public void InitializeSlotItems()
        {
            storage.ASlot = LinkInventory.PrimaryItem.WoodenSword;
            storage.ASlotItems.Add(storage.ASlot, CreateAItem(storage.ASlot, TOP));
            storage.ASlotItemsBottom.Add(storage.ASlot, CreateAItem(storage.ASlot, BOTTOM));

            storage.BSlot = LinkInventory.SecondaryItem.Arrow;
            storage.BSlotItems.Add(storage.BSlot, CreateBItem(storage.BSlot, TOP));
            storage.BSlotItemsBottom.Add(storage.BSlot, CreateBItem(storage.BSlot, BOTTOM));
        }

        public void SetBSlotItem(LinkInventory.SecondaryItem item)
        {
            if (!storage.BSlotItems.ContainsKey(item))
                storage.BSlotItems.Add(item, CreateBItem(item, TOP));
            if (!storage.BSlotItemsBottom.ContainsKey(item))
                storage.BSlotItemsBottom.Add(item, CreateBItem(item, BOTTOM));
            storage.BSlot = item;
        }

        private IItems CreateBItem(LinkInventory.SecondaryItem item, string place)
        {
            Vector2 loc;
            if (place is TOP) loc = storage.BSlotLocation; 
                else loc = storage.BSlotBottomLocation; 

            switch (item)
            {
                case LinkInventory.SecondaryItem.Candle:
                    return new BlueCandle(ItemsFactory.Instance.CreateBlueCandleSprite(), loc);

                case LinkInventory.SecondaryItem.Arrow:
                    return new ArrowObject(ItemsFactory.Instance.CreateArrowSprite(direction), loc);

                case LinkInventory.SecondaryItem.Bomb:
                    return new BombObject(ItemsFactory.Instance.CreateBombSprite(), loc);

                case LinkInventory.SecondaryItem.Boomerang:
                    return new BoomerangObject(ItemsFactory.Instance.CreateBoomerangSprite(), loc);

                case LinkInventory.SecondaryItem.Bow:
                    return new Bow(ItemsFactory.Instance.CreateBowSprite(), loc);

                default: //potion
                    return new BluePotion(ItemsFactory.Instance.CreateBluePotionSprite(), loc);
            }
        }

        public void SetASlotItem(LinkInventory.PrimaryItem item)
        {
            if (!storage.ASlotItems.ContainsKey(item)) 
                storage.ASlotItems.Add(item, CreateAItem(item, TOP));
            if (!storage.ASlotItemsBottom.ContainsKey(item)) 
                storage.ASlotItemsBottom.Add(item, CreateAItem(item, BOTTOM));
            storage.ASlot = item;
        }

        private IItems CreateAItem(LinkInventory.PrimaryItem item, string place)
        {
            Vector2 loc;
            if (place is TOP) loc = storage.ASlotLocation;
                else loc = storage.ASlotBottomLocation;

            switch (item)
            {
                case LinkInventory.PrimaryItem.WoodenSword:
                    return new WoodenSword(ItemsFactory.Instance.CreateWoodenSwordSprite(), loc);

                case LinkInventory.PrimaryItem.SilverSword:
                    return new SilverSword(ItemsFactory.Instance.CreateSilverSwordSprite(), loc);

                default: //wand
                    return new Wand(ItemsFactory.Instance.CreateWandSprite(), loc);
            }
        }

        public void Reset()
        {
            storage.ASlotItems.Clear();
            storage.BSlotItems.Clear();
            storage.ASlotItemsBottom.Clear();
            storage.BSlotItemsBottom.Clear();
            InitializeSlotItems();
        }


    }
}
