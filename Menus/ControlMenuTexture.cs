using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Sprint5.InputHandling;
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
        private MainMenu Menu;
        private static Vector2 title = new Vector2(225, 10);
        private static Vector2 back = new Vector2(20, 385);

        public ControlMenuTexture(MainMenu menu, Texture2D text)
        {
            texture = text;
            end = new Point(260, 320);
            background = SpriteFactory.Instance.CreateBlackScreen();
            listOptions = UpdatingControls.Instance.initializeItemList(listOptions);
            Menu = menu;
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
            if (selected == listOptions.Length) mainScreen.State = MenuState.main;
            else
            {
                UpdatingControls.Instance.setWait(this);
                UpdatingControls.Instance.waitKey(listOptions[listOptions.Length-1].KeyA);
            }
        }
        public void swappingBindings()
        {
            Keys currentKey;
            Keys keyToChange;
            if (listOptions[selected].SelectedB)
            {
                currentKey = listOptions[selected].KeyB;
                keyToChange = Menu.Game.Controllers[1].getKey();
                if (ChangeBinding.Instance.SwapKeys(currentKey, keyToChange, GamePlayCommands.Instance))
                {
                    for (int i = 0; i < listOptions.Length; i++)
                    {
                        if (listOptions[i].KeyA == keyToChange) listOptions[i].KeyA = currentKey;
                        else if (listOptions[i].KeyB == keyToChange) listOptions[i].KeyB = currentKey;
                    }
                }
                listOptions[selected].KeyB = keyToChange;
            }
            else
            {
                currentKey = listOptions[selected].KeyA;
                keyToChange = Menu.Game.Controllers[1].getKey();
                if (ChangeBinding.Instance.SwapKeys(currentKey, keyToChange, GamePlayCommands.Instance))
                {
                    for (int i = 0; i < listOptions.Length; i++)
                    {
                        if (listOptions[i].KeyA == keyToChange) listOptions[i].KeyA = currentKey;
                        else if (listOptions[i].KeyB == keyToChange) listOptions[i].KeyB = currentKey;
                    }
                }
                listOptions[selected].KeyA = keyToChange;
            }
        }
    }
}