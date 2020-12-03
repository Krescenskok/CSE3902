using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint5
{
    public class PauseCommand : ICommand

    {
        private String DoorPause;
        private bool isPause = false;
        private LinkPlayer Player;

        private Game1 Game;

        public PauseCommand(Game1 game, LinkPlayer player, String doorOrScreen)
        {
            this.DoorPause = doorOrScreen;
            this.Game = game;
            this.Player = player;
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            if (DoorPause == "NotDoor")
            {
                this.Game.DoorPause = false;
                this.Game.IsPaused = !Game.IsPaused;
                Player.Paused = !Player.Paused;
                Sounds.Instance.TogglePause();
            }
            else
            {
                this.Game.DoorPause = true;
                this.Game.IsPaused = !Game.IsPaused;
                Player.Paused = !Player.Paused;
                Sounds.Instance.TogglePause();
            }
        }
    }
}
