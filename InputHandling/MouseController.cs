using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Sprint5.Items;
using Sprint5.Link;
using Sprint5.Blocks;
using Microsoft.VisualBasic.CompilerServices;

namespace Sprint5
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
                //Camera.Instance.ScrollDown(2); //for testing, can remove
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
                //Camera.Instance.ScrollUp(3); //for testing, can remove
            }
            prevState = currentState;

            return changeCommand;
        }
    }
}
