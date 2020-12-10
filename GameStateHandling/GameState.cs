using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.GameStateHandling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class GameState
    {

        private Game1 Game;
        public DifficultyLevel Difficulty { get; set; }
        public IGameStates Current { get; set; }
        public IGameStates  Previous { get; set; }

        public GameState(Game1 game, DifficultyLevel difficulty, IGameStates initialState)
        {
            Game = game;
            Current = initialState;
            Previous = initialState;
        }

        public void Swap(StateId newState)
        {
            System.Diagnostics.Debug.WriteLine(this.Current.Id);
            Previous = Current;
            IGameStates NewState = IDToState.Instance.State(newState);


            this.Current = NewState;

            System.Diagnostics.Debug.WriteLine(this.Current.Id);
        }

        public void Update(GameTime gameTime)
        {

            if (Game.LinkPlayer.Health == 0 && !(Game.LinkPlayer.IsDead))
            {
                Game.LinkPlayer.IsDead = true;
                Swap(StateId.GameOver);
            }
            
            
                foreach (var cont in Game.Controllers)
                {
                    cont.HandleInput(Game);

                    if (Game.ActiveCommand != null)
                    {
                        break;
                    }

                }
            Current.Update(Game, gameTime);
        }

        public void Draw(SpriteFont font, GameTime gameTime)
        {
            Current.Draw(font, Game, gameTime);
        }
    }
}
