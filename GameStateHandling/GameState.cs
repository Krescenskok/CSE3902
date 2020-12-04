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
        public IGameStates.Type Id { get; set; }

        public IDifficulty.Level Difficulty { get; set; }

        public GameState(Game1 game)
        {
            Game = game;
        }

        public void Update(GameTime gameTime)
        {
            if (Game.LinkPlayer.Health == 0 && !(Game.LinkPlayer.IsDead))
            {
                Game.LinkPlayer.IsDead = true;
                Game.ActiveCommand = new GameOverCommand(Game.LinkPlayer);
                Game.State.Id = IGameStates.Type.GameOver;
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

            if (Id == IGameStates.Type.Gameplay)
            {
                GameplayState.Instance.Update(Game, gameTime);
            }
            else if (Id == IGameStates.Type.Pause)
            {
                PauseState.Instance.Update(Game, gameTime);
            }
            else if (Id == IGameStates.Type.Inventory)
            {
                InventoryState.Instance.Update(Game, gameTime);
            }
            else if (Id == IGameStates.Type.GameOver)
            {
                GameOverState.Instance.Update(Game, gameTime);
            }
            else if (Id == IGameStates.Type.MainMenu)
            {
                MainMenuState.Instance.Update(Game, gameTime);
            }
        }

        public void Draw(SpriteFont font, GameTime gameTime)
        {
            if (Id == IGameStates.Type.Gameplay)
            {
                GameplayState.Instance.Draw(font, Game, gameTime);
            } else if (Id == IGameStates.Type.Pause)
            {
                PauseState.Instance.Draw(font, Game, gameTime);
            } else if (Id == IGameStates.Type.Inventory)
            {
                InventoryState.Instance.Draw(font, Game, gameTime);
            } else if (Id == IGameStates.Type.GameOver)
            {
                GameOverState.Instance.Draw(font, Game, gameTime);
            } else if (Id == IGameStates.Type.MainMenu)
            {
                MainMenuState.Instance.Draw(font, Game, gameTime);
            }
        }
    }
}
