using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class ControlStringList
    {
        private String display;
        private Vector2 position;
        private Keys keyA;
        private Keys keyB;
        private String command;
        private bool selectedA;
        private bool selectedB;
        private Color currentColor1;
        private Color currentColor2;
        public Vector2 Position { get => position; set => position = value; }
        public Keys KeyA { get => keyA; set => keyA = value; }
        public Keys KeyB { get => keyB; set => keyB = value; }
        public String Command { get => command; set => command = value; }
        public Color CurrentColor1 { get => currentColor1; set => currentColor1 = value; }
        public Color CurrentColor2 { get => currentColor2; set => currentColor2 = value; }
        public bool SelectedA { get => selectedA; set => selectedA = value; }
        public bool SelectedB { get => selectedB; set => selectedB = value; }

        public ControlStringList(Keys key1, Keys key2, String sentence, Vector2 location) {
            KeyA = key1;
            KeyB = key2;
            Command = sentence;
            Position = location;
            CurrentColor1 = Color.DarkGoldenrod;
            CurrentColor2 = Color.DarkGoldenrod;
            SelectedA = false;
            SelectedB = false;
        }

        public void Draw(SpriteBatch batch, SpriteFont font)
        {
            if (KeyB != Keys.Zoom)
            {
                if (selectedA) CurrentColor1 = Color.White;
                if (selectedB) CurrentColor2 = Color.White;
                batch.DrawString(font, Command, Position, Color.DarkGoldenrod);
                batch.DrawString(font, " |" + KeyA.ToString(), new Vector2(Position.X + 130, Position.Y), CurrentColor1);
                batch.DrawString(font, "   |", new Vector2(Position.X + 150, Position.Y), Color.DarkGoldenrod);
                batch.DrawString(font, KeyB.ToString(), new Vector2(Position.X + 190, Position.Y), CurrentColor2);
                CurrentColor1 = Color.DarkGoldenrod;
                CurrentColor2 = Color.DarkGoldenrod;
            }
            else
            {
                if (selectedA) CurrentColor1 = Color.White;
                batch.DrawString(font, Command, Position, Color.DarkGoldenrod);
                batch.DrawString(font, " |" + KeyA.ToString(), new Vector2(Position.X + 130, Position.Y), CurrentColor1);
                CurrentColor1 = Color.DarkGoldenrod;
            }
        }

        public void switchSelected()
        {
            if(selectedA){
                selectedA = false;
                selectedB = true;
            }else{
                selectedA = true;
                selectedB = false;
            }
        }
    }
}