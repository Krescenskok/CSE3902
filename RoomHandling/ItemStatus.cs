using Sprint3.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.RoomHandling
{
    class itemStatus : IStatus
    {
        Boolean IsActive;
        ItemsSprite Item;


        public itemStatus(ItemsSprite item)
        {
            IsActive = true;
            Item = item;
        }

        public void setInactive()
        {
            IsActive = false;
        }

        public void setActive()
        {
            IsActive = true;
        }
    }
}
