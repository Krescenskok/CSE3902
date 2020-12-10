using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class RoomSpriteFactory
    {

        public static RoomSpriteFactory instance {get;} = new RoomSpriteFactory();

        private RoomSpriteFactory() { }

        Texture2D blankRoom;
        Texture2D leftDoor;
        Texture2D rightDoor;
        Texture2D topDoor;
        Texture2D bottomDoor;
        Texture2D blankRoomTop;
        Texture2D leftDoorTop;
        Texture2D rightDoorTop;
        Texture2D topDoorTop;
        Texture2D bottomDoorTop;

        public void LoadTextures(Game1 game)
        {
            blankRoom = game.Content.Load<Texture2D>("Procedural/BlankRoom");
            leftDoor = game.Content.Load<Texture2D>("Procedural/LeftDoor");
            rightDoor = game.Content.Load<Texture2D>("Procedural/RightDoor");
            topDoor = game.Content.Load<Texture2D>("Procedural/TopDoor");
            bottomDoor = game.Content.Load<Texture2D>("Procedural/BottomDoor");


            blankRoomTop = game.Content.Load<Texture2D>("Procedural/BaseRoomTop");
            leftDoorTop = game.Content.Load<Texture2D>("Procedural/LeftDoorTop");
            rightDoorTop = game.Content.Load<Texture2D>("Procedural/RightDoorTop");
            topDoorTop = game.Content.Load<Texture2D>("Procedural/TopDoorTop");
            bottomDoorTop = game.Content.Load<Texture2D>("Procedural/BottomDoorTop");

        }

        public RoomSprite NewRoom(int row, int col)
        {
            return new RoomSprite(new List<Texture2D> { blankRoom },row,col);
        }

        public RoomSprite NewRoomTop(int row, int col)
        {
            return new RoomSprite(new List<Texture2D> { blankRoomTop }, row, col);
        }

        public void AddLeftDoor(ref RoomSprite sprite)
        {
            sprite.AddTexture(leftDoor);
        }

        public void AddRightDoor(ref RoomSprite sprite)
        {
            sprite.AddTexture(rightDoor);
        }

        public void AddTopDoor(ref RoomSprite sprite)
        {
            sprite.AddTexture(topDoor);
        }

        public void AddBottomDoor(ref RoomSprite sprite)
        {
            sprite.AddTexture(bottomDoor);
        }

        public void AddLeftDoorTop(ref RoomSprite sprite)
        {
            sprite.AddTexture(leftDoorTop);
        }

        public void AddRightDoorTop(ref RoomSprite sprite)
        {
            sprite.AddTexture(rightDoorTop);
        }

        public void AddTopDoorTop(ref RoomSprite sprite)
        {
            sprite.AddTexture(topDoorTop);
        }

        public void AddBottomDoorTop(ref RoomSprite sprite)
        {
            sprite.AddTexture(bottomDoorTop);
        }
    }
}
