
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5;
using Sprint5.Enemies;
using Sprint5.Blocks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Sprint5.DifficultyHandling;
using Sprint5.Menus;
using Sprint5.GamePadVibration;
using Sprint5.InputHandling;
using Sprint5.GameStateHandling;

namespace Sprint5
{
    public class Game1 : Game
    {
        private Vector2 spritePos;
        public List<IController> Controllers { get; set; }
        private const int offset = 160;
        public MainMenu mainScreen { get; set; }
        public ICommand ActiveCommand { get; set; }
        public LinkCommand LinkPersistent { get; set; }
        public ProjectilesCommand ProjectilePersistent { get; set; }
        public Camera Camera { get; set; }
        public bool DoorPause { get; set; }
        public LinkPlayer LinkPlayer { get; set; }
        public SpriteFont Font { get; set; }
        public GameState State { get; set; }
        public SpriteBatch Spritebatch { get; set; }
        public GraphicsDeviceManager Graphics { get; set; }

        public Game1()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            State = new GameState(this, DifficultyLevel.Normal, MainMenuState.Instance);


            base.Initialize();
        }
        protected override void LoadContent()
        {

            Font = Content.Load<SpriteFont>("8bitFont");

            ItemsFactory.Instance.LoadItemsTextures(Content);

            Sounds.Instance.LoadSounds(this);

            SpriteFactory.Instance.LoadAllTextures(Content);

            Spritebatch = new SpriteBatch(GraphicsDevice);


            LinkPlayer = new LinkPlayer(this);

            DifficultyMultiplier.Instance.SetDifficulty(this);

            mainScreen = new MainMenu(this, LinkPlayer);
            
            MenuCommands.Instance.LoadCommands(this);
            GamePlayCommands.Instance.LoadCommands(this);
            InventoryCommands.Instance.LoadCommands(this);
            PauseCommands.Instance.LoadCommands(this);
            WinLoseCommands.Instance.LoadCommands(this);
            EndMenuCommands.Instance.LoadCommands(this);
            WaitingCommand.Instance.LoadCommands(this);

            Controllers = new List<IController>();
            Controllers.Add(new GamePadController(this));
            Controllers.Add(new KeyboardController(this));
            Controllers.Add(new MouseController(this));

            this.Window.Title = "Legend Of Zelda";

            LinkPersistent = new LinkCommand(LinkPlayer, "");

            ProjectilePersistent = ProjectilesCommand.Instance;
            ProjectilePersistent.Link = LinkPlayer;

            EnemySpriteFactory.Instance.LoadAllTextures(this);
            DoorSpriteFactory.Instance.LoadAllTextures(this);

            //do not move//
            this.Camera = Camera.Instance;
            this.Camera.Load(this);
            //do not move//

            HUD.Instance.LoadHUD(this);
            LinkInventory.Instance.InitializeInventory(this);

            CollisionHandler.Instance.Initialize();

            GridGenerator.Instance.GetGrid(this, 12, 7);
            RoomSpawner.Instance.LoadAllRooms(this);
            RoomSpawner.Instance.LoadRoom(this, 1);

            spritePos = new Vector2(Graphics.GraphicsDevice.Viewport.Width / 2,
            Graphics.GraphicsDevice.Viewport.Height / 2);
        }
        protected override void Update(GameTime gameTime)
        {

            this.State.Update(gameTime);
            this.Camera.Update();
            HPBarDrawer.Update();
            base.Update(gameTime);

        }
        void PrepareToDraw()
        {
            Spritebatch.Begin(transformMatrix: this.Camera.Transform);
            GraphicsDevice.Viewport = this.Camera.gameView;
            GraphicsDevice.Clear(Color.Black);
        }
        public void switchScreen()
        {
            if (Graphics.IsFullScreen) Graphics.IsFullScreen = false;
            else Graphics.IsFullScreen = true;
            Graphics.ApplyChanges();
        }
        protected override void Draw(GameTime gameTime)
        {
            this.State.Draw(Font, gameTime);

            Spritebatch.Begin();
            base.Draw(gameTime);
            Camera.Instance.Draw(Spritebatch);


            Spritebatch.End();

        }
    }
}
