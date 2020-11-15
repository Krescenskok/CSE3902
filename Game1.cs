using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Items;
using Sprint4.Link;
using Sprint4;
using Sprint4.Enemies;
using Sprint4.Blocks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Linq;
using Microsoft.Xna.Framework.Input;


namespace Sprint4
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;

        private Vector2 spritePos;

        List<IController> controllers = new List<IController>();

        public Camera camera;

        private const int offset = 160;

        ICommand activeCommand;
        LinkCommand LinkPersistent;
        ProjectilesCommand ProjectilePersistent;
       


        LinkPlayer linkPlayer = new LinkPlayer();
        public bool isPaused = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            base.Initialize();
            CollisionHandler.Instance.Initialize(this);

        }

        protected override void LoadContent()
        {


            font = Content.Load<SpriteFont>("File");

            ItemsFactory.Instance.LoadItemsTextures(Content);

            Sounds.Instance.LoadSounds(this);

            SpriteFactory.Instance.LoadAllTextures(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            controllers.Add(new KeyboardController(linkPlayer, this, _spriteBatch));
            controllers.Add(new MouseController(this));

            LinkPersistent = new LinkCommand(linkPlayer, "");
            ProjectilePersistent = ProjectilesCommand.Instance;
            ProjectilePersistent.Link = linkPlayer;

            EnemySpriteFactory.Instance.LoadAllTextures(this);
            DoorSpriteFactory.Instance.LoadAllTextures(this);

            HUD.Instance.LoadHUD(this);
            LinkInventory.Instance.InitializeInventory(this);


            camera = Camera.Instance;
            camera.Load(this);


            //set up grid where everything is spawned
            GridGenerator.Instance.GetGrid(this, 12, 7);

            //create list of rooms
            RoomSpawner.Instance.LoadAllRooms(this);
            
            RoomSpawner.Instance.LoadRoom(this, 1);

            spritePos = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,
            _graphics.GraphicsDevice.Viewport.Height / 2);


        }

        protected override void Update(GameTime gameTime)
        {
            if(linkPlayer.Health == 0)
            {

                activeCommand = new ResetCommand(linkPlayer);
                activeCommand.Update(gameTime);
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


            if (activeCommand is PauseCommand)
            {
                isPaused = ((PauseCommand)activeCommand).IsPause;
            }

            if (!isPaused && !LinkInventory.Instance.ShowInventory)
            {
                if (activeCommand != null)
                    activeCommand.Update(gameTime);

                RoomSpawner.Instance.Update();
                LinkPersistent.Update(gameTime);
                ProjectilePersistent.Update(gameTime);
                CollisionHandler.Instance.Update();
                Sounds.Instance.Update();
                HUD.Instance.UpdateHearts(linkPlayer);
                HUD.Instance.UpdateInventoryCounts(LinkInventory.Instance.RupeeCount, LinkInventory.Instance.KeyCount, LinkInventory.Instance.BombCount);
                base.Update(gameTime);

            }

            camera.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: camera.Transform);

            GraphicsDevice.Clear(Color.Black);
            if (activeCommand != null)
            {
                activeCommand.ExecuteCommand(this, gameTime, _spriteBatch);
            }

            if (!LinkInventory.Instance.ShowInventory)
            {
                RoomSpawner.Instance.Draw(_spriteBatch);
                LinkPersistent.ExecuteCommand(this, gameTime, _spriteBatch);
                RoomSpawner.Instance.DrawTopLayer(_spriteBatch);
                ProjectilePersistent.ExecuteCommand(this, gameTime, _spriteBatch);
                base.Draw(gameTime);
                _spriteBatch.End();

                _spriteBatch.Begin();
                HUD.Instance.DrawTop(_spriteBatch);
                _spriteBatch.End();
            }
            else
            {
                _spriteBatch.End();

                _spriteBatch.Begin();
                LinkInventory.Instance.Draw(_spriteBatch);
                HUD.Instance.DrawBottom(_spriteBatch);
                _spriteBatch.End();
            }
        }
    }
}
