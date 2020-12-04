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
                if ((game as Game1).State.Id == IGameStates.Type.Pause)
                {
                    (game as Game1).State.Id = IGameStates.Type.Pause;
                } else
                {
                    (game as Game1).State.Id = IGameStates.Type.Gameplay;
                }
                this.Game.DoorPause = false;
                Player.Paused = !Player.Paused;
                Sounds.Instance.TogglePause();
            }
            else
            {
                this.Game.DoorPause = true;
                if ((game as Game1).State.Id == IGameStates.Type.Pause)
                {
                    (game as Game1).State.Id = IGameStates.Type.Pause;
                }
                else
                {
                    (game as Game1).State.Id = IGameStates.Type.Gameplay;
                }
                Player.Paused = !Player.Paused;
                Sounds.Instance.TogglePause();
            }
        }
    }
}
