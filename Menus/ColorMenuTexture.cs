using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public enum selectedColor
    {
        Regular,
        Blue,
        Pink,
        Red,
        Teal,
        Yellow,
        Return
    }
    public class ColorMenuTexture
    {
        Texture2D texture;

        private LinkPlayer link;

        private static int drawBounds = 0;
        const float size = 2.5f;
        public selectedColor currentItem = selectedColor.Regular;
        public selectedColor chosenColor = selectedColor.Regular;

        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 regular = new Vector2(20, 185);
        private static Vector2 blue = new Vector2(20, 205);
        private static Vector2 pink = new Vector2(20, 225);
        private static Vector2 red = new Vector2(20, 245);
        private static Vector2 teal = new Vector2(20, 265);
        private static Vector2 yellow = new Vector2(20, 285);
        private static Vector2 back = new Vector2(20, 305);

        public ColorMenuTexture(Texture2D text, LinkPlayer link)
        {
            texture = text;
            this.link = link;
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {

            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);

            batch.DrawString(font, "Legend of Zelda", title, Color.Silver, 0, new Vector2(0, 0), size, new SpriteEffects(), 0);

            if (chosenColor == selectedColor.Regular)
            {
                batch.DrawString(font, "selected ->", regular, Color.Black);
                regular.X = regular.X + 100;
            }
            if (currentItem == selectedColor.Regular) batch.DrawString(font, "Classic Outfit", regular, Color.Green);
            else batch.DrawString(font, "Classic Outfit", regular, Color.DarkGoldenrod);
            regular = new Vector2(20, 185);

            if (chosenColor == selectedColor.Blue)
            {
                batch.DrawString(font, "selected ->", blue, Color.Black);
                blue.X = blue.X + 100;
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreateBlueLinkSprite();
            }
            if (currentItem == selectedColor.Blue) batch.DrawString(font, "Blue Outfit", blue, Color.Blue);
            else batch.DrawString(font, "Blue Outfit", blue, Color.DarkGoldenrod);
            blue = new Vector2(20, 205);

            if (chosenColor == selectedColor.Pink)
            {
                batch.DrawString(font, "selected ->", pink, Color.Black);
                pink.X = pink.X + 100;
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreatePinkLinkSprite();
            }
            if (currentItem == selectedColor.Pink) batch.DrawString(font, "Pink Outfit", pink, Color.HotPink);
            else batch.DrawString(font, "Pink Outfit", pink, Color.DarkGoldenrod);
            pink = new Vector2(20, 225);

            if (chosenColor == selectedColor.Red)
            {
                batch.DrawString(font, "selected ->", red, Color.Black);
                red.X = red.X + 100;
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreateRedLinkSprite();

            }
            if (currentItem == selectedColor.Red) batch.DrawString(font, "Red Outfit", red, Color.Red);
            else batch.DrawString(font, "Red Outfit", red, Color.DarkGoldenrod);
            red = new Vector2(20, 245);

            if (chosenColor == selectedColor.Teal)
            {
                batch.DrawString(font, "selected ->", teal, Color.Black);
                teal.X = teal.X + 100;
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreateTealLinkSprite();

            }
            if (currentItem == selectedColor.Teal) batch.DrawString(font, "Teal Outfit", teal, Color.Teal);
            else batch.DrawString(font, "Teal Outfit", teal, Color.DarkGoldenrod);
            teal = new Vector2(20, 265);

            if (chosenColor == selectedColor.Yellow)
            {
                batch.DrawString(font, "selected ->", yellow, Color.Black);
                yellow.X = yellow.X + 100;
                link.sprite = (LinkSprite)SpriteFactory.Instance.CreateYellowLinkSprite();

            }
            if (currentItem == selectedColor.Yellow) batch.DrawString(font, "Yellow Outfit", yellow, Color.Yellow);
            else batch.DrawString(font, "Yellow Outfit", yellow, Color.DarkGoldenrod);
            yellow = new Vector2(20, 285);

            if (currentItem == selectedColor.Return) batch.DrawString(font, "Return", back, Color.Black);
            else batch.DrawString(font, "Return", back, Color.DarkGoldenrod);

        }
        public void goUp()
        {
            if (currentItem == selectedColor.Regular) currentItem = selectedColor.Return;
            else currentItem--;
        }

        public void goDown()
        {
            if (currentItem == selectedColor.Return) currentItem = selectedColor.Regular;
            else currentItem++;
        }

        public void select(MainMenu mainScreen)
        {
            if (currentItem != selectedColor.Return) chosenColor = currentItem;
            else mainScreen.state = MenuState.main;
        }
        public selectedColor CurrentItem { get => currentItem; set => currentItem = value; }
    }
}