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
        Texture2D texture;

        private static int drawBounds = 0;
        const float size = 2.5f;
        public selectedMain currentItem = selectedMain.start;

        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 start = new Vector2(20, 185);
        private static Vector2 difficulty = new Vector2(20, 205);
        private static Vector2 colors = new Vector2(20, 225);
        private static Vector2 controls = new Vector2(20, 245);
        private static Vector2 sound = new Vector2(20, 265);
        private static Vector2 quit = new Vector2(20, 285);

        public MainMenuTexture(Texture2D text)
        {
            texture = text;
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            batch.DrawString(font, "Legend of Zelda", title, Color.Silver, 0 ,new Vector2(0,0),size ,new SpriteEffects(),0);

            if(currentItem == selectedMain.start) batch.DrawString(font, "Start", start, Color.Black);
            else batch.DrawString(font, "Start", start, Color.DarkGoldenrod);

            if (currentItem == selectedMain.difficulty) batch.DrawString(font, "Difficulty", difficulty, Color.Black);
            else batch.DrawString(font, "Difficulty", difficulty, Color.DarkGoldenrod);

            if (currentItem == selectedMain.color) batch.DrawString(font, "Link's Outfit", colors, Color.Black);
            else batch.DrawString(font, "Link's Outfits", colors, Color.DarkGoldenrod);

            if (currentItem == selectedMain.controls) batch.DrawString(font, "Controls", controls, Color.Black);
            else batch.DrawString(font, "Controls", controls, Color.DarkGoldenrod);

            if (currentItem == selectedMain.sound) batch.DrawString(font, "Sound", sound, Color.Black);
            else batch.DrawString(font, "Sound", sound, Color.DarkGoldenrod);

            if (currentItem == selectedMain.quit) batch.DrawString(font, "Quit", quit, Color.Black);
            else batch.DrawString(font, "Quit", quit, Color.DarkGoldenrod);

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
            if (currentItem == selectedMain.start) mainScreen.currentGame.mainMenu = false;
            else if (currentItem == selectedMain.difficulty) mainScreen.state = MenuState.difficulty;
            else if (currentItem == selectedMain.color) mainScreen.state = MenuState.color;
            else if (currentItem == selectedMain.controls) mainScreen.state = MenuState.controls;
            else if (currentItem == selectedMain.sound) mainScreen.state = MenuState.sound;
            else mainScreen.currentGame.Exit();
        }
        public Texture2D Texture { get => texture; set => texture = value; }
        public selectedMain CurrentItem { get => currentItem; set => currentItem = value; }
    }
}