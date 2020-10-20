using Microsoft.Xna.Framework;
using Sprint3.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.RoomHandling
{
    class EnemyStatus : IStatus
    {
        Boolean IsActive;
        ISprite Enemy;
        int Health;
        Vector2 Location;


        public EnemyStatus(ISprite enemy)
        {
            IsActive = true;
            Enemy = enemy;
        }

        public void setInactive()
        {
            IsActive = false;
        }

        public void setActive()
        {
            IsActive = true;
        }
        public void modifyHealth(int healthModifier)
        {
            Health += healthModifier;
        }
        public void modifyLocation(Vector2 locationUpdate)
        {
            Location = locationUpdate;
        }
    }
}
