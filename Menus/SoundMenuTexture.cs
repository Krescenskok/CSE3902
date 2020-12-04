using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum selectedSound
    {
        Song1  ,
        Song2,
        Song3,
        Song4,
        Return
    }
    public class SoundMenuTexture
    {
        Texture2D texture;

        private static int drawBounds = 0;
        const float size = 2.5f;
        public selectedSound currentItem = selectedSound.Song1;
        public selectedSound selectedSong = selectedSound.Song1;

        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 S1 = new Vector2(20, 185);
        private static Vector2 S2 = new Vector2(20, 205);
        private static Vector2 S3 = new Vector2(20, 225);
        private static Vector2 S4 = new Vector2(20, 245);
        private static Vector2 back = new Vector2(20, 265);

        private static Dictionary<selectedSound, string> songName = new Dictionary<selectedSound, string>()
        {
            {selectedSound.Song1,"DungeonTheme" },
            {selectedSound.Song2, "TronTheme" },
            {selectedSound.Song3, "DoomTheme" },
            {selectedSound.Song4, "SeptemberTheme" }
        };

        public SoundMenuTexture(Texture2D text)
        {
            texture = text;
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            batch.DrawString(font, "Legend of Zelda", title, Color.Silver, 0, new Vector2(0, 0), size, new SpriteEffects(), 0);

            if (selectedSong == selectedSound.Song1)
            {
                batch.DrawString(font, "selected ->", S1, Color.Black);
                S1!.X = S1!.X + 100;
            }
            if (currentItem == selectedSound.Song1) batch.DrawString(font, "Classic Zelda", S1, Color.Black);
            else batch.DrawString(font, "Classic Zelda", S1, Color.DarkGoldenrod);
            S1 = new Vector2(20, 185);

            if (selectedSong == selectedSound.Song2)
            {
                batch.DrawString(font, "selected ->", S2, Color.Black);
                S2.X = S2.X + 100;
            }
            if (currentItem == selectedSound.Song2) batch.DrawString(font, "Tron: Legacy Theme", S2, Color.Black);
            else batch.DrawString(font, "Tron: Legacy Theme", S2, Color.DarkGoldenrod);
            S2 = new Vector2(20, 205);

            if (selectedSong == selectedSound.Song3)
            {
                batch.DrawString(font, "selected ->", S3, Color.Black);
                S3.X = S3.X + 100;
            }
            if (currentItem == selectedSound.Song3) batch.DrawString(font, "Doom Theme", S3, Color.Black);
            else batch.DrawString(font, "Doom Theme", S3, Color.DarkGoldenrod);
            S3 = new Vector2(20, 225);

            if (selectedSong == selectedSound.Song4)
            {
                batch.DrawString(font, "selected ->", S4, Color.Black);
                S4.X = S4.X + 100;
            }
            if (currentItem == selectedSound.Song4) batch.DrawString(font, "September - Earth, Wind, and Fire", S4, Color.Black);
            else batch.DrawString(font, "September - Earth, Wind, and Fire", S4, Color.DarkGoldenrod);
            S4 = new Vector2(20, 245);

            if (currentItem == selectedSound.Return) batch.DrawString(font, "Return", back, Color.Black);
            else batch.DrawString(font, "Return", back, Color.DarkGoldenrod);

        }
        public void goUp()
        {
            if (currentItem == selectedSound.Song1) currentItem = selectedSound.Return;
            else currentItem--;
        }

        public void goDown()
        {
            if (currentItem == selectedSound.Return) currentItem = selectedSound.Song1;
            else currentItem++;
        }

        public void select(MainMenu mainScreen)
        {
            if (currentItem != selectedSound.Return) selectedSong = currentItem;
            else mainScreen.state = MenuState.main;

            if(songName.ContainsKey(currentItem)) Sounds.Instance.ChangeBGM(songName[currentItem]);

        }
        public selectedSound CurrentItem { get => currentItem; set => currentItem = value; }
    }
}