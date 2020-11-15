using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;

namespace Sprint4
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


        private int unlockTime = 60;
        private int unlockClock = 0;
        private bool unlocked = false;

        private DoorSprite sprite;

        public Door(Game game, Point innerLocation, Point size, int nextRoom, char heading, bool locked, XElement item, Point outerLocation, int currentRoom)
        {
            this.innerLocation = innerLocation;
            this.outerLocation = outerLocation;
            NextRoom = nextRoom;
            CurrentRoom = currentRoom;
            Heading = heading;
            this.game = game;
            Size = size;
            saveInfo = item;
            
            if (locked)
            {
                outerCollider = new LockedDoorCollider(this, outerLocation, size, heading, true);
            } else
            {
                outerCollider = new DoorEntranceCollider(this, outerLocation, size, heading, currentRoom == 6); //special case for room six
            }

            

            innerCollider = new UnlockedDoorCollider(this, innerLocation, size, heading);

            RoomEnemies.Instance.AddTestCollider(new Rectangle(innerLocation, size));

        }

     

        public void ChangeRoom()
        {
            if (Heading == 'L') Camera.Instance.ScrollLeft(NextRoom);
            else if(Heading == 'R') Camera.Instance.ScrollRight(NextRoom);
            else if(Heading == 'T') Camera.Instance.ScrollUp(NextRoom);
            else if (Heading == 'B') Camera.Instance.ScrollDown(NextRoom);

        }

        public void Unlock()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetElementValue("Locked", "false");

            
            outerCollider = new DoorEntranceCollider(this, outerLocation, this.Size, this.Heading, false);
            RoomDoors.Instance.OpenDoor(CurrentRoom);
        }

        public void Lock()
        {
            
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            RoomDoors.Instance.CloseDoor(CurrentRoom);

            saveInfo.SetElementValue("Locked", "true");
            outerCollider = new LockedDoorCollider(this, outerLocation, this.Size, this.Heading, false);
        }

        public void StartUnlock()
        {
            unlocked = true;
            unlockClock = unlockTime;
        }

        public void Update()
        {
            if(unlocked && unlockClock > 0)
            {
                unlockClock--;
            }else if (unlocked)
            {
                Unlock();
            }

            
        }

        public void Draw(SpriteBatch batch)
        {
            if (sprite != null) sprite.Draw(batch);
        }

        void Timer()
        {

        }

    }
}
