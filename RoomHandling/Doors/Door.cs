using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;


namespace Sprint5
{
    public class Door: IDoors
    {

        private Point outerLocation;
        
        public IDoorCollider innerCollider;
        public IDoorCollider outerCollider;
        private int NextRoom;
        private int CurrentRoom;
        private char Heading;

        private XElement saveInfo;
        private Point Size;


        private int unlockTime = 20;
        private int unlockClock = 0;
        private bool unlocked = false, unlocking = false;

        public bool open;

        public Door(Point innerLocation, Point size, int nextRoom, char heading, DoorType type, XElement item, Point outerLocation, int currentRoom)
        {

            this.outerLocation = outerLocation;
            NextRoom = nextRoom;
            CurrentRoom = currentRoom;
            Heading = heading;
            Size = size;
            saveInfo = item;
            
            if (type == DoorType.locked)
            {
                outerCollider = new LockedDoorCollider(this, outerLocation, size, heading);
                open = false;
            } else if(type == DoorType.normal)
            {
                outerCollider = new DoorEntranceCollider(this, outerLocation, size, heading);
                open = true;

                if(currentRoom == 6) outerCollider = new DoorEntranceCollider(this, outerLocation, size, heading, "sixer");

            } else if(type == DoorType.special_open)
            {
                outerCollider = new SpecialDoorCollider(this,outerLocation,size);
                open = true;
            }else if (type == DoorType.special_closed)
            {
                outerCollider = new SpecialDoorCollider(this, outerLocation, size);
                open = false;
            }

            innerCollider = new UnlockedDoorCollider(this, innerLocation, size, heading);

        }

     

        public void ChangeRoom()
        {
            StatsScreen.Instance.RoomsEntered++;
            if (Heading == 'L') Camera.Instance.Scroll(NextRoom, "left");
            else if (Heading == 'R') Camera.Instance.Scroll(NextRoom, "right");
            else if (Heading == 'T') Camera.Instance.Scroll(NextRoom, "up");
            else if (Heading == 'S' || Heading == 'C') Camera.Instance.Transition(NextRoom);
            else if (Heading == 'B') Camera.Instance.Scroll(NextRoom, "down");

        }

        //for doors that open on special conditions
        public void Open()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Type", "normal");
            outerCollider = new DoorEntranceCollider(this, outerLocation, Size, Heading);
            open = true;
            Sounds.Instance.Play("DoorUnlock");
        }

        public void Close()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Type", "locked");
            outerCollider = new SpecialDoorCollider(this, outerLocation, Size);
            open = false;
            Sounds.Instance.Play("DoorUnlock");
        }

        //doors that open with a key
        public void Unlock()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Type", "normal");

            
            outerCollider = new DoorEntranceCollider(this, outerLocation, this.Size, this.Heading);
            RoomDoors.Instance.ShowDoorSprite(CurrentRoom);
            Sounds.Instance.Play("DoorUnlock");
        }


        public void StartUnlock()
        {

            unlocking = true;
            unlockClock = unlockTime;
   

        }

        public void Update()
        {
            if(unlocking && unlockClock > 0)
            {
                unlockClock--;
            }else if (unlocking && !unlocked)
            {
                Unlock();
                unlocked = true;
            }

            
        }



    }
}
