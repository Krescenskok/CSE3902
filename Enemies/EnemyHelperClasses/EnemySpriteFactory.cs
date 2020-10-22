using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies;
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
        
        private static int[] sheetSize = { 15, 8 };

        private static Dictionary<string, Vector2> coordinateMappings = new Dictionary<string, Vector2>
        {
            //use texture
            {"Stalfos", new Vector2( 2,12)},
            {"RedGoriyaDown", new Vector2( 4,2)},
            {"RedGoriyaUp", new Vector2( 4,3)},
            {"RedGoriyaRight", new Vector2( 4,4)},
            {"RedGoriyaLeft", new Vector2( 4,5)},
            {"HurtGoriyaDown", new Vector2( 3,2)},
            {"HurtGoriyaUp", new Vector2( 3,3)},
            {"HurtGoriyaRight", new Vector2( 3,4)},
            {"HurtGoriyaLeft", new Vector2( 3,5)},
            {"Keese", new Vector2( 2,0)},
            {"Gel", new Vector2( 0,6)},
            {"Boomerang", new Vector2( 2,10)},
            {"WallMaster", new Vector2( 2,13)},
            {"WallMasterUp", new Vector2( 4,13)},
            {"Trap", new Vector2(0,14) },
            {"Zol", new Vector2(0,0) },
            {"Rope", new Vector2(2,11) },
            {"RopeLeft", new Vector2(4,11) },
            {"Spawn", new Vector2(2,14) },
            {"EnemyDeath", new Vector2(2,6) },
            
            //use bossTexture and NPCTexture
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

        public static EnemySpriteFactory Instance { get; } = new EnemySpriteFactory();

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
                return new GoriyaWalkSprite(texture, "RedGoriyaLeft");
            }
            else if(num == 2)
            {
                return new GoriyaWalkSprite(texture, "RedGoriyaRight");
            }
            else if(num == 3)
            {
                return new GoriyaWalkSprite(texture, "RedGoriyaUp");

            }
            else
            {
                return new GoriyaWalkSprite(texture, "RedGoriyaDown");
            }

        }

        public ISprite CreateGoriyaWalkingSprite(string direction)
        {
            string sheetID = "RedGoriya" + char.ToUpper(direction[0]) + direction.Substring(1);
           
            return new GoriyaWalkSprite(texture, sheetID);

        }

        public ISprite CreateGoriyaDamagedSprite(string direction)
        {
            string sheetID = "HurtGoriya" + char.ToUpper(direction[0]) + direction.Substring(1);

            return new GoriyaDamagedSprite(texture, sheetID);

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

        public ISprite CreateRopeMoveSprite(string dir)
        {
            return new RopeMoveSprite(texture, dir);
        }

        public ISprite CreateSpawnSprite()
        {
            return new SpawnSprite(texture);
        }

        public ISprite CreateDyingSprite()
        {
            return new EnemyDeathSprite(texture);
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
        
        public ISprite CreateDamagedDragonSprite()
        {
            return new AquamentusDamagedSprite(bossTextrue);
        }

        public ISprite CreateFireBall()
        {
            return new FireBallSprite(bossTextrue);
        }

        
        public ISprite CreateDodongoSprite(string direction)
        {
            return new DodongoMovingSprite(bossTextrue, direction);

        }

        public ISprite CreateDamagedDodongoSprite(string direction)
        {
            return new DodongoDamagedSprite(bossTextrue, direction);
        }
    }
}
