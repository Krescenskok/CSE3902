using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Items;
using Sprint2.Link;
using Sprint2.Blocks;

namespace Sprint2
{
    public class SpriteFactory
    {
        private Texture2D linkSpriteSheet;
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
            blocksSpriteSheet = content.Load<Texture2D>("zelda_tiles_focused");
        }
        
        public ISprite CreateLinkSprite()
		{            
            return new LinkSprite(linkSpriteSheet);
		}

        public ISprite CreateBlocksSprite()
        {
            return new BlocksSprite(blocksSpriteSheet);
        }

    }
}
