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

        ICommand activeCommand;
        LinkCommand LinkPersistent;
        ProjectilesCommand ProjectilePersistent;
       


        LinkPlayer linkPlayer = new LinkPlayer();

       

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



            SpriteFactory.Instance.LoadAllTextures(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            controllers.Add(new KeyboardController(linkPlayer, this, _spriteBatch));
            controllers.Add(new MouseController(this));


            LinkPersistent = new LinkCommand(linkPlayer, "");
            ProjectilePersistent = ProjectilesCommand.Instance;
            ProjectilePersistent.Link = linkPlayer;

            EnemySpriteFactory.Instance.LoadAllTextures(this);

            //set up grid where everything is spawned
            GridGenerator.Instance.GetGrid(this, 12, 7);


            camera = Camera.Instance;
            camera.Load(this);

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
                    ICommand command = cont.HandleInput(this);

                    if (command != null)
                    {
                        activeCommand = command;
                        activeCommand.Update(gameTime);

                        break;
                    }
                    else
                    {
                        activeCommand = null;
                    }

                }
            }


            RoomSpawner.Instance.Update();
            LinkPersistent.Update(gameTime);
            ProjectilePersistent.Update(gameTime);

            CollisionHandler.Instance.Update();

            camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: camera.Transform);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (activeCommand != null)
            {
                activeCommand.ExecuteCommand(this, gameTime, _spriteBatch);
            }

            
            RoomSpawner.Instance.Draw(_spriteBatch);
            LinkPersistent.ExecuteCommand(this, gameTime, _spriteBatch);
            RoomSpawner.Instance.DrawTopLayer(_spriteBatch);

            ProjectilePersistent.ExecuteCommand(this, gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
