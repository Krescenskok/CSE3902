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
        private Point innerLocation;
        private Point outerLocation;
        
        public IDoorCollider innerCollider;
        public IDoorCollider outerCollider;
        private int NextRoom;
        private int CurrentRoom;
        private char Heading;
        private Game game;
        private XElement saveInfo;
        private Point Size;


        private int unlockTime = 20;
        private int unlockClock = 0;
        private bool unlocked = false, unlocking = false;

        public bool open;

        public Door(Game game, Point innerLocation, Point size, int nextRoom, char heading, DoorType type, XElement item, Point outerLocation, int currentRoom)
        {
            this.innerLocation = innerLocation;
            this.outerLocation = outerLocation;
            NextRoom = nextRoom;
            CurrentRoom = currentRoom;
            Heading = heading;
            this.game = game;
            Size = size;
            saveInfo = item;
            
            if (type == DoorType.locked)
            {
                outerCollider = new LockedDoorCollider(this, outerLocation, size, heading);
                open = false;
            } else if(type == DoorType.normal)
            {
                outerCollider = new DoorEntranceCollider(this, outerLocation, size, heading, false);
                open = true;

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
            if (Heading == 'L') Camera.Instance.ScrollLeft(NextRoom);
            else if (Heading == 'R') Camera.Instance.ScrollRight(NextRoom);
            else if (Heading == 'C') Camera.Instance.ScrollDown(NextRoom);
            else if (Heading == 'T') Camera.Instance.ScrollUp(NextRoom);
            else if (Heading == 'S') Camera.Instance.ScrollUp(NextRoom);
            else if (Heading == 'B') Camera.Instance.ScrollDown(NextRoom);
            else if (Heading == 'C') Camera.Instance.ScrollDown(NextRoom); //Something Special

        }

        //for doors that open on special conditions
        public void Open()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Type", "normal");
            outerCollider = new DoorEntranceCollider(this, outerLocation, Size, Heading, CurrentRoom == 6);
            open = true;
            Sounds.Instance.PlaySoundEffect("DoorUnlock");
        }

        public void Close()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Type", "locked");
            outerCollider = new SpecialDoorCollider(this, outerLocation, Size);
            open = false;
            Sounds.Instance.PlaySoundEffect("DoorUnlock");
        }

        //doors that open with a key
        public void Unlock()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Type", "normal");

            
            outerCollider = new DoorEntranceCollider(this, outerLocation, this.Size, this.Heading, false);
            RoomDoors.Instance.ShowDoorSprite(CurrentRoom);
            Sounds.Instance.PlaySoundEffect("DoorUnlock");
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
