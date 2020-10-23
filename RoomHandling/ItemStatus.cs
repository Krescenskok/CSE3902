using Microsoft.Xna.Framework;
using Sprint3.Items;
using System;
using System.Collections.Generic;
using System.Text;


namespace Sprint3.RoomHandling
{
    class ItemStatus : IStatus
    {
        Boolean IsActive;
        ISprite Item;
        Vector2 Location;


        public ItemStatus(ISprite item)
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
        public void modifyLocation(Vector2 locationUpdate)
        {
            Location = locationUpdate;
        }
    }
}
