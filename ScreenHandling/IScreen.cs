using Microsoft.Xna.Framework;

using System.Collections.Generic;


namespace Sprint5
{
    public interface IScreen
    {
        public void Draw(Game1 game, GameTime gameTime);

        public ScreenName Background { get; set; }

        public List<ScreenName> Options { get; set; }

        public List<ScreenName> DrawList { get; set; }
    }
}
