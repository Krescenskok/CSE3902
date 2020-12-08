using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;

namespace Sprint5
{
    public class SpriteFactory
    {
        private Texture2D linkSpriteSheet;
        private Texture2D itemsSpriteSheet;
        private Texture2D blocksSpriteSheet;
        private Texture2D linkBlueRingSpriteSheet;
        private Texture2D blueLinkSpriteSheet;
        private Texture2D redLinkSpriteSheet;
        private Texture2D tealLinkSpriteSheet;
        private Texture2D yellowLinkSpriteSheet;
        private Texture2D pinkLinkSpriteSheet;
        private Texture2D blackScreen;
        private Texture2D mainMenuTexture;
        private Texture2D pauseScreenTexture;
        private Texture2D winScreenTexture;
        private Texture2D statsScreenTexture;
        private Texture2D gameOverScreenTexture;
        private Texture2D creditsScreenTexture;



        private static SpriteFactory instance = new SpriteFactory();

        public static SpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public SpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            linkSpriteSheet = content.Load<Texture2D>("LinkSpriteSheet");
            blueLinkSpriteSheet = content.Load<Texture2D>("BlueLinkSpriteSheet");
            pinkLinkSpriteSheet = content.Load<Texture2D>("PinkLinkSpriteSheet");
            redLinkSpriteSheet = content.Load<Texture2D>("RedLinkSpriteSheet");
            tealLinkSpriteSheet = content.Load<Texture2D>("TealLinkSpriteSheet");
            yellowLinkSpriteSheet = content.Load<Texture2D>("YellowLinkSpriteSheet");

            linkBlueRingSpriteSheet = content.Load<Texture2D>("LinkSpriteSheetBlueRing");
            itemsSpriteSheet = content.Load<Texture2D>("itemSprites");
            blocksSpriteSheet = content.Load<Texture2D>("zelda_tiles_focused");
            blackScreen = content.Load<Texture2D>("BlackScreen");
            mainMenuTexture = content.Load<Texture2D>("MMbackground");
            pauseScreenTexture = content.Load<Texture2D>("PauseScreen");
            winScreenTexture = content.Load<Texture2D>("gameWinScreen");
            gameOverScreenTexture = content.Load<Texture2D>("gameOverScreen");
            creditsScreenTexture = content.Load<Texture2D>("CreditsScreen");
            statsScreenTexture = content.Load<Texture2D>("StatsScreen");
        }
        public LinkSprite CreateLinkSprite()
        {

            return new LinkSprite(linkSpriteSheet);
        }

        public LinkSprite CreateBlueRingLinkSprite()
        {
            return new LinkSprite(linkBlueRingSpriteSheet);
        }
        public LinkSprite CreateBlueLinkSprite()
        {
            return new LinkSprite(blueLinkSpriteSheet);
        }
        public LinkSprite CreatePinkLinkSprite()
        {
            return new LinkSprite(pinkLinkSpriteSheet);
        }
        public LinkSprite CreateRedLinkSprite()
        {
            return new LinkSprite(redLinkSpriteSheet);
        }
        public LinkSprite CreateTealLinkSprite()
        {
            return new LinkSprite(tealLinkSpriteSheet);
        }
        public LinkSprite CreateYellowLinkSprite()
        {
            return new LinkSprite(yellowLinkSpriteSheet);
        }

        public ISprite CreateBlocksSprite()
        {
            return new BlocksSprite(blocksSpriteSheet);
        }


        public MainMenuTexture createMainMenu(MainMenu menu)
        {
                return new MainMenuTexture(menu, mainMenuTexture);           
        }

        public BlackScreenSprite CreateBlackScreen()
        {
            return new BlackScreenSprite(blackScreen);
        }
        }
    }

