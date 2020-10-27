using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint3
{
    public class ChangeRoomCommand : ICommand
    {
        int roomNumber;

        public ChangeRoomCommand(int roomNumber)
        {
            this.roomNumber = roomNumber;
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            RoomSpawner.Instance.RoomChange(game, roomNumber);
        }
    }
}
