using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum selectedMain
    {
        start,
        difficulty,
        color,
        controls,
        sound,

        quit
    }
    public class MainMenuTexture
    {
        private Texture2D texture;

        private static int drawBounds = 0;
        private const float size = 2.5f;
        private selectedMain currentItem = selectedMain.start;

        private MainMenu Menu;

        private static Vector2 title = new Vector2(150, 10);
        private static Vector2 start = new Vector2(500, 185);
        private static Vector2 difficulty = new Vector2(500, 205);
        private static Vector2 colors = new Vector2(500, 225);
        private static Vector2 controls = new Vector2(500, 245);
        private static Vector2 sound = new Vector2(500, 265);
        private static Vector2 quit = new Vector2(500, 285);

        public MainMenuTexture(MainMenu menu, Texture2D text)
        {
            this.Menu = menu;
            texture = text;

        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            batch.DrawString(font, "THE LEGEND OF ZELDA", title, Color.Goldenrod, 0 ,new Vector2(0,0),size ,new SpriteEffects(),0);

            if(currentItem == selectedMain.start) batch.DrawString(font, "START<", start, Color.White);
            else batch.DrawString(font, "START", start, Color.Goldenrod);

            if (currentItem == selectedMain.difficulty) batch.DrawString(font, "DIFFICULTY<", difficulty, Color.White);
            else batch.DrawString(font, "DIFFICULTY", difficulty, Color.Goldenrod);

            if (currentItem == selectedMain.color) batch.DrawString(font, "CUSTOMIZE CHARACTER<", colors, Color.White);
            else batch.DrawString(font, "CUSTOMIZE CHARACTER", colors, Color.Goldenrod);

            if (currentItem == selectedMain.controls) batch.DrawString(font, "CONTROLS<", controls, Color.White);
            else batch.DrawString(font, "CONTROLS", controls, Color.Goldenrod);

            if (currentItem == selectedMain.sound) batch.DrawString(font, "SOUND<", sound, Color.White);
            else batch.DrawString(font, "SOUND", sound, Color.Goldenrod);

            if (currentItem == selectedMain.quit) batch.DrawString(font, "QUIT<", quit, Color.White);
            else batch.DrawString(font, "QUIT", quit, Color.Goldenrod);

        }

        public void goUp()
        {
            if (currentItem == selectedMain.start) currentItem = selectedMain.quit;
            else currentItem--; 
        }

        public void goDown()
        {
            if (currentItem == selectedMain.quit) currentItem = selectedMain.start;
            else currentItem++;
        }

        public void select(MainMenu mainScreen)
        {

            if (currentItem == selectedMain.start) mainScreen.Game.State.Swap(StateId.Gameplay);
            else if (currentItem == selectedMain.difficulty) mainScreen.State = MenuState.difficulty;
            else if (currentItem == selectedMain.controls) mainScreen.State = MenuState.controls;
            else if (currentItem == selectedMain.sound) mainScreen.State = MenuState.sound;
            else if (currentItem == selectedMain.color) mainScreen.State = MenuState.color;
            else mainScreen.Game.Exit();

        }
        public Texture2D Texture { get => texture; set => texture = value; }
        public selectedMain CurrentItem { get => currentItem; set => currentItem = value; }
    }
}