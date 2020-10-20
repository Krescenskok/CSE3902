﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Items;
using Sprint3.Link;
using Sprint3;
using Sprint3.Enemies;
using Sprint3.Blocks;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Linq;

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


        XElement xml;
       

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
            

            //set up room loader
            GridGenerator.Instance.GetGrid(this, 12, 7);
            xml = XElement.Load("../../../PartialLevelOne.xml").Element("Asset");
            List<XElement> rooms = xml.Elements("Room").ToList();

            //load enemies for room 1
            RoomEnemies.Instance.LoadRoom(this, rooms[0]);

            spritePos = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2,
        _graphics.GraphicsDevice.Viewport.Height / 2);
        }

        protected override void Update(GameTime gameTime)
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


           
            LinkPersistent.Update(gameTime);
            ItemPersistent.Update(gameTime);
            BlockPersistent.Update(gameTime);
            CollisionHandler.Instance.Update();

            RoomEnemies.Instance.Update();
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
            
            RoomEnemies.Instance.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);


        }
    }
}
