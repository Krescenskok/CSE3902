﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static Sprint5.LinkInventory;

namespace Sprint5.HUDManagement
{
    public class HUDStorage
    {
        private static readonly HUDStorage instance = new HUDStorage();
        public static HUDStorage Instance {get => instance;}
        public HUDStorage() { }

        private PrimaryItem aSlot;
        public PrimaryItem ASlot { get => aSlot; set => aSlot = value; }

        private SecondaryItem bSlot;
        public SecondaryItem BSlot { get => bSlot; set => bSlot = value; }

        private Vector2 bSlotLocation;
        public Vector2 BSlotLocation { get => bSlotLocation; set => bSlotLocation = value; }

        private Vector2 aSlotLocation;
        public Vector2 ASlotLocation { get => aSlotLocation; set => aSlotLocation = value; }

        private Vector2 rupeeCountLocation;
        public Vector2 RupeeCountLocation { get => rupeeCountLocation; set => rupeeCountLocation = value; }

        private Vector2 keyCountLocation;
        public Vector2 KeyCountLocation { get => keyCountLocation; set => keyCountLocation = value; }

        private Vector2 bombCountLocation;
        public Vector2 BombCountLocation { get => bombCountLocation; set => bombCountLocation = value; }

        private Vector2 bSlotBottomLocation;
        public Vector2 BSlotBottomLocation { get => bSlotBottomLocation; set => bSlotBottomLocation = value; }

        private Vector2 aSlotBottomLocation;
        public Vector2 ASlotBottomLocation { get => aSlotBottomLocation; set => aSlotBottomLocation = value; }

        private Vector2 rupeeCountBottomLocation;
        public Vector2 RupeeCountBottomLocation { get => rupeeCountBottomLocation; set => rupeeCountBottomLocation = value; }

        private Vector2 keyCountBottomLocation;
        public Vector2 KeyCountBottomLocation { get => keyCountBottomLocation; set => keyCountBottomLocation = value; }

        private Vector2 bombCountBottomLocation;
        public Vector2 BombCountBottomLocation { get => bombCountBottomLocation; set => bombCountBottomLocation = value; }

        private Vector2 bottomCorner;
        public Vector2 BottomCorner { get => bottomCorner; set => bottomCorner = value; }

        private Dictionary<SecondaryItem, IItems> bSlotItems = new Dictionary<LinkInventory.SecondaryItem, IItems>();
        public Dictionary<SecondaryItem, IItems> BSlotItems { get => bSlotItems; set => bSlotItems = value; }

        private Dictionary<SecondaryItem, IItems> bSlotItemsBottom = new Dictionary<LinkInventory.SecondaryItem, IItems>();
        public Dictionary<SecondaryItem, IItems> BSlotItemsBottom { get => bSlotItemsBottom; set => bSlotItemsBottom = value; }

        private Dictionary<PrimaryItem, IItems> aSlotItems = new Dictionary<LinkInventory.PrimaryItem, IItems>();
        public Dictionary<PrimaryItem, IItems> ASlotItems { get => aSlotItems; set => aSlotItems = value; }

        private Dictionary<PrimaryItem, IItems> aSlotItemsBottom = new Dictionary<LinkInventory.PrimaryItem, IItems>();
        public Dictionary<PrimaryItem, IItems> ASlotItemsBottom { get => aSlotItemsBottom; set => aSlotItemsBottom = value; }

        private Dictionary<String, IItems> hearts = new Dictionary<String, IItems>();
        public Dictionary<String, IItems> Hearts { get => hearts; set => hearts = value; }

        private List<IItems> drawnHearts = new List<IItems>();
        public List<IItems> DrawnHearts { get => drawnHearts; set => drawnHearts = value; }

        private List<IItems> drawnHeartsBottom = new List<IItems>();
        public List<IItems> DrawnHeartsBottom { get => drawnHeartsBottom; set => drawnHeartsBottom = value; }

        private Vector2 firstHeartLoc;
        public Vector2 FirstHeartLoc { get => firstHeartLoc; set => firstHeartLoc = value; }

        private Vector2 firstBottomHeartLoc;
        public Vector2 FirstBottomHeartLoc { get => firstBottomHeartLoc; set => firstBottomHeartLoc = value; }

        private int maxHearts = 3;
        public int MaxHearts { get => maxHearts; set => maxHearts = value; }

        private int prevHealth = -1;
        public int PreviousHealth { get => prevHealth; set => prevHealth = value; }

        private int bottomAdjust;
        public int BottomAdjust { get => bottomAdjust; set => bottomAdjust = value; }
    }
}
