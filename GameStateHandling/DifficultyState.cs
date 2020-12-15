using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.ScreenHandling;

namespace Sprint5.GameStateHandling
{
    public class DifficultyState : IGameStates
    {
        private static readonly DifficultyState instance = new DifficultyState();
        public StateId Id { get; } = StateId.Win;
        public IGameStates Parent { get; set; }

        public IScreen Screen { get; set; } = new DifficultyScreen();

        public static DifficultyState Instance
        {
            get
            {
                return instance;
            }
        }
        private DifficultyState()
        {

        }
        public void Update(Game1 game, GameTime gameTime)
        {
            if (game.ActiveCommand != null)
                game.ActiveCommand.Update(gameTime);

            Sounds.Instance.Update();
        }
        public void Draw(SpriteFont font, Game1 game, GameTime gameTime)
        {
            game.Spritebatch.Begin();
            if (game.ActiveCommand != null)
                game.ActiveCommand.ExecuteCommand(game, gameTime, game.Spritebatch);
            game.Spritebatch.End();
            Screen.Draw(game, gameTime);
        }
    }
}
