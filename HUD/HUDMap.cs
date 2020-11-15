using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace Sprint4
{
    public class HUDMap
    {
        private Texture2D mapIndivRoomsTexture;
        private Texture2D mapCurrRoomTexture;
        private Texture2D mapMarkerTexture;
        Dictionary<int, ISprite> currentMapLocationSprites;
        Dictionary<int, ISprite> individualRoomSprites;
        Dictionary<int,Boolean> visitedRoom;
        private Point mapTopLoc;
        private Point mapBottomLoc;
        Point bossMarkerLocation;
        Point bossMarkerLocationBottom;
        private ISprite bossLocation;
        private int linkRoom;
        private bool drawMap = false;
        private bool drawBossLocation = false;
        private int ROOM_BUFFER = 40;
        private const int FORTH = 4;
        private const int TWO = 2;
        private const int THREE = 3;
        private const int LASTROOM = 18;


        public bool HasMap
        {
            get { return drawMap; }
            set { drawMap = value; }
        }

        public bool HasCompass
        {
            get { return drawBossLocation; }
            set { drawBossLocation = value; }
        }


        private static readonly HUDMap instance = new HUDMap();

        public static HUDMap Instance
        {
            get { return instance; }
        }

        public HUDMap()
        {

        }

        public void LoadHUDMap(Game1 game, Point HUDSize)
        {
            mapIndivRoomsTexture = game.Content.Load<Texture2D>("HUDandInv/soloRooms");
            mapCurrRoomTexture = game.Content.Load<Texture2D>("HUDandInv/SingularLocations");
            mapMarkerTexture = game.Content.Load<Texture2D>("HUDandInv/MapIndicator");

            Point mapSize = new Point(HUDSize.X / FORTH, HUDSize.Y / 2);
            mapTopLoc = new Point(HUDSize.X / (FORTH * FORTH), game.Window.ClientBounds.Height / (FORTH*TWO));
            mapBottomLoc = new Point(mapTopLoc.X, mapTopLoc.Y + game.Window.ClientBounds.Height - HUDSize.Y);
            InitializeMap(mapSize);

            bossMarkerLocation = new Point(mapTopLoc.X * FORTH, mapTopLoc.Y + (2*THREE*THREE));
            bossMarkerLocationBottom = new Point(bossMarkerLocation.X, bossMarkerLocation.Y + game.Window.ClientBounds.Height - HUDSize.Y);
            bossLocation = new HUDMapMarkerSprite(mapMarkerTexture);

        }

        private void InitializeMap(Point size)
        {
            currentMapLocationSprites = new Dictionary<int, ISprite>();
            currentMapLocationSprites.Add(1, new HUDMapSprite(mapCurrRoomTexture, 0, 0, size));
            currentMapLocationSprites.Add(2, new HUDMapSprite(mapCurrRoomTexture, 0, 1, size));
            currentMapLocationSprites.Add(3, new HUDMapSprite(mapCurrRoomTexture, 0, 2, size));
            currentMapLocationSprites.Add(4, new HUDMapSprite(mapCurrRoomTexture, 0, 3, size));
            currentMapLocationSprites.Add(5, new HUDMapSprite(mapCurrRoomTexture, 0, 4, size));
            currentMapLocationSprites.Add(6, new HUDMapSprite(mapCurrRoomTexture, 1, 0, size));
            currentMapLocationSprites.Add(7, new HUDMapSprite(mapCurrRoomTexture, 1, 1, size));
            currentMapLocationSprites.Add(8, new HUDMapSprite(mapCurrRoomTexture, 1, 2, size));
            currentMapLocationSprites.Add(9, new HUDMapSprite(mapCurrRoomTexture, 1, 3, size));
            currentMapLocationSprites.Add(10, new HUDMapSprite(mapCurrRoomTexture, 1, 4, size));
            currentMapLocationSprites.Add(11, new HUDMapSprite(mapCurrRoomTexture, 2, 0, size));
            currentMapLocationSprites.Add(12, new HUDMapSprite(mapCurrRoomTexture, 2, 1, size));
            currentMapLocationSprites.Add(13, new HUDMapSprite(mapCurrRoomTexture, 2, 4, size));
            currentMapLocationSprites.Add(14, new HUDMapSprite(mapCurrRoomTexture, 2, 2, size));
            currentMapLocationSprites.Add(15, new HUDMapSprite(mapCurrRoomTexture, 2, 3, size));
            currentMapLocationSprites.Add(16, new HUDMapSprite(mapCurrRoomTexture, 3, 0, size));
            currentMapLocationSprites.Add(17, new HUDMapSprite(mapCurrRoomTexture, 3, 1, size));

            individualRoomSprites = new Dictionary<int, ISprite>();
            individualRoomSprites.Add(1, new HUDMapSprite(mapIndivRoomsTexture, 0, 0, size));
            individualRoomSprites.Add(2, new HUDMapSprite(mapIndivRoomsTexture, 0, 1, size));
            individualRoomSprites.Add(3, new HUDMapSprite(mapIndivRoomsTexture, 0, 2, size));
            individualRoomSprites.Add(4, new HUDMapSprite(mapIndivRoomsTexture, 0, 3, size));
            individualRoomSprites.Add(5, new HUDMapSprite(mapIndivRoomsTexture, 0, 4, size));
            individualRoomSprites.Add(6, new HUDMapSprite(mapIndivRoomsTexture, 1, 0, size));
            individualRoomSprites.Add(7, new HUDMapSprite(mapIndivRoomsTexture, 1, 1, size));
            individualRoomSprites.Add(8, new HUDMapSprite(mapIndivRoomsTexture, 1, 2, size));
            individualRoomSprites.Add(9, new HUDMapSprite(mapIndivRoomsTexture, 1, 3, size));
            individualRoomSprites.Add(10, new HUDMapSprite(mapIndivRoomsTexture, 1, 4, size));
            individualRoomSprites.Add(11, new HUDMapSprite(mapIndivRoomsTexture, 2, 0, size));
            individualRoomSprites.Add(12, new HUDMapSprite(mapIndivRoomsTexture, 2, 1, size));
            individualRoomSprites.Add(13, new HUDMapSprite(mapIndivRoomsTexture, 2, 4, size));
            individualRoomSprites.Add(14, new HUDMapSprite(mapIndivRoomsTexture, 2, 2, size));
            individualRoomSprites.Add(15, new HUDMapSprite(mapIndivRoomsTexture, 2, 3, size));
            individualRoomSprites.Add(16, new HUDMapSprite(mapIndivRoomsTexture, 3, 0, size));
            individualRoomSprites.Add(17, new HUDMapSprite(mapIndivRoomsTexture, 3, 1, size));

            visitedRoom = new Dictionary<int, Boolean>();
            int i;
            for (i=1; i< LASTROOM; i++)
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

        public void Draw(SpriteBatch spriteBatch, int pos)
        {
            Vector2 loc;
            if (pos == 0)
            {
                loc = new Vector2(mapTopLoc.X, mapTopLoc.Y);
            }
            else
            {
                loc = new Vector2(mapBottomLoc.X, mapBottomLoc.Y);
            }

            GetLinkLocation();
            if (HasMap)
            {
                foreach (HUDMapSprite room in individualRoomSprites.Values)
                {
                    room.Draw(spriteBatch, loc, 0, Color.White);
                }
            }
            else
            {
                foreach (KeyValuePair<int, Boolean> pair in visitedRoom)
                {
                    if (pair.Value)
                    {
                        individualRoomSprites[pair.Key].Draw(spriteBatch, loc, 0, Color.White);
                    }
                }                
            }
            currentMapLocationSprites[linkRoom].Draw(spriteBatch, loc, 0, Color.White);

            if (HasCompass)
            {
                bossLocation.Draw(spriteBatch, loc, 0, Color.Red);
            }
        }

        public void Reset()
        {
            visitedRoom.Clear();
            int i;
            for (i = 0; i < LASTROOM; i++)
            {
                visitedRoom.Add(i, false);
            }

            HasMap = false;
            HasCompass = false;
        }


    }
}
