using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint2Final.Blocks
{
    class BlocksCommand : ICommand
    {
        LinkBlocks Blocks;
        bool DoSwap;
        bool GoingForward;
        


        public BlocksCommand(SpriteBatch spriteBatch,LinkBlocks blocks, bool doSwap, bool goingForward)
        {
            GoingForward = goingForward;
            DoSwap = doSwap;
            Blocks = blocks;
        }

        public void DoInit(Game game)
        {
            //throw new NotImplementedException();
        }

        public void ExecuteCommand(Game game, GameTime gameTime,  SpriteBatch spriteBatch)
        {
            if (!DoSwap)
            {
                
                Blocks.Draw();
                if (GoingForward)
                {
                    Blocks.Reset();
                }
            }
            
        }

        public void Update(GameTime GameTime)
        {
            if (DoSwap)
            {
                Blocks.ChangeState(this.GoingForward);
                Blocks.Update();
            } else
            {
                Blocks.Update();
            }
            
  
            
        }
        

    }
}

