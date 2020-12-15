using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint5
{
    public static class HPBarDrawer
    {

        private static readonly List<HPBar> bars = new List<HPBar>();
        private static List<HPBar> removed = new List<HPBar>();

        public static void Draw(SpriteBatch batch)
        {
            foreach(HPBar bar in bars)
            {
                
                bar.Draw(batch);
            }
        }

        public static void Update()
        {
            foreach (HPBar bar in bars)
            {
                bar.Update();
            }

            bars.RemoveAll(removed.Contains);
            removed.Clear();
        }

        public static void AddBar(HPBar bar)
        {
            bars.Add(bar);
        }

        public static void Remove(HPBar bar)
        {
            removed.Add(bar);
            
        }
        
        public static void RemoveAll()
        {
            bars.Clear();
        }
    }
}
