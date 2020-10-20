using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint2.Items;
using Sprint2Final.Items_Handling.ItemHelperClasses;

namespace Sprint2
{
    public class ItemsFactory
    {
        private Texture2D itemsSpriteSheet;
        private Texture2D particlesSheet;
        private Texture2D explosionSheet;
        private Vector2 rowsAndColumns = new Vector2(9, 8);
        private Vector2 explosionDimensions = new Vector2(50, 100);

        private static ItemsFactory instance = new ItemsFactory();

        public static ItemsFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public ItemsFactory()
        {

        }

        public void LoadItemsTextures(ContentManager content)
        {
            itemsSpriteSheet = content.Load<Texture2D>("itemSprites");
            particlesSheet = content.Load<Texture2D>("particles");
            explosionSheet = content.Load<Texture2D>("explosion");
        }

        public Vector2 GetSheetSize()
        {
            return rowsAndColumns;
        }

        public Vector2 GetExplosionSheetSize()
        {
            return explosionDimensions;
        }

        public ISprite EraseSprite()
        {
            return new ErasedSprite(itemsSpriteSheet);
        }

        public ISprite CreateExplosionSprite()
        {
            return new ExplosionSprite(explosionSheet);
        }

        public ISprite CreateArrowSprite()
        {
            return new BlueCandleSprite(itemsSpriteSheet);
        }

        public ISprite CreateBlueCandleSprite()
        {
            return new BlueCandleSprite(itemsSpriteSheet);
        }

        public ISprite CreateBluePotionSprite()
        {
            return new BluePotionSprite(itemsSpriteSheet);
        }

        public ISprite CreateBlueRingSprite()
        {
            return new BlueRingSprite(itemsSpriteSheet);
        }

        public ISprite CreateBombSprite()
        {
            return new BombSprite(itemsSpriteSheet);
        }

        public ISprite CreateBoomerangSprite()
        {
            return new BoomerangSprite(itemsSpriteSheet);
        }

        public ISprite CreateBowSprite()
        {
            return new BowSprite(itemsSpriteSheet);
        }

        public ISprite CreateBraceletSprite()
        {
            return new BraceletSprite(itemsSpriteSheet);
        }
        public ISprite CreateClockSprite()
        {
            return new ClockSprite(itemsSpriteSheet);
        }
        public ISprite CreateCompassSprite()
        {
            return new CompassSprite(itemsSpriteSheet);
        }

        public ISprite CreateEmptyHeartSprite()
        {
            return new EmptyHeartSprite(itemsSpriteSheet);
        }

        public ISprite CreateFairySprite()
        {
            return new FairySprite(itemsSpriteSheet);
        }

        public ISprite CreateFancyKeySprite()
        {
            return new FancyKeySprite(itemsSpriteSheet);
        }

        public ISprite CreateFancySwordSprite()
        {
            return new FancySwordSprite(itemsSpriteSheet);
        }

        public ISprite CreateHalfHeartSprite()
        {
            return new HalfHeartSprite(itemsSpriteSheet);
        }

        public ISprite CreateHeartSprite()
        {
            return new HeartSprite(itemsSpriteSheet);
        }

        public ISprite CreateHeartContainerSprite()
        {
            return new HeartContainerSprite(itemsSpriteSheet);
        }

        public ISprite CreateKeySprite()
        {
            return new KeySprite(itemsSpriteSheet);
        }

        public ISprite CreateLetterSprite()
        {
            return new LetterSprite(itemsSpriteSheet);
        }

        public ISprite CreateMagicalBoomerangSprite()
        {
            return new MagicalBoomerangSprite(itemsSpriteSheet);
        }

        public ISprite CreateMagicBookoSprite()
        {
            return new MagicBookSprite(itemsSpriteSheet);
        }

        public ISprite CreateMapSprite()
        {
            return new MapSprite(itemsSpriteSheet);
        }

        public ISprite CreateMeatSprite()
        {
            return new MeatSprite(itemsSpriteSheet);
        }

        public ISprite CreateRaftSprite()
        {
            return new RaftSprite(itemsSpriteSheet);
        }

        public ISprite CreateRecorderSprite()
        {
            return new RecorderSprite(itemsSpriteSheet);
        }

        public ISprite CreateRedCandleSprite()
        {
            return new RedCandleSprite(itemsSpriteSheet);
        }

        public ISprite CreateRedPotionSprite()
        {
            return new RedPotionSprite(itemsSpriteSheet);
        }

        public ISprite CreateRedRingSprite()
        {
            return new RedRingSprite(itemsSpriteSheet);
        }

        public ISprite CreateRupeeSprite()
        {
            return new RupeeSprite(itemsSpriteSheet);
        }

        public ISprite CreateShieldSprite()
        {
            return new ShieldSprite(itemsSpriteSheet);
        }

        public ISprite CreateSilverArrowSprite()
        {
            return new SilverArrowSprite(itemsSpriteSheet);
        }

        public ISprite CreateSilverSwordSprite()
        {
            return new SilverSwordSprite(itemsSpriteSheet);
        }

        public ISprite CreateTriforcePieceSprite()
        {
            return new TriforcePieceSprite(itemsSpriteSheet);
        }

        public ISprite CreateWandSprite()
        {
            return new WandSprite(itemsSpriteSheet);
        }

        public ISprite CreateWoodenSwordSprite()
        {
            return new WoodenSwordSprite(itemsSpriteSheet);
        }

    }
}
