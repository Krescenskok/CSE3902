using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class ShowInventoryCommand : ICommand
    {
        public ShowInventoryCommand()
        {
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {
            
        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            LinkInventory.Instance.ShowInventory = !LinkInventory.Instance.ShowInventory;
        }
    }
}
