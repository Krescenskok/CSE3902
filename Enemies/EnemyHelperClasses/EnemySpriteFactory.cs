using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Enemies;
using Sprint5.Enemies.Zol;
using Sprint5.EnemyAndNPC.AquamentusAndFireballs;
using Sprint5.EnemyAndNPC.Merchant;
using Sprint5.EnemyAndNPC.OldMan;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Sprint5
{
    /// <summary>
    /// <para>Factory for generating Enemy and NPC sprites</para>
    /// <para>Also a database for spritesheet information.</para>
    /// </summary>
    public class EnemySpriteFactory
    {
        private Texture2D texture;
        private Texture2D bossTexture;
        private Texture2D NPCTexture;

        private Texture2D HPFill;
        private Texture2D HPBackground;

        string enemyTextureName;
        string bossTextureName;
        string NPCTextureName;
        
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
            {"WallMasterTop", new Vector2( 4,13)},
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

        public void Load(XElement factory)
        {
            List<XElement> coordinates = factory.Elements("Position").ToList();
            foreach(XElement coordinate in coordinates)
            {
                string callName = coordinate.Element("Name").Value;
                string xValue = coordinate.Element("XValue").Value;
                string yValue = coordinate.Element("YValue").Value;
                int x = int.Parse(xValue), y = int.Parse(yValue);
                coordinateMappings.Add(callName, new Vector2(x, y));
            }

            XElement spriteSheetSize = factory.Element("SheetSize");
            int width = int.Parse(spriteSheetSize.Element("Width").Value);
            int height = int.Parse(spriteSheetSize.Element("Height").Value);

            sheetSize = new int[2] { height, width };

            enemyTextureName = factory.Element("EnemySheetName").Value;
            NPCTextureName = factory.Element("NPCSheetName").Value;
            bossTextureName = factory.Element("BossSheetName").Value;

        }

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
            NPCTexture = game.Content.Load<Texture2D>("NPCSpriteSheet");
            bossTexture = game.Content.Load<Texture2D>("BossSpriteSheet");

            HPFill = game.Content.Load<Texture2D>("HPFill");
            HPBackground = game.Content.Load<Texture2D>("HPBackground");
           
        }

        public Texture2D GetHPFill()
        {
            return HPFill;
        }

        public Texture2D GetHPBackground()
        {
            return HPBackground;
        }

        public EnemySprite CreateStalfosWalkingSprite()
        {
            return new StalfosWalkingSprite(texture);
        }

        public EnemySprite CreateKeeseMoveSprite()
        {
            return new KeeseMoveSprite(texture);
        }

        public EnemySprite CreateGoriyaWalkingSprite(string direction)
        {
            string sheetID = "RedGoriya" + char.ToUpper(direction[0]) + direction.Substring(1);
           
            return new GoriyaWalkSprite(texture, sheetID);

        }

        public EnemySprite CreateGoriyaDamagedSprite(string direction)
        {
            string sheetID = "HurtGoriya" + char.ToUpper(direction[0]) + direction.Substring(1);

            return new GoriyaDamagedSprite(texture, sheetID);

        }


        public EnemySprite CreateBoomerangSprite()
        {
            return new GoriyaBoomerangSprite(texture);
        }

        public EnemySprite CreateGelMoveSprite()
        {
            return new GelMoveSprite(texture);
        }

        public EnemySprite CreateWallMasterSprite(string dir)
        {
            string str = "";
            if (dir == "top") str = "Top";
            return new WallMasterSprite(texture, str);
        }

        public EnemySprite CreateWallMasterGrabSprite(string dir)
        {
            string str = "";
            if (dir == "top") str = "Top";
            return new WallMasterGrabbingLinkSprite(texture, str);
        }

        public EnemySprite CreateBladeTrapSprite()
        {
            return new BladeTrapSprite(texture);
        }

        public EnemySprite CreateZolMoveSprite()
        {
            return new ZolMoveSprite(texture);
        }

        public EnemySprite CreateRopeMoveSprite(string dir)
        {
            return new RopeMoveSprite(texture, dir);
        }

        public EnemySprite CreateSpawnSprite()
        {
            return new SpawnSprite(texture);
        }

        public ISprite CreateDyingSprite()
        {
            return new EnemyDeathSprite(texture);
        }

        public EnemySprite CreateStalfosDamagedSprite()
        {
            return new StalfosDamagedSprite(texture);
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

        public EnemySprite CreateDragonSprite()
        {
            return new AquamentusNormalSprite(bossTexture);
        }
        
        public EnemySprite CreateDamagedDragonSprite()
        {
            return new AquamentusDamagedSprite(bossTexture);
        }

        public ISprite CreateFireBall()
        {
            return new FireBallSprite(bossTexture);
        }

        
        public EnemySprite CreateDodongoSprite(string direction)
        {
            return new DodongoMovingSprite(bossTexture, direction);

        }

        public EnemySprite CreateDamagedDodongoSprite(string direction)
        {
            return new DodongoDamagedSprite(bossTexture, direction);
        }
    }
}
