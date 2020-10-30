using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint4.Items;
using Sprint4.Link;
using Sprint4.Blocks;
using Microsoft.VisualBasic.CompilerServices;

namespace Sprint4
{
    public class MouseController : IController
    {
        const int maxRoomNum = 18;
        const int minRoomNum = 1;
        int currentRoomNum;
        MouseState prevState;
        IDictionary<int, ICommand> commandsList = new Dictionary<int, ICommand>();

        public MouseController(Game1 game)
        {
            currentRoomNum = minRoomNum;
            prevState = Mouse.GetState();

        }

        public ICommand HandleInput(Game1 game)
        {
            ICommand changeCommand = null;
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
                changeCommand = new ChangeRoomCommand(currentRoomNum);
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
                changeCommand = new ChangeRoomCommand(currentRoomNum);
            }
            prevState = currentState;

            return changeCommand;
        }
    }
}
