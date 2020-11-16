using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class ConsumeItemCommand : ICommand
    {
        LinkPlayer link;

        public ConsumeItemCommand(LinkPlayer link)
        {
            this.link = link;
        }


        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {
        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            if (LinkInventory.Instance.ShowInventory)
            {
                LinkInventory.Instance.ConsumeItem(link);
            }
        }
    }
}
