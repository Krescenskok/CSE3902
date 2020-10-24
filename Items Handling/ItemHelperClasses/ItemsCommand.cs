using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprint3
{
    class ItemsCommand : ICommand
    {
        LinkItems Items;
        bool DoSwap;
        bool GoingForward;
        bool pressed = false;
        bool released = true;


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
            KeyboardState state = Keyboard.GetState();
            if (state.GetPressedKeyCount() > 0)
            {
                pressed = true;
                released = false;
            }
            else
            {
                pressed = false;
                released = true;
            }
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
            if (!pressed && released)
            {
                if (DoSwap)
                {
                    Items.ChangeState(this.GoingForward);
                    Items.Update();
                }
                else
                {
                    Items.Update();
                }
            }
            
  
            
        }
        

    }
}

