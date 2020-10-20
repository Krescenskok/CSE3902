using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2
{
    class ItemsCommand : ICommand
    {
        LinkItems Items;
        bool DoSwap;
        bool GoingForward;
        


        public ItemsCommand(SpriteBatch spriteBatch, LinkItems items, bool doSwap, bool goingForward)
        {
            GoingForward = goingForward;
            DoSwap = doSwap;
            Items = items;
        }

        public void DoInit(Game game)
        {
            //throw new NotImplementedException();
        }

        public void ExecuteCommand(Game game, GameTime gameTime,  SpriteBatch spriteBatch)
        {
            if (!DoSwap)
            {
                
                Items.Draw();
                if (GoingForward)
                {
                    Items.Reset();
                }
            }
            
        }

        public void Update(GameTime GameTime)
        {
            if (DoSwap)
            {
                Items.ChangeState(this.GoingForward);
                Items.Update();
            } else
            {
                Items.Update();
            }
            
  
            
        }
        

    }
}

