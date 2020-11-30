using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    public class PauseCommand : ICommand

    {

        bool isPause = false;

        public bool IsPause { get => isPause; set => isPause = value; }

        public PauseCommand(Game1 game, String doorOrScreen)
        {
            if (doorOrScreen == "NotDoor")
            {
                game.DoorPause = false;
            } else
            {
                game.DoorPause = true;
            }
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {

        }
    }
}
