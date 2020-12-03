using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum selectedControl
    {
        Return
    }
    public class ControlMenuTexture
    {
        Texture2D texture;

        private static int drawBounds = 0;
        const float size = 2.5f;
        public selectedControl currentItem = selectedControl.Return;


        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 back = new Vector2(20, 165);

        public ControlMenuTexture(Texture2D text)
        {
            texture = text;
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            batch.DrawString(font, "Legend of Zelda", title, Color.Silver, 0, new Vector2(0, 0), size, new SpriteEffects(), 0);

            batch.DrawString(font, "Return", back, Color.Black);

            batch.DrawString(font, "W and Up - Move Up", new Vector2(20, 185), Color.DarkGoldenrod);
            batch.DrawString(font, "A and Left - Move Left", new Vector2(20, 205), Color.DarkGoldenrod);
            batch.DrawString(font, "S and Down - Move Down", new Vector2(20, 225), Color.DarkGoldenrod);
            batch.DrawString(font, "D and Right - Move Right", new Vector2(20, 245), Color.DarkGoldenrod);
            batch.DrawString(font, "Z and X - Attack", new Vector2(20, 265), Color.DarkGoldenrod);
            batch.DrawString(font, "G - Pause", new Vector2(20, 285), Color.DarkGoldenrod);
            batch.DrawString(font, "Q - Exit Game", new Vector2(20, 305), Color.DarkGoldenrod);
            batch.DrawString(font, "F - Full Screen ", new Vector2(20, 325), Color.DarkGoldenrod);


        }
        public void goUp()
        {
            //only implement if you're insane
        }

        public void goDown()
        {
            //only implement if you're insane
        }

        public void select(MainMenu mainScreen)
        {
            mainScreen.state = MenuState.main;
        }

        public selectedControl CurrentItem { get => currentItem; set => currentItem = value; }
    }
}