using Microsoft.Xna.Framework;

using System.Collections.Generic;


namespace Sprint5
{
    public interface IScreen
    {
        public void Draw(Game1 game, GameTime gameTime);

        public MenuOption Background { get; set; }


        public List<MenuOption> Options { get; set; }

        public List<MenuOption> Sprites { get; set; }

        public void Navigate(string action);

        public void Back();

        public void Select();

    }
}
