using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final.Items;
using Sprint2Final.Link;
using Sprint2Final.Blocks;

namespace Sprint2Final
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
        
        public ISprite CreateLinkSprite()
		{
            
            return new LinkSprite(linkSpriteSheet);
		}
        
		
        public ISprite CreateItemsSprite()
        {
            return new ItemsSprite(itemsSpriteSheet);
        }
        public ISprite CreateBlocksSprite()
        {
            return new BlocksSprite(blocksSpriteSheet);
        }

    }
}
