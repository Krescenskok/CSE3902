using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Items;
using Sprint4.Link;
using Sprint4.Blocks;

namespace Sprint4
{
    public class SpriteFactory
    {
        private Texture2D linkSpriteSheet;
        private Texture2D itemsSpriteSheet;
        private Texture2D blocksSpriteSheet;

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
            itemsSpriteSheet = content.Load<Texture2D>("itemSprites");
            blocksSpriteSheet = content.Load<Texture2D>("zelda_tiles_focused");
        }
        
        public LinkSprite CreateLinkSprite()
        {

            return new LinkSprite(linkSpriteSheet);
        }

        public LinkSprite CreateBlueLinkSprite()
        {
            return new LinkSprite(linkBlueSpriteSheet);
        }
        
		
        //public ISprite CreateItemsSprite()
        //{
        //    return new ItemsSprite(itemsSpriteSheet);
        //}
        public ISprite CreateBlocksSprite()
        {
            return new BlocksSprite(blocksSpriteSheet);
        }

    }
}
