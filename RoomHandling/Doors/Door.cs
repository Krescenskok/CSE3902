using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
{
    public class Door: IDoors
    {
        private Point Location;
        public IDoorCollider collider;
        private int doorDimensionX = 0;
        private int doorDimensionY = 0;
        private Vector2 Coordinates;
        private int RoomNumber;
        private char Heading;
        private Game game;
        private XElement saveInfo;
        private Point Size;


        public Door(Game game, Point location, Point size, int roomNumber, char heading, bool locked, XElement item)
        {
            this.Location = location;
            this.RoomNumber = roomNumber;
            this.Heading = heading;
            this.game = game;
            this.Size = size;
            this.saveInfo = item;

            //calculate coordinates

            Rectangle locationRect = new Rectangle((int)location.X, (int)location.Y, doorDimensionX, doorDimensionY);
            if (locked)
            {
                collider = new LockedDoorCollider(this, location, size, heading);
            } else
            {
                collider = new UnlockedDoorCollider(this, location, size, heading);
            }
        }

        public void ChangeRoom()
        {

            RoomSpawner.Instance.RoomChange(this.game, RoomNumber, Heading);
        }

        public void Unlock()
        {
            CollisionHandler.Instance.RemoveCollider(collider);
            saveInfo.SetElementValue("Alive", "true");
            collider = new UnlockedDoorCollider(this, this.Location, this.Size, this.Heading);
        }

    }
}
