using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;
using Sprint5.Menus;
using Sprint5.ScreenHandling.ScreenSprites;
using Sprint5.ScreenHandling;
using System.Collections.Generic;

namespace Sprint5
{
    public class ScreenFactory
    {

        private Texture2D blackScreen;
        private Texture2D mainMenuTexture;
        private Texture2D pauseScreenTexture;
        private Texture2D winScreenTexture;
        private Texture2D statsScreenTexture;
        private Texture2D gameOverScreenTexture;
        private Texture2D creditsScreenTexture;
        private Texture2D soundScreenTexture;
        private Texture2D optionsScreenTexture;
        private Texture2D controlsScreenTexture;

        public List<IScreen> Screens { get; set; }


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

        public void LoadAllTextures(ContentManager content)
        {
            blackScreen = content.Load<Texture2D>("BlackScreen");
            mainMenuTexture = content.Load<Texture2D>("mainMenu/newMenu");
            pauseScreenTexture = content.Load<Texture2D>("pause/PauseScreen");
            winScreenTexture = content.Load<Texture2D>("win/winscreen");
            gameOverScreenTexture = content.Load<Texture2D>("gameover/LOSESCREEN");
            creditsScreenTexture = content.Load<Texture2D>("creditsScreen/CreditsScreen");
            statsScreenTexture = content.Load<Texture2D>("stats/STATSCREEN");
            soundScreenTexture = content.Load<Texture2D>("SoundScreen.png");
            optionsScreenTexture = content.Load<Texture2D>("optionsScreen/OptionsScreen");
            controlsScreenTexture = content.Load<Texture2D>("controls/CONTROLSscreen");

            CreateAllScreenSprites(content);
            
        }
        
        public void CreateAllScreenSprites(ContentManager content)
        {
            PauseScreen.Instance.Sprite = new ScreenSprite(pauseScreenTexture);
            Screens.Add(PauseScreen.Instance);
            CreditsScreen.Instance.Sprite = new ScreenSprite(creditsScreenTexture);
            Screens.Add(PauseScreen.Instance);
            GameOverScreen.Instance.Sprite = new ScreenSprite(gameOverScreenTexture);
            Screens.Add(PauseScreen.Instance);
            StatsScreen.Instance.Sprite = new ScreenSprite(statsScreenTexture);
            Screens.Add(PauseScreen.Instance);
            WinScreen.Instance.Sprite = new ScreenSprite(winScreenTexture);
            Screens.Add(PauseScreen.Instance);
            OptionsScreen.Instance.Sprite = new ScreenSprite(optionsScreenTexture);
            Screens.Add(PauseScreen.Instance);
            ControlsScreen.Instance.Sprite = new ScreenSprite(controlsScreenTexture);
            Screens.Add(PauseScreen.Instance);
            SoundScreen.Instance.Sprite = new ScreenSprite(soundScreenTexture);
            Screens.Add(PauseScreen.Instance);
            MainMenuScreen.Instance.Sprite = new ScreenSprite(mainMenuTexture);
            Screens.Add(PauseScreen.Instance);
            CreateScreenTextures(content);
        }
        public void CreateScreenTextures(ContentManager content)
        {
            foreach(IScreen screen in Screens)
            {
                screen.GenerateOptions(content);
            }

        }

        public ScreenSprite createScreenSprite(Texture2D tex)
        {
            return (new ScreenSprite(tex));    
        }


    }
}

