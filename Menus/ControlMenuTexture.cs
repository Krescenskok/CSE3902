using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class ControlMenuTexture
    {
        Texture2D texture;
        BlackScreenSprite background;
        Point end;
        private static int drawBounds = 0;
        const float size = 2.5f;
        private ControlStringList[] listOptions;
        private int selected = 0;
        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 back = new Vector2(20, 385);

        public ControlMenuTexture(Texture2D text)
        {
            texture = text;
            end = new Point(260, 320);
            background = SpriteFactory.Instance.CreateBlackScreen();
            initializeItemList();
        }

        public void Draw(SpriteBatch batch, Game1 game, SpriteFont font)
        {
            batch.Draw(texture, new Rectangle(drawBounds, drawBounds, game.Window.ClientBounds.Width, game.Window.ClientBounds.Height), Color.White);
            background.Draw(batch, new Vector2(10,95),end,Color.White);
            batch.DrawString(font, "Legend of Zelda", title, Color.Silver, 0, new Vector2(0, 0), size, new SpriteEffects(), 0);
            if(selected == listOptions.Length) batch.DrawString(font, "Return", back, Color.White);
            else batch.DrawString(font, "Return", back, Color.DarkGoldenrod);
            for (int i = 0; i < listOptions.Length; i++)
            {
                listOptions[i].Draw(batch, font);
            }

        }
        public void goUp()
        {
            if (selected == 0 && listOptions[selected].SelectedA)
            {
                listOptions[selected].SelectedA = false;
                selected = listOptions.Length;
            }
            else {
                if (selected == listOptions.Length)
                {
                    selected--;
                    listOptions[selected].SelectedA = true; 
                }
                else
                {
                    if (listOptions[selected].KeyB == Keys.Zoom)
                    {
                        listOptions[selected].SelectedA = false;
                        selected--;
                        if(listOptions[selected].KeyB == Keys.Zoom) listOptions[selected].SelectedA = true;
                        else listOptions[selected].SelectedB = true;
                    }
                    else
                    {
                        if (listOptions[selected].SelectedA)
                        {
                            listOptions[selected].SelectedA = false;
                            selected--;
                            if (listOptions[selected].KeyB != Keys.Zoom) listOptions[selected].SelectedB = true;
                            else listOptions[selected].SelectedA = true;
                        }
                        else listOptions[selected].switchSelected();
                    }
                }
            }
        }
        public void goDown()
        {
            if (selected == listOptions.Length)
            {
                selected = 0;
                listOptions[selected].SelectedA = true;
            }
            else
            {
                if (listOptions[selected].KeyB == Keys.Zoom)
                {
                    listOptions[selected].SelectedA = false;
                    selected++;
                    if (selected != listOptions.Length) listOptions[selected].SelectedA = true;
                }
                else
                {
                    if (listOptions[selected].SelectedB)
                    {
                        listOptions[selected].SelectedB = false;
                        selected++;
                        listOptions[selected].SelectedA = true;
                    }
                    else listOptions[selected].switchSelected();
                }
            }
        }

        public void select(MainMenu mainScreen)
        {
            if (selected == listOptions.Length) mainScreen.state = MenuState.main;
            //else changeBinding(listOptions, selected);
        }

        public void initializeItemList()
        {
            listOptions = new ControlStringList[14];
            listOptions[0] = new ControlStringList(Keys.W, Keys.Up, "Move Up - ", new Vector2(20, 105));
            listOptions[1] = new ControlStringList(Keys.A, Keys.Left, "Move Left - ", new Vector2(20, 125));
            listOptions[2] = new ControlStringList(Keys.S, Keys.Down, "Move Down - ", new Vector2(20, 145));
            listOptions[3] = new ControlStringList(Keys.D, Keys.Right, "Move Right - ", new Vector2(20, 165));
            listOptions[4] = new ControlStringList(Keys.G, Keys.Zoom, "Pause - ", new Vector2(20, 185));
            listOptions[5] = new ControlStringList(Keys.Q, Keys.Zoom, "Exit Game - ", new Vector2(20, 205));
            listOptions[6] = new ControlStringList(Keys.F, Keys.Zoom, "Full Screen - ", new Vector2(20, 225));
            listOptions[7] = new ControlStringList(Keys.N, Keys.Zoom, "Attack One - ", new Vector2(20, 245));
            listOptions[8] = new ControlStringList(Keys.B, Keys.Zoom, "Attack Two - ", new Vector2(20, 265));
            listOptions[9] = new ControlStringList(Keys.M, Keys.Zoom, "Mute - ", new Vector2(20, 285));
            listOptions[10] = new ControlStringList(Keys.Space, Keys.Zoom, "Inventory - ", new Vector2(20, 305));
            listOptions[11] = new ControlStringList(Keys.U, Keys.Zoom, "Left Inven. - ", new Vector2(20, 325));
            listOptions[12] = new ControlStringList(Keys.I, Keys.Zoom, "Right Inven. - ", new Vector2(20, 345));
            listOptions[13] = new ControlStringList(Keys.Enter, Keys.Zoom, "Select - ", new Vector2(20, 365));
            listOptions[0].SelectedA = true;
        }
    }
}