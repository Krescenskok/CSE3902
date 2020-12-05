using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Sprint5.InputHandling
{
    public class UpdatingControls
    {
        private static readonly UpdatingControls instance = new UpdatingControls();
        public bool waiting = false;
        private bool wasWaiting = false;
        public Keys wait = Keys.Zoom;
        private ControlMenuTexture controlMenu;
        public static UpdatingControls Instance
        {
            get
            {
                return instance;
            }
        }
        public UpdatingControls()
        {

        }

        public void Update()
        {
            if(wasWaiting && wasWaiting != waiting)
            {
                controlMenu.swappingBindings();
            }
            wasWaiting = waiting;
        }

        public void setWait(ControlMenuTexture control)
        {
            waiting = true;
            controlMenu = control;
        }

        public void waitKey(Keys key)
        {
            wait = key;
        }
        public ControlStringList[] initializeItemList(ControlStringList[] listOptions)
        {
            listOptions = new ControlStringList[14];
            listOptions[0] = new ControlStringList(Keys.W, Keys.Up, "Move Up  ", new Vector2(20, 105));
            listOptions[1] = new ControlStringList(Keys.A, Keys.Left, "Move Left ", new Vector2(20, 125));
            listOptions[2] = new ControlStringList(Keys.S, Keys.Down, "Move Down ", new Vector2(20, 145));
            listOptions[3] = new ControlStringList(Keys.D, Keys.Right, "Move Right ", new Vector2(20, 165));
            listOptions[4] = new ControlStringList(Keys.G, Keys.Zoom, "Pause ", new Vector2(20, 185));
            listOptions[5] = new ControlStringList(Keys.Q, Keys.Zoom, "Exit Game ", new Vector2(20, 205));
            listOptions[6] = new ControlStringList(Keys.F, Keys.Zoom, "Full Screen ", new Vector2(20, 225));
            listOptions[7] = new ControlStringList(Keys.N, Keys.Zoom, "Attack One ", new Vector2(20, 245));
            listOptions[8] = new ControlStringList(Keys.B, Keys.Zoom, "Attack Two ", new Vector2(20, 265));
            listOptions[9] = new ControlStringList(Keys.M, Keys.Zoom, "Mute ", new Vector2(20, 285));
            listOptions[10] = new ControlStringList(Keys.Space, Keys.Zoom, "Inventory ", new Vector2(20, 305));
            listOptions[11] = new ControlStringList(Keys.U, Keys.Zoom, "Left Inven. ", new Vector2(20, 325));
            listOptions[12] = new ControlStringList(Keys.I, Keys.Zoom, "Right Inven. ", new Vector2(20, 345));
            listOptions[13] = new ControlStringList(Keys.Enter, Keys.Zoom, "Select ", new Vector2(20, 365));
            listOptions[0].SelectedA = true;

            return listOptions;
        }
    }
}
