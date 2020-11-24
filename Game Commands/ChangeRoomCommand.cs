using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
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
            Camera.Instance.MoveToRoom(roomNumber);
           
           
        }
    }
}
