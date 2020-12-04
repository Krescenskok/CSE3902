/*
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;

namespace Sprint5
{
    public class KeyTranslator
    {
        public KeyTranslator()
        {
            intializeKeys();
        }

        public Dictionary<Keys,Keys> ListKeys { get = listKeys; set = listKeys = value; }

        private static void initiliazeKeys()
        {
            ListKeys.Add(Keys.Q, Keys.Q);
            ListKeys.Add(Keys.A, Keys.A);
            ListKeys.Add(Keys.Left, Keys.Left);
            ListKeys.Add(Keys.P, Keys.P);
            ListKeys.Add(Keys.D, Keys.D);
            ListKeys.Add(Keys.Right, Keys.Right);
            ListKeys.Add(Keys.W, Keys.W);
            ListKeys.Add(Keys.Up, Keys.Up);
            ListKeys.Add(Keys.S, Keys.S);
            ListKeys.Add(Keys.Down, Keys.Down);
            ListKeys.Add(Keys.N, Keys.N);
            ListKeys.Add(Keys.B, Keys.B);
            ListKeys.Add(Keys.Z, Keys.Z);
            ListKeys.Add(Keys.E, Keys.E);
            ListKeys.Add(Keys.LeftShift, Keys.LeftShift);
            ListKeys.Add(Keys.R, Keys.R);
            ListKeys.Add(Keys.Space, Keys.Space);
            ListKeys.Add(Keys.I, Keys.I);
            ListKeys.Add(Keys.U, Keys.U);
            ListKeys.Add(Keys.Enter, Keys.Enter);
            ListKeys.Add(Keys.G, Keys.G);
            ListKeys.Add(Keys.K, Keys.K);
            ListKeys.Add(Keys.J, Keys.J);
            ListKeys.Add(Keys.F, Keys.F);
        }

        public void changeKey(Keys inputKey, Keys outputKey)
        {
            Keys substitute;
            if (ListKeys.ContainsKey(key))
            {
                substitute = findValue(key);
                ListKeys.Remove(key);

            }
            else
            {

            }
        }

        private static Keys findValue(Keys key)
        {
            foreach (KeyValuePair<Keys, Keys> Pair in ListKeys) { if (Pair.Key == key) return Pair.Value; }
            return Keys.zoom;
        }

    }
}
*/