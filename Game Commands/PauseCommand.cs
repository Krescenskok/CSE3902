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
                if ((game as Game1).State.Current.Id == StateId.Pause)
                {
                    (game as Game1).State.Swap(StateId.Gameplay);
                } else
                {
                    (game as Game1).State.Swap(StateId.Pause);
                }
                Sounds.Instance.TogglePause();
            }
            else
            {
                if ((game as Game1).State.Current.Id == StateId.Pause)
                {
                    (game as Game1).State.Swap(StateId.Gameplay);
                }
                else
                {
                    (game as Game1).State.Swap(StateId.Pause);
                }
                Sounds.Instance.TogglePause();
            }
        }
    }
}
