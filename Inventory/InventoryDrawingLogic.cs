using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static Sprint5.LinkInventory;

namespace Sprint5.Inventory
{
    public class InventoryDrawingLogic
    {
        private const int CURSORMAX = 8;
        private readonly InventoryItemsStorage itemStorage = InventoryItemsStorage.Instance;
        private static readonly InventoryDrawingLogic instance = new InventoryDrawingLogic();
        public static InventoryDrawingLogic Instance
        {
            get { return instance; }
        }
        public InventoryDrawingLogic() { }

        public void DrawInventory(SpriteBatch spriteBatch)
        {
            InventoryMap.Instance.Draw(spriteBatch);
            DrawItems(spriteBatch);
            itemStorage.CursorLocation[itemStorage.CursorPosition].Draw(spriteBatch, Vector2.Zero, 0, Color.White);
        }

        private void DrawItems(SpriteBatch spriteBatch)
        {
            foreach (KeyValuePair<SecondaryItem, Boolean> pair in itemStorage.SecondInInventory)
            {
                if (pair.Value) itemStorage.SecondItems[pair.Key].Draw(spriteBatch);
            }

            foreach (KeyValuePair<PrimaryItem, Boolean> pair in itemStorage.FirstInInventory)
            {
                if (pair.Value) itemStorage.FirstItems[pair.Key].Draw(spriteBatch);
            }


            if (itemStorage.CursorPosition < CURSORMAX / 2 + 1)
            {
                if (itemStorage.SecondInInventory[itemStorage.SecondSlotItem]) 
                    itemStorage.CurrentSecondItems[itemStorage.SecondSlotItem].Draw(spriteBatch);
            }
            else
            {
                if (itemStorage.FirstInInventory[itemStorage.FirstSlotItem])
                    itemStorage.CurrentFirstItems[itemStorage.FirstSlotItem].Draw(spriteBatch);
            }

        }
    
    }
}
