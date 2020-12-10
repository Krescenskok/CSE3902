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
        private string roomKey;
        private Direction Direction;

        private XElement saveInfo;
        private Point Size;


        private int unlockTime = 20;
        private int unlockClock = 0;
        private bool unlocked = false, unlocking = false;

        public bool open;

        private DoorSprite sprite;

        public Door(Point innerLocation, Point size, int nextRoom, DoorType type, XElement item, Point outerLocation, string key, DoorSprite sprite)
        {

            this.outerLocation = outerLocation;
            NextRoom = nextRoom;
            roomKey = key;
            Direction = Directions.Parse(key);
            Size = size;
            saveInfo = item;
            this.sprite = sprite;
            
            if (type == DoorType.locked)
            {
                outerCollider = new LockedDoorCollider(this, outerLocation, size);
                open = false;
            } else if(type == DoorType.normal || type == DoorType.open)
            {
                outerCollider = new DoorEntranceCollider(this, outerLocation, size);
                open = true;


            }else if (type == DoorType.closed)
            {
                outerCollider = new SpecialDoorCollider(this, outerLocation, size);
                open = false;
            }

            if(key.Contains("secret"))
                innerCollider = new UnlockedDoorCollider(this, innerLocation, size,'S');
            else if (key.Contains("center"))
                innerCollider = new UnlockedDoorCollider(this, innerLocation, size, 'C');
            else
                innerCollider = new UnlockedDoorCollider(this, innerLocation, size, Direction);

        }

     

        public void ChangeRoom()
        {
            StatsScreen.Instance.RoomsEntered++;
            if (Direction == Direction.left) Camera.Instance.Scroll(NextRoom, "left");
            else if (Direction == Direction.right) Camera.Instance.Scroll(NextRoom, "right");
            else if (Direction == Direction.top) Camera.Instance.Scroll(NextRoom, "up");
            else if (Direction == Direction.none) Camera.Instance.Transition(NextRoom);
            else if (Direction == Direction.bottom) Camera.Instance.Scroll(NextRoom, "down");

        }

        //for doors that open on special conditions
        public void Open()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetAttributeValue("Type", "open" + Direction.ToString());
            outerCollider = new DoorEntranceCollider(this, outerLocation, Size);
            open = true;
            Sounds.Instance.Play("DoorUnlock");
            sprite.shouldDraw = false;
        }

        public void Close()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetAttributeValue("Type", "closed" + Direction.ToString());
            outerCollider = new SpecialDoorCollider(this, outerLocation, Size);
            open = false;
            Sounds.Instance.Play("DoorUnlock");

            sprite.shouldDraw = true;
        }

        //doors that open with a key
        public void Unlock()
        {
            CollisionHandler.Instance.RemoveCollider(outerCollider);
            saveInfo.SetAttributeValue("Type", "normal" + Direction.ToString());

            
            outerCollider = new DoorEntranceCollider(this, outerLocation, this.Size);
            Sounds.Instance.Play("DoorUnlock");

            sprite.shouldDraw = false;
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
