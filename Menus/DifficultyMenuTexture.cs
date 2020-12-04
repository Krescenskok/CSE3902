using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.DifficultyHandling;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum selectedDiff
    {
        Navice,
        Normal,
        Tough,
        Nightmare,
        Return
    }
    public class DifficultyMenuTexture
    {
        private Texture2D texture;

        private static int drawBounds = 0;
        private const float size = 2.5f;
        private selectedDiff currentItem = selectedDiff.Navice;
        private selectedDiff difficulty = selectedDiff.Normal;
        private MainMenu Menu;

        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 navice = new Vector2(20, 185);
        private static Vector2 normal = new Vector2(20, 205);
        private static Vector2 tough = new Vector2(20, 225);
        private static Vector2 nightmare = new Vector2(20, 245);
        private static Vector2 back = new Vector2(20, 265);


        public DifficultyMenuTexture(MainMenu menu, Texture2D text)
        {
            texture = text;
            this.Menu = menu;
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            batch.DrawString(font, "Legend of Zelda", title, Color.Silver, 0, new Vector2(0, 0), size, new SpriteEffects(), 0);

            if (difficulty == selectedDiff.Navice)
            {
                batch.DrawString(font, "selected ->", navice, Color.Black);
                navice.X = navice.X + 100;
            }
            if (currentItem == selectedDiff.Navice) batch.DrawString(font, "Navice", navice, Color.Black);
            else batch.DrawString(font, "Navice", navice, Color.DarkGoldenrod);
            navice = new Vector2(20, 185);

            if (difficulty == selectedDiff.Normal)
            {
                batch.DrawString(font, "selected ->", normal, Color.Black);
                normal.X = normal.X + 100;
            }
            if (currentItem == selectedDiff.Normal) batch.DrawString(font, "Normal", normal, Color.Black);
            else batch.DrawString(font, "Normal", normal, Color.DarkGoldenrod);
            normal = new Vector2(20, 205);

            if (difficulty == selectedDiff.Tough)
            {
                batch.DrawString(font, "selected ->", tough, Color.Black);
                tough.X = tough.X + 100;
            }
            if (currentItem == selectedDiff.Tough) batch.DrawString(font, "Tough", tough, Color.Black);
            else batch.DrawString(font, "Tough", tough, Color.DarkGoldenrod);
            tough = new Vector2(20, 225);

            if (difficulty == selectedDiff.Nightmare)
            {
                batch.DrawString(font, "selected ->", nightmare, Color.Black);
                nightmare.X = nightmare.X + 100;
            }
            if (currentItem == selectedDiff.Nightmare) batch.DrawString(font, "Nightmare", nightmare, Color.Black);
            else batch.DrawString(font, "Nightmare", nightmare, Color.DarkGoldenrod);
            nightmare = new Vector2(20, 245);

            if (currentItem == selectedDiff.Return) batch.DrawString(font, "Return", back, Color.Black);
            else batch.DrawString(font, "Return", back, Color.DarkGoldenrod);

        }
        public void goUp()
        {
            if (currentItem == selectedDiff.Navice) currentItem = selectedDiff.Return;
            else currentItem--;
        }

        public void goDown()
        {
            if (currentItem == selectedDiff.Return) currentItem = selectedDiff.Navice;
            else currentItem++;
        }

        public void select(MainMenu mainScreen)
        {
            if (currentItem != selectedDiff.Return)
            {
                difficulty = currentItem;
                Menu.Game.State.Difficulty = CurrentToLevel(currentItem);
                DifficultyMultiplier.Instance.SetDifficulty(Menu.Game);

            }
            else mainScreen.State = MenuState.main;
        }
        private DifficultyLevel CurrentToLevel(selectedDiff current)
        {
            DifficultyLevel CurrentLevel = Menu.Game.State.Difficulty;
            if (current == selectedDiff.Navice)
            {
                CurrentLevel = DifficultyLevel.Navice;
            }
            else if (current == selectedDiff.Normal)
            {
                CurrentLevel = DifficultyLevel.Normal;
            }
            else if (current == selectedDiff.Tough)
            {
                CurrentLevel = DifficultyLevel.Tough;
            }
            else if (current == selectedDiff.Nightmare)
            {
                CurrentLevel = DifficultyLevel.Nightmare;
            }
            return CurrentLevel;
                
        }
        public selectedDiff CurrentItem { get => currentItem; set => currentItem = value; }
    }
}
