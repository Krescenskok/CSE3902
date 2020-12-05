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
        public StateId Id { get; set; }

        public DifficultyLevel Difficulty { get; set; }

        public IGameStates Current { get; set; }

        public IGameStates  Previous { get; set; }

        public GameState(Game1 game, DifficultyLevel difficulty, IGameStates initialState)
        {
            Game = game;
            Id = initialState.Id;
            Current = initialState;


        }

        public void Swap(IGameStates swapTo)
        {
            System.Diagnostics.Debug.WriteLine(this.Current.Id);
            this.Current = swapTo;
            Id = swapTo.Id;
            System.Diagnostics.Debug.WriteLine(this.Current.Id);
        }

        public void Update(GameTime gameTime)
        {

            if (Game.LinkPlayer.Health == 0 && !(Game.LinkPlayer.IsDead))
            {
                Game.LinkPlayer.IsDead = true;
                Game.State.Id = StateId.GameOver;
            }
            
            
                foreach (var cont in Game.Controllers)
                {
                    cont.HandleInput(Game);

                    if (Game.ActiveCommand != null)
                    {
                        break;
                    }

                }
            
           
            if (Id == StateId.Gameplay)
            {
                GameplayState.Instance.Update(Game, gameTime);
                Current = GameplayState.Instance;
            }
            else if (Id == StateId.Pause)
            {
                PauseState.Instance.Update(Game, gameTime);
                Current = PauseState.Instance;
            }
            else if (Id == StateId.Inventory)
            {
                InventoryState.Instance.Update(Game, gameTime);
                Current = InventoryState.Instance;
            }
            else if (Id == StateId.GameOver)
            {
                GameOverState.Instance.Update(Game, gameTime);
                Current = GameOverState.Instance;
            }
            else if (Id == StateId.MainMenu)
            {
                MainMenuState.Instance.Update(Game, gameTime);
                Current = MainMenuState.Instance;
            }
            else if (Id == StateId.Transition)
            {
                TransitionState.Instance.Update(Game, gameTime);
                Current = TransitionState.Instance;
            }
            else if (Id == StateId.Credits)
            {
                CreditsState.Instance.Update(Game, gameTime);
                Current = CreditsState.Instance;
            }
            else if (Id == StateId.Stats)
            {
                StatsState.Instance.Update(Game, gameTime);
                Current = StatsState.Instance;
            }
            else if (Id == StateId.Win)
            {
                WinState.Instance.Update(Game, gameTime);
                Current = WinState.Instance;
            }
            else if (Id == StateId.Sound)
            {
                SoundState.Instance.Update(Game, gameTime);
                Current = SoundState.Instance;
            }
            else if (Id == StateId.Options)
            {
                OptionsState.Instance.Update(Game, gameTime);
                Current = OptionsState.Instance;
            }
            else if (Id == StateId.Controls)
            {
                ControlsState.Instance.Update(Game, gameTime);
                Current = ControlsState.Instance;
            }

        }

        public void Draw(SpriteFont font, GameTime gameTime)
        {
            if (Id == StateId.Gameplay)
            {
                GameplayState.Instance.Draw(font, Game, gameTime);
            } else if (Id == StateId.Pause)
            {
                PauseState.Instance.Draw(font, Game, gameTime);
            } else if (Id == StateId.Inventory)
            {
                InventoryState.Instance.Draw(font, Game, gameTime);
            } else if (Id == StateId.GameOver)
            {
                GameOverState.Instance.Draw(font, Game, gameTime);
            } else if (Id == StateId.MainMenu)
            {
                MainMenuState.Instance.Draw(font, Game, gameTime);
            }
            else if (Id == StateId.Transition)
            {
                TransitionState.Instance.Draw(font, Game, gameTime);
            }
            else if (Id == StateId.Credits)
            {
                CreditsState.Instance.Draw(font, Game, gameTime);
            }
            else if (Id == StateId.Stats)
            {
                StatsState.Instance.Draw(font, Game, gameTime);
            }
            else if (Id == StateId.Win)
            {
                WinState.Instance.Draw(font, Game, gameTime);
            }
            else if (Id == StateId.Sound)
            {
                SoundState.Instance.Draw(font, Game, gameTime);
                Current = SoundState.Instance;
            }
            else if (Id == StateId.Options)
            {
                OptionsState.Instance.Draw(font, Game, gameTime);
                Current = OptionsState.Instance;
            }
            else if (Id == StateId.Controls)
            {
                ControlsState.Instance.Draw(font, Game, gameTime);
                Current = ControlsState.Instance;
            }
        }
    }
}
