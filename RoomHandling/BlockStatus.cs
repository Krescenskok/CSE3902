using Microsoft.Xna.Framework;
using Sprint3.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.RoomHandling
{
    class BlockStatus : IStatus
    {
        Boolean IsActive;
        ISprite Block;
        Vector2 Location;


        public BlockStatus(ISprite block)
        {
            IsActive = true;
            Block = block;
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
