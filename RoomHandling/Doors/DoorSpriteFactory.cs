using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    class DoorSpriteFactory
    {

        private Texture2D door1;
        private Texture2D door6;
        private Texture2D door8;
        private Texture2D door9;
        private Texture2D door13;
        private Texture2D door14;
        private Texture2D door16;

        private Dictionary<int, Texture2D> textures = new Dictionary<int, Texture2D>();

        public static DoorSpriteFactory Instance { get; } = new DoorSpriteFactory();


        private DoorSpriteFactory()
        {
            
        }

        public void LoadAllTextures(Game1 game)
        {

            textures.Add(1,game.Content.Load<Texture2D>("Door1"));
            textures.Add(6,game.Content.Load<Texture2D>("Door6"));
            textures.Add(8, game.Content.Load<Texture2D>("Door8"));
            textures.Add(9, game.Content.Load<Texture2D>("Door9"));
            textures.Add(13, game.Content.Load<Texture2D>("Door13"));
            textures.Add(14, game.Content.Load<Texture2D>("Door14"));
            textures.Add(16, game.Content.Load<Texture2D>("Door16"));

        }

        public DoorSprite CreateDoor(int num)
        {
            return new DoorSprite(textures[num]);
        }

        public ISprite CreateDoor6()
        {
            return new DoorSprite(door6);
        }
        public ISprite CreateDoor8()
        {
            return new DoorSprite(door8);
        }
        public ISprite CreateDoor9()
        {
            return new DoorSprite(door9);
        }
        public ISprite CreateDoor13()
        {
            return new DoorSprite(door13);

        }
        public ISprite CreateDoor14()
        {
            return new DoorSprite(door14);
        }
        public ISprite CreateDoor16()
        {
            return new DoorSprite(door16);
        }


    }
}
