﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;
using Sprint3.Link;
using Sprint3;
using Sprint3.Enemies;
using Sprint3.Blocks;

namespace Sprint3
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont font;

        private Vector2 spritePos;



        List<IController> controllers = new List<IController>();

        ICommand activeCommand;
        LinkCommand LinkPersistent;
        ItemsCommand ItemPersistent;
        BlocksCommand BlockPersistent;

        LinkPlayer linkPlayer = new LinkPlayer();

        public Items.LinkItems items;
        public Blocks.LinkBlocks blocks;
        EnemyCollider test;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("File");


            SpriteFactory.Instance.LoadAllTextures(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            items = new Items.LinkItems(_spriteBatch);
            blocks = new Blocks.LinkBlocks(_spriteBatch);

            controllers.Add(new KeyboardController(linkPlayer, this, _spriteBatch));


            LinkPersistent = new LinkCommand(linkPlayer, "");
            ItemPersistent = new ItemsCommand(_spriteBatch, items, false, false);
            BlockPersistent = new BlocksCommand(_spriteBatch, blocks, false, false);

            EnemySpriteFactory.Instance.LoadAllTextures(this);
            EnemyNPCDisplay.Instance.Load(this, new Vector2 ( 220, 220 ), new Vector2 (220, 420));
            spritePos = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,
        _graphics.GraphicsDevice.Viewport.Height / 2);

            CollisionHandler collisionHandler = CollisionHandler.Instance;
            test = new EnemyCollider(new Rectangle(100, 100, 64, 64), new Aquamentus(new Vector2(100, 100)).State, 10);
            collisionHandler.AddCollider(new PlayerCollider(linkPlayer));
        }

        protected override void Update(GameTime gameTime)
        {
            if(linkPlayer.Health == 0)
            {
                activeCommand = new ResetCommand(linkPlayer, items, blocks);
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

                }
            }
            


            EnemyNPCDisplay.Instance.Update();
            LinkPersistent.Update(gameTime);
            ItemPersistent.Update(gameTime);
            BlockPersistent.Update(gameTime);
            CollisionHandler.Instance.Update();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();

            GraphicsDevice.Clear(Color.CornflowerBlue);
            if (activeCommand != null)
            {
                activeCommand.ExecuteCommand(this, gameTime, _spriteBatch);
            }
            LinkPersistent.ExecuteCommand(this, gameTime, _spriteBatch);
            ItemPersistent.ExecuteCommand(this, gameTime, _spriteBatch);
            BlockPersistent.ExecuteCommand(this, gameTime, _spriteBatch);
            EnemyNPCDisplay.Instance.Draw(_spriteBatch, gameTime);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
