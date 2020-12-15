using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint5
{
    public class ChangeItemCommand : ICommand
    {
        private bool right;
        private LinkPlayer link;
        private Direction Direction;

        public ChangeItemCommand(Direction dir, LinkPlayer link)
        {
            Direction = dir;
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
                LinkInventory.Instance.MoveCursor(Direction);
                LinkInventory.Instance.UpdateLinkWeapons(link);
            }
        }
    }
}
