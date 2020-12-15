using Microsoft.Xna.Framework;

using System.Collections.Generic;


namespace Sprint5
{
    public interface IScreen
    {
        public void Draw(Game1 game, GameTime gameTime);

        public void Navigate(string action);

        public void Back();

        public void Select();

    }
}
