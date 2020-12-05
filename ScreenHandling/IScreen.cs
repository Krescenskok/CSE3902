using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.ScreenHandling.ScreenSprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5.ScreenHandling
{
    public interface IScreen
    {
        public void Draw(Game1 game, GameTime gameTime);

        public ScreenSprite Sprite { get; set; }

        public List<ScreenSprite> Options { get; set; }

        public void GenerateOptions(ContentManager content);
    }
}
