using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies.Zol;
using Sprint3.EnemyAndNPC.AquamentusAndFireballs;
using Sprint3.EnemyAndNPC.Merchant;
using Sprint3.EnemyAndNPC.OldMan;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Sprint3
{
    /// <summary>
    /// <para>Factory for generating Enemy and NPC sprites</para>
    /// <para>Also a database for spritesheet information.</para>
    /// </summary>
    public class EnemySpriteFactory
    {
        private Texture2D texture;

        
        private Texture2D bossTextrue;
        private Texture2D NPCTexture;
        
        private static int[] sheetSize = { 15, 5 };

        private static Dictionary<string, Vector2> coordinateMappings = new Dictionary<string, Vector2>
        {
            {"Stalfos", new Vector2( 2,12)},
            {"RedGoriyaDown", new Vector2( 2,6)},
            {"RedGoriyaUp", new Vector2( 2,7)},
            {"RedGoriyaRight", new Vector2( 2,8)},
            {"RedGoriyaLeft", new Vector2( 2,9)},
            {"Keese", new Vector2( 0,1)},
            {"Gel", new Vector2( 0,6)},
            {"Boomerang", new Vector2( 2,10)},
            {"WallMaster", new Vector2( 2,13)},
            {"Trap", new Vector2(2,14) },
            {"Zol", new Vector2(0,1) },
            {"Rope", new Vector2(2,11) },
            
            {"OldMan", new Vector2(0,0) },
            {"Merchant", new Vector2(0, 2) },
            {"Flame", new Vector2(0, 1) },
            {"Dragon", new Vector2(0,0)},
            {"FireBall", new Vector2 (0, 32) }
        };

      

        public static int GetRow(string spriteName)
        {
            return (int)coordinateMappings[spriteName].Y;
        }

        public static int GetColumn(string spriteName)
        {
            return (int)coordinateMappings[spriteName].X;
        }

        private static EnemySpriteFactory instance = new EnemySpriteFactory();

        public static EnemySpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public static int[] SheetSize()
        {
            return sheetSize;
        }

        private EnemySpriteFactory()
        {

        }

        public void LoadAllTextures(Game1 game)
        {
            texture = game.Content.Load<Texture2D>("EnemySpriteSheet");

            //****
            NPCTexture = game.Content.Load<Texture2D>("NPCSpriteSheet");
            bossTextrue = game.Content.Load<Texture2D>("BossSpriteSheet");
            //****
        }

        public ISprite CreateStalfosWalkingSprite()
        {
            return new StalfosWalkingSprite(texture);
        }

        public ISprite CreateStalfosSpawnSprite(StalfosSpawnState state)
        {
            return new StalfosSpawnSprite(state,texture);
        }

        public ISprite CreateKeeseMoveSprite()
        {
            return new KeeseMoveSprite(texture);
        }

        public ISprite CreateRandomGoriyaSprite()
        {
            Random rand = new Random();
            int num = rand.Next(1, 5);

            if(num == 1)
            {
                return new GoriyaWalkLeftSprite(texture);
            }else if(num == 2)
            {
                return new GoriyaWalkRightSprite(texture);
            }else if(num == 3)
            {
                return new GoriyaWalkUpSprite(texture);

            }
            else
            {
                return new GoriyaWalkDownSprite(texture);
            }

        }

        public ISprite CreateGoriyaWalkingSprite(string direction)
        {
       
            if (direction.Equals("left"))
            {
                return new GoriyaWalkLeftSprite(texture);
            }
            else if (direction.Equals("right"))
            {
                return new GoriyaWalkRightSprite(texture);
            }
            else if (direction.Equals("up"))
            {
                return new GoriyaWalkUpSprite(texture);

            }
            else
            {
                return new GoriyaWalkDownSprite(texture);
            }

        }


        public ISprite CreateBoomerangSprite()
        {
            return new GoriyaBoomerangSprite(texture);
        }

        public ISprite CreateGelMoveSprite()
        {
            return new GelMoveSprite(texture);
        }

        public ISprite CreateWallMasterSprite()
        {
            return new WallMasterSprite(texture);
        }

        public ISprite CreateBladeTrapSprite()
        {
            return new BladeTrapSprite(texture);
        }

        public ISprite CreateZolMoveSprite()
        {
            return new ZolMoveSprite(texture);
        }

        public ISprite CreateRopeMoveSprite()
        {
            return new RopeMoveSprite(texture);
        }




        //*******new***********

        public ISprite CreateFlameSprite()
        {
            return new FlameSprite(NPCTexture);
        }

        public ISprite CreateMerchantSprite()
        {
            return new MerchantSprite(NPCTexture);
        }

        public ISprite CreateOldManSprite()
        {
            return new OldManNormalSprite(NPCTexture);
        }

        public ISprite CreateDragonSprite()
        {
            return new AquamentusNormalSprite(bossTextrue);
        }

        public ISprite CreateFireBall()
        {
            return new FireBallSprite(bossTextrue);
        }

        public ISprite CreateDodongoSprite(string direction)
        {
            if (direction.Equals("Forward"))
            {
                return new DodongoForwardSprite(bossTextrue);
            }
            else if (direction.Equals("Backward"))
            {
                return new DodongoBackwardSprite(bossTextrue);
            }
            else if (direction.Equals("Left"))
            {
                return new DodongoLeftSprite(bossTextrue);
            }
            else if (direction.Equals("Right"))
            {
                return new DodongoRightSprite(bossTextrue);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
