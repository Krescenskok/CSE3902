using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Sprint5.HUDManagement;
using System.Text;

namespace Sprint5.Inventory
{
    public class InventoryMap
    {
        private Texture2D mapIndivRoomsTexture;
        private Texture2D mapCurrRoomTexture;
        Dictionary<int, ISprite> currentMapLocationSprites;
        Dictionary<int, ISprite> individualRoomSprites;
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

            currentMapLocationSprites = new Dictionary<int, ISprite>();
            for (i=1; i<18; i++)
            {
                currentMapLocationSprites.Add(i, new InventoryMapSprite(mapCurrRoomTexture, i-1, size));
            }

            individualRoomSprites = new Dictionary<int, ISprite> ();
            for (i = 1; i < LASTROOM; i++)
            {
                individualRoomSprites.Add(i, new InventoryMapSprite(mapIndivRoomsTexture, i-1, size));
            }

            visitedRoom = new Dictionary<int, Boolean>();
            for (i = 1; i < LASTROOM; i++)
            {
                visitedRoom.Add(i, false);
            }
            visitedRoom[1] = true;
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
                foreach (KeyValuePair<int, Boolean> pair in visitedRoom)
                {
                    if (pair.Value is true) individualRoomSprites[pair.Key].Draw(spriteBatch, mapLocation, 0, Color.White);
                }
            }
            else
            {
                foreach (KeyValuePair<int, Boolean> pair in visitedRoom)
                {
                    individualRoomSprites[pair.Key].Draw(spriteBatch, mapLocation, 0, Color.White);
                }
            }
            currentMapLocationSprites[linkRoom].Draw(spriteBatch, mapLocation, 0, Color.White);
        }

        public void Reset()
        {
            int i;
            for (i = 0; i < LASTROOM; i++)
            {
                visitedRoom[i] = false;
            }
            visitedRoom[1] = true;
        }

    }
}
