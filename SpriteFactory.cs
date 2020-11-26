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
        private Texture2D linkBlueSpriteSheet;

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
        }

        public LinkSprite CreateLinkSprite()
        {

            return new LinkSprite(linkSpriteSheet);
        }

        public LinkSprite CreateBlueRingLinkSprite()
        {
            return new LinkSprite(linkBlueRingSpriteSheet);
        }

        //public LinkSprite CreateBlueLinkSprite()
        //{
        //    return new LinkSprite(linkBlueSpriteSheet);
        //}
        //public LinkSprite CreateBlueLinkSprite()
        //{
        //    return new LinkSprite(linkBlueSpriteSheet);
        //}
        //public LinkSprite CreateBlueLinkSprite()
        //{
        //    return new LinkSprite(linkBlueSpriteSheet);
        //}

        public ISprite CreateBlocksSprite()
        {
            return new BlocksSprite(blocksSpriteSheet);
        }

    }
}