using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Items;
using Sprint4Final.Items_Handling.ItemHelperClasses;

namespace Sprint4
{
    public class ItemsFactory
    {
        private Texture2D itemsSpriteSheet;
        private Texture2D particlesSheet;
        private Texture2D explosionSheet;
        private Texture2D wandBeamSheet;
        private Texture2D swordBeamImpactSheet;
        private readonly Vector2 itemSheetSize = new Vector2(12, 8);
        private readonly Vector2 particlesSheetSize = new Vector2(2, 4);
        private readonly Vector2 explosionDimensions = new Vector2(1, 3);
        private readonly Vector2 wandBeamDimensions = new Vector2(4, 2);
        private readonly Vector2 swordBeamDimensions = new Vector2(1, 6);

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
            explosionSheet = content.Load<Texture2D>("explosionRow");
            wandBeamSheet = content.Load<Texture2D>("wandbeam");
            swordBeamImpactSheet = content.Load<Texture2D>("beamImpactRow");
        }

        public Vector2 GetSheetSize()
        {
            return itemSheetSize;
        }

        public Vector2 GetParticlesSheetSize()
        {
            return particlesSheetSize;
        }

        public Vector2 GetExplosionSheetSize()
        {
            return explosionDimensions;
        }

        public Vector2 GetWandBeamSheetSize()
        {
            return wandBeamDimensions;
        }

        public Vector2 GetSwordBeamSheetSize()
        {
            return swordBeamDimensions;
        }

        public ISprite EraseSprite()
        {
            return new ErasedSprite(itemsSpriteSheet);
        }

        public ISprite CreateProjectileImpactSprite()
        {
            return new ProjectileImpactSprite(itemsSpriteSheet);
        }

        public ISprite CreateCandleFireSprite()
        {
            return new CandleFireSprite(particlesSheet);
        }

        public ISprite CreateExplosionSprite()
        {
            return new ExplosionSprite(explosionSheet);
        }

        public ISprite CreateBeamImpactSprite()
        {
            return new BeamImpactSprite(swordBeamImpactSheet);
        }

        public ISprite CreateWandBeamSprite(string direction)
        {
            if (direction.Equals("Right"))
            {
                return new WandBeamRightSprite(wandBeamSheet);
            }
            if (direction.Equals("Left"))
            {
                return new WandBeamLeftSprite(wandBeamSheet);
            }
            if (direction.Equals("Up"))
            {
                return new WandBeamUpSprite(wandBeamSheet);
            }
            else
            {
                return new WandBeamDownSprite(wandBeamSheet);
            }
        }

        public ISprite CreateArrowSprite(string direction)
        {
            if (direction.Equals("Right"))
            {
                return new ArrowRightSprite(itemsSpriteSheet);
            }
            if (direction.Equals("Left"))
            {
                return new ArrowLeftSprite(itemsSpriteSheet);
            }
            if (direction.Equals("Up"))
            {
                return new ArrowUpSprite(itemsSpriteSheet);
            }
            else
            {
                return new ArrowDownSprite(itemsSpriteSheet);
            }
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

        public ISprite CreateMapSprite()
        {
            return new MapSprite(itemsSpriteSheet);
        }        

        public ISprite CreateRupeeSprite()
        {
            return new RupeeSprite(itemsSpriteSheet);
        }

        public ISprite CreateSilverSwordSprite()
        {
            return new SilverSwordSprite(itemsSpriteSheet);
        }

        public ISprite CreateUpBeamSprite()
        {
            return new UpBeamSprite(itemsSpriteSheet);
        }

        public ISprite CreateDownBeamSprite()
        {
            return new DownBeamSprite(itemsSpriteSheet);
        }

        public ISprite CreateLeftBeamSprite()
        {
            return new LeftBeamSprite(itemsSpriteSheet);
        }

        public ISprite CreateRightBeamSprite()
        {
            return new RightBeamSprite(itemsSpriteSheet);
        }

        public ISprite CreateShieldSprite()
        {
            return new ShieldSprite(itemsSpriteSheet);
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
