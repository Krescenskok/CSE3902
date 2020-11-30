using Sprint5.Items;
using System;
using System.Collections.Generic;
using System.Text;
using static Sprint5.LinkInventory;

namespace Sprint5.Inventory
{
    public class InventoryItemManagement
    {
        private readonly InventoryItemsStorage itemStorage = InventoryItemsStorage.Instance;
        private static readonly InventoryItemManagement instance = new InventoryItemManagement();        
        public static InventoryItemManagement Instance
        {
            get { return instance; }
        }

        public InventoryItemManagement()
        {

        }

        public void PickUpItem(IItems item, LinkPlayer link)
        {
            if (!(itemStorage.PreviousItem is null) && itemStorage.PreviousItem.Equals(item))
            {
                return;
            }

            if (item is BoomerangObject)
            {
                itemStorage.SecondInInventory[SecondaryItem.Boomerang] = true;
            }
            else if (item is BombObject)
            {
                itemStorage.SecondInInventory[SecondaryItem.Bomb] = true;
            }
            else if (item is Bow)
            {
                itemStorage.SecondInInventory[SecondaryItem.Bow] = true;
            }
            else if (item is BluePotion)
            {
                itemStorage.SecondInInventory[SecondaryItem.Potion] = true;
                LinkInventory.Instance.PotionCount++;
            }
            else if (item is BlueCandle)
            {
                itemStorage.SecondInInventory[SecondaryItem.Candle] = true;
            }
            else if (item is ArrowObject)
            {
                itemStorage.SecondInInventory[SecondaryItem.Arrow] = true;
            }
            else if (item is Rupee)
            {
                LinkInventory.Instance.RupeeCount++;
            }
            else if (item is BombObject)
            {
                LinkInventory.Instance.BombCount++;
            }
            else if (item is Key)
            {
                LinkInventory.Instance.KeyCount++;
            }
            else if (item is Map)
            {
                HUDMap.Instance.HasMap = true;
            }
            else if (item is Compass)
            {
                HUDMap.Instance.HasCompass = true;
            }
            else if (item is Clock)
            {
                link.Clock = true;
                RoomEnemies.Instance.StunAllEnemies();
            }
            else if (item is BlueRing)
            {
                link.UseRing = true;
                LinkInventory.Instance.ShowBlueRing = true;
            }
            itemStorage.PreviousItem = item;
        }

        public void ConsumeItem(LinkPlayer link)
        {
            if (!itemStorage.SecondInInventory[itemStorage.SecondSlotItem])
            {
                return;
            }

            if (itemStorage.SecondSlotItem == SecondaryItem.Potion)
            {
                if (LinkInventory.Instance.PotionCount > 0)
                {
                    link.Health = link.FullHealth;
                    HUD.Instance.UpdateHearts(link);
                    LinkInventory.Instance.PotionCount--;

                    if (LinkInventory.Instance.PotionCount == 0)
                    {
                        itemStorage.SecondInInventory[SecondaryItem.Potion] = false;
                    }
                }

            }
        }

        public void UseItem(IItems item)
        {
            if (item is Key)
            {
                LinkInventory.Instance.KeyCount--;
            }
            if (item is BombObject)
            {
                LinkInventory.Instance.BombCount--;
            }
        }

        public void UpdateLinkWeapons(LinkPlayer link)
        {
            switch (itemStorage.SecondSlotItem)
            {
                case SecondaryItem.Arrow:
                case SecondaryItem.Bow:
                    link.SecondaryWeapon = ItemForLink.ArrowBow;
                    break;
                case SecondaryItem.Boomerang:
                    link.SecondaryWeapon = ItemForLink.Boomerang;
                    break;
                case SecondaryItem.Candle:
                    link.SecondaryWeapon = ItemForLink.BlueCandle;
                    break;
                case SecondaryItem.Bomb:
                    link.SecondaryWeapon = ItemForLink.Bomb;
                    break;
                default: //potion - do nothing
                    break;
            }

            switch (itemStorage.FirstSlotItem)
            {
                case PrimaryItem.WoodenSword:
                    link.CurrentWeapon = ItemForLink.WoodenSword;
                    break;
                case PrimaryItem.SilverSword:
                    link.CurrentWeapon = ItemForLink.Sword;
                    break;
                default: //wand
                    link.CurrentWeapon = ItemForLink.MagicalRod;
                    break;
            }
        }

    }
}
