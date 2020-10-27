using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint3.Items;
using Sprint3.Link;
using Sprint3.Blocks;

namespace Sprint3
{
    public class MouseController : IController
    {
        const int maxRoomNum = 19;
        const int minRoomNum = 1;
        int currentRoomNum;
        MouseState prevState;
        IDictionary<Keys, ICommand> commandsList = new Dictionary<Integer, ICommand>();

        public MouseController(Game1 game, SpriteBatch spriteBatch)
        {
            currentRoomNum = minRoomNum;
            prevState = Mouse.GetState();


        }

        public ICommand HandleInput(Game1 game)
        {
            MouseState currentState = Mouse.GetState();
            if (currentState.LeftButton == ButtonState.Pressed && prevState.LeftButton != currentState.LeftButton)
            {
                if (currentRoomNum == minRoomNum)
                {
                    currentRoomNum = maxRoomNum;
                }
                else
                {
                    currentRoomNum--;
                }
            }
            else if (currentState.RightButton == ButtonState.Pressed && prevState.RightButton != currentState.RightButton)
            {
                if (currentRoomNum == maxRoomNum)
                {
                    currentRoomNum = minRoomNum;
                }
                else
                {
                    currentRoomNum++;
                }
            }

            prevState = currentState;

            return 
        }
    }
}
