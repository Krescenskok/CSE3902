using Sprint5.GameStateHandling;
using Sprint5.ScreenHandling.ScreenSprites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class ScreenSpriteMap
    {
        public IDictionary<ScreenName, ScreenSprite> Sprite { get; set; }

        private static readonly ScreenSpriteMap instance = new ScreenSpriteMap();
        public static ScreenSpriteMap Instance
        {
            get
            {
                return instance;
            }
        }

        private ScreenSpriteMap()
        {
            Sprite = new Dictionary<ScreenName, ScreenSprite>();

        }
        public void MapSprite(ScreenName name, ScreenSprite sprite)
        {
            Sprite.Add(name, sprite);
        }


        public ScreenSprite GetSprite(ScreenName id)
        {
            return Sprite[id];
        }




    }
}
