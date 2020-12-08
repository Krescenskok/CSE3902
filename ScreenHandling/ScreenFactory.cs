using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.ScreenHandling.ScreenSprites;

namespace Sprint5
{
    public class ScreenFactory
    {
        private static ScreenFactory instance = new ScreenFactory();

        public static ScreenFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public ScreenFactory()
        {
        }

        public void CreateAllScreens(ContentManager content)
        {
            foreach (ScreenName Name in Enum.GetValues(typeof(ScreenName)))
            {
                Texture2D CurrentTex = content.Load<Texture2D>(ScreenFiles.Instance.SpriteFileMap[Name]);
                ScreenSprite CurrentSprite = new ScreenSprite(CurrentTex, Name);
                ScreenSpriteMap.Instance.Sprite.Add(Name, CurrentSprite);
            }
            
        }
        public void CreateScreenTextures(ContentManager content)
        {
            

        }

        public ScreenSprite createScreenSprite(Texture2D tex, ScreenName name)
        {
            return (new ScreenSprite(tex, name));    
        }


    }
}

