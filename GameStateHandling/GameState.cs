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

        public GameState(Game1 game, DifficultyLevel difficulty, StateId id)
        {
            Game = game;
            Id = id;
            Difficulty = difficulty;
        }

        public void Update(GameTime gameTime)
        {
            if (Game.LinkPlayer.Health == 0 && !(Game.LinkPlayer.IsDead))
            {
                Game.LinkPlayer.IsDead = true;
                Game.ActiveCommand = new GameOverCommand(Game.LinkPlayer);
                Game.State.Id = StateId.GameOver;
            }
            else
            {
                foreach (var cont in Game.Controllers)
                {
                    cont.HandleInput(Game);

                    if (Game.ActiveCommand != null)
                    {
                        break;
                    }

                }
            }
           
            if (Id == StateId.Gameplay)
            {
                GameplayState.Instance.Update(Game, gameTime);
            }
            else if (Id == StateId.Pause)
            {
                PauseState.Instance.Update(Game, gameTime);
            }
            else if (Id == StateId.Inventory)
            {
                InventoryState.Instance.Update(Game, gameTime);
            }
            else if (Id == StateId.GameOver)
            {
                GameOverState.Instance.Update(Game, gameTime);
            }
            else if (Id == StateId.MainMenu)
            {
                MainMenuState.Instance.Update(Game, gameTime);
            }
            else if (Id == StateId.Transition)
            {
                TransitionState.Instance.Update(Game, gameTime);
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
        }
    }
}
