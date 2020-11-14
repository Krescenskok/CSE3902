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
        List<ISprite> currentMapLocationSprites;
        List<ISprite> individualRoomSprites;
        Dictionary<int,Boolean> visitedRoom;
        private ISprite bossLocation;
        private int linkRoom;
        private Vector2 mapName;
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

        public void LoadHUDMap(Game1 game)
        {
            mapIndivRoomsTexture = game.Content.Load<Texture2D>("soloRooms");
            mapCurrRoomTexture = game.Content.Load<Texture2D>("SingularLocations");
            mapMarkerTexture = game.Content.Load<Texture2D>("MapIndicator");

            Point mapSize = new Point(game.Window.ClientBounds.Width / FORTH, game.Window.ClientBounds.Height / (TWO*THREE));
            Point mapLocation = new Point(game.Window.ClientBounds.Width / (FORTH * FORTH), game.Window.ClientBounds.Height / (FORTH*TWO));
            InitializeMap(mapSize, mapLocation);

            Point bossMarkerLocation = new Point(mapLocation.X * FORTH, mapLocation.Y + (2*THREE*THREE));
            bossLocation = new HUDMapMarkerSprite(mapMarkerTexture, bossMarkerLocation);

            mapName = new Vector2(mapLocation.X + ROOM_BUFFER, mapLocation.Y - ROOM_BUFFER);
        }

        private void InitializeMap(Point size, Point mapLoc)
        {
            currentMapLocationSprites = new List<ISprite>();
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 0, 0, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 0, 1, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 0, 2, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 0, 3, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 0, 4, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 1, 0, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 1, 1, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 1, 4, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 1, 2, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 1, 3, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 2, 0, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 2, 1, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 2, 2, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 2, 3, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 2, 4, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 3, 0, size, mapLoc));
            currentMapLocationSprites.Add(new HUDMapSprite(mapCurrRoomTexture, 3, 1, size, mapLoc));

            individualRoomSprites = new List<ISprite>();
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 0, 0, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 0, 1, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 0, 2, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 0, 3, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 0, 4, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 1, 0, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 1, 1, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 1, 4, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 1, 2, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 1, 3, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 2, 0, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 2, 1, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 2, 2, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 2, 3, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 2, 4, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 3, 0, size, mapLoc));
            individualRoomSprites.Add(new HUDMapSprite(mapIndivRoomsTexture, 3, 1, size, mapLoc));

            visitedRoom = new Dictionary<int, Boolean>();
            int i;
            for (i=0; i<18; i++)
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

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font, "LEVEL 1", mapName, Color.White);
            GetLinkLocation();
            if (HasMap)
            {
                foreach (HUDMapSprite room in individualRoomSprites)
                {
                    room.Draw(spriteBatch, Vector2.Zero, 0, Color.White);
                }
            }
            else
            {
                int i;
                for (i=0; i< individualRoomSprites.Count; i++)
                { 
                    if (visitedRoom[i])
                    {
                        individualRoomSprites[i].Draw(spriteBatch, Vector2.Zero, 0, Color.White);
                    }
                }
            }
            currentMapLocationSprites[linkRoom].Draw(spriteBatch, Vector2.Zero, 0, Color.White);

            if (HasCompass)
            {
                bossLocation.Draw(spriteBatch, Vector2.Zero, 0, Color.Red);
            }
        }


    }
}
