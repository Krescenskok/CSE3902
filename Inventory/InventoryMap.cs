using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4.Inventory
{
    public class InventoryMap
    {
        private Texture2D mapIndivRoomsTexture;
        private Texture2D mapCurrRoomTexture;
        List<ISprite> currentMapLocationSprites;
        List<ISprite> individualRoomSprites;
        Dictionary<int, Boolean> visitedRoom;
        private int linkRoom;
        Vector2 mapLocation;


        private const int FIVE = 5;
        private const int LASTROOM = 18;
        private const int SIZE = 48;
        private const int ADJUST_X = 20;
        private const int ADJUST_Y = 10;

        private static readonly InventoryMap instance = new InventoryMap();

        public static InventoryMap Instance
        {
            get { return instance; }
        }

        public InventoryMap()
        {

        }

        public void LoadInventoryMap(Game game, Point InventorySize)
        {
            mapIndivRoomsTexture = game.Content.Load<Texture2D>("HUDandInv/InvMapRooms");
            mapCurrRoomTexture = game.Content.Load<Texture2D>("HUDandInv/InvMapRoomsCurrent");

            Point mapSize = new Point(InventorySize.X / FIVE, InventorySize.Y / FIVE);
            InitializeMap(mapSize);

            mapLocation = new Vector2(InventorySize.X / 2 + ADJUST_X, InventorySize.Y / 2 - ADJUST_Y);


        }

        private void InitializeMap(Point size)
        {
            int i;

            currentMapLocationSprites = new List<ISprite>();
            for (i=0; i<18; i++)
            {
                currentMapLocationSprites.Add(new InventoryMapSprite(mapCurrRoomTexture, i, size));
            }

            individualRoomSprites = new List<ISprite>();
            for (i = 0; i < LASTROOM; i++)
            {
                individualRoomSprites.Add(new InventoryMapSprite(mapIndivRoomsTexture, i, size));
            }

            visitedRoom = new Dictionary<int, Boolean>();
            for (i = 0; i < LASTROOM; i++)
            {
                visitedRoom.Add(i, false);
            }
        }

        public void GetLinkLocation()
        {
            linkRoom = RoomSpawner.Instance.CurrentRoom;
            if (linkRoom >= LASTROOM)
            {
                linkRoom = LASTROOM - 1;
            }
            if (!visitedRoom[linkRoom])
            {
                visitedRoom[linkRoom] = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GetLinkLocation();

            if (!HUDMap.Instance.HasMap)
            {
                int i;
                for (i = 0; i < individualRoomSprites.Count; i++)
                {
                    if (visitedRoom[i])
                    {
                        individualRoomSprites[i].Draw(spriteBatch, mapLocation, 0, Color.White);
                    }
                }
            }

            currentMapLocationSprites[linkRoom].Draw(spriteBatch, mapLocation, 0, Color.White);

        }


        public void Reset()
        {
            visitedRoom.Clear();
            int i;
            for (i = 0; i < LASTROOM; i++)
            {
                visitedRoom.Add(i, false);
            }
        }



    }
}
