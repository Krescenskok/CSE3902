using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint4
{
    public class ChangeItemCommand : ICommand
    {
        bool right;
        LinkPlayer link;

        public ChangeItemCommand(bool goingRight, LinkPlayer link)
        {
            right = goingRight;
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
                LinkInventory.Instance.MoveCursor(right);
                LinkInventory.Instance.UpdateLinkWeapons(link);
            }
        }
    }
}
