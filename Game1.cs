
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

namespace Sprint5
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;
        public MainMenu mainScreen;

        private Vector2 spritePos;

        private List<IController> controllers = new List<IController>();

        private Camera Camera;

        private const int offset = 160;

        private ICommand activeCommand;
        private LinkCommand LinkPersistent;
        private ProjectilesCommand ProjectilePersistent;

        private string difficulty;
        private bool doorPause;

        public Camera GameCamera { get => this.Camera; set => this.Camera = value; }

        public bool DoorPause { get => doorPause; set => doorPause = value; }

        public string Difficulty { get => difficulty; set => difficulty = value; }

        private LinkPlayer linkPlayer;

        private bool Paused;
        public bool IsPaused { get => Paused; set => Paused = value; }

        public bool isPaused;

        public bool mainMenu = true;

        public LinkPlayer LinkPlayer { get => linkPlayer; }
        public SpriteFont Font { get => font; }
        public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

        private bool isGameOver = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Paused = false;
        }

        protected override void Initialize()
        {
            base.Initialize();

        }

        protected override void LoadContent()
        {

            Difficulty = "Normal";
            DifficultyMultiplier.Instance.SetDifficulty(this);

            font = Content.Load<SpriteFont>("File");

            ItemsFactory.Instance.LoadItemsTextures(Content);

            Sounds.Instance.LoadSounds(this);

            SpriteFactory.Instance.LoadAllTextures(Content);

            mainScreen = new MainMenu();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            linkPlayer = new LinkPlayer(this);

            controllers.Add(new GamePadController(linkPlayer, this, _spriteBatch));
            controllers.Add(new KeyboardController(linkPlayer, this, _spriteBatch));
            controllers.Add(new MouseController(this));


            this.Window.Title = "Legend Of Zelda";

            LinkPersistent = new LinkCommand(linkPlayer, "");

            ProjectilePersistent = ProjectilesCommand.Instance;
            ProjectilePersistent.Link = linkPlayer;

            EnemySpriteFactory.Instance.LoadAllTextures(this);
            DoorSpriteFactory.Instance.LoadAllTextures(this);

            //do not move//
            this.Camera = Camera.Instance;
            this.Camera.Load(this);
            //do not move//

            HUD.Instance.LoadHUD(this);
            LinkInventory.Instance.InitializeInventory(this);

            CollisionHandler.Instance.Initialize(this);

            GridGenerator.Instance.GetGrid(this, 12, 7);
            RoomSpawner.Instance.LoadAllRooms(this);
            RoomSpawner.Instance.LoadRoom(this, 1);

            spritePos = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,
            _graphics.GraphicsDevice.Viewport.Height / 2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (linkPlayer.Health == 0 && !linkPlayer.IsDead)
            {
                linkPlayer.IsDead = true;
                activeCommand = new GameOverCommand(linkPlayer);
            }
            else
            {
                foreach (var cont in controllers)
                {
                    activeCommand = cont.HandleInput(this);

                    if (activeCommand != null)
                    {
                        break;
                    }

                }
            }

            GamePadVibrate.Instance.Update(this);

            if (!Paused )
            {
                if (activeCommand != null)
                    activeCommand.Update(gameTime);

                RoomSpawner.Instance.Update();
                LinkPersistent.Update(gameTime);
                ProjectilePersistent.Update(gameTime);
                CollisionHandler.Instance.Update();
                Sounds.Instance.Update();
                HUD.Instance.UpdateHearts(linkPlayer);

                base.Update(gameTime);                
            }            

            this.Camera.Update();
            
        }

        void PrepareToDraw()
        {
            _spriteBatch.Begin(transformMatrix: this.Camera.Transform);
            GraphicsDevice.Viewport = this.Camera.gameView;
            GraphicsDevice.Clear(Color.Black);
        }

        public void switchScreen()
        {
            if (_graphics.IsFullScreen) _graphics.IsFullScreen = false;
            else _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
        }

        protected override void Draw(GameTime gameTime)
        {
            if (!IsGameOver)
            {
                if (mainMenu) 
                {
                    _spriteBatch.Begin();

                    if (activeCommand != null)
                        activeCommand.ExecuteCommand(this, gameTime, _spriteBatch);

                    mainScreen.Draw(_spriteBatch, this, font);
                    base.Draw(gameTime);

                    _spriteBatch.End();
                }
                else
                {
                    _spriteBatch.Begin(transformMatrix: camera.Transform);
                    GraphicsDevice.Viewport = camera.gameView;
                    GraphicsDevice.Clear(Color.Black);

                    if (activeCommand != null)
                        activeCommand.ExecuteCommand(this, gameTime, _spriteBatch);
                    RoomSpawner.Instance.Draw(_spriteBatch);
                    LinkPersistent.ExecuteCommand(this, gameTime, _spriteBatch);
                    RoomSpawner.Instance.DrawTopLayer(_spriteBatch);
                    ProjectilePersistent.ExecuteCommand(this, gameTime, _spriteBatch);
                    base.Draw(gameTime);
                    RoomEnemies.Instance.DrawTests(_spriteBatch);

                    _spriteBatch.End();

                if (Paused && !DoorPause)
                {
                    PauseScreen.Instance.Draw(_spriteBatch, this, font);
                }
                else
                {
                    //Draw HUD in separate viewport
                    _spriteBatch.Begin();

                    GraphicsDevice.Viewport = this.Camera.HUDView;
                    LinkInventory.Instance.Draw(_spriteBatch);
                    HUD.Instance.DrawBottom(_spriteBatch);
                    _spriteBatch.End();
                }
                    }

                }
            }
            else
            {
                _spriteBatch.Begin();

                if (activeCommand != null)
                    activeCommand.ExecuteCommand(this, gameTime, _spriteBatch);

                GameOverScreen.Instance.Draw(_spriteBatch, this, font);

                base.Draw(gameTime);

                _spriteBatch.End();
            }
        }

        public void Pause(bool pause) { if (pause != Paused) { Sounds.Instance.TogglePause(); } Paused = pause; }
    }
}
