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
        private const int MaxRoomNum = 18;
        private const int MinRoomNum = 1;
        private int CurrentRoomNum;
        private MouseState PrevState;

        public MouseController(Game1 game)
        {
            CurrentRoomNum = MinRoomNum;
            PrevState = Mouse.GetState();

        }

        public void HandleInput(Game1 game)
        {
            ICommand ChangeCommand = null;
            MouseState CurrentState = Mouse.GetState();
            CurrentRoomNum = RoomSpawner.Instance.CurrentRoom;
            if (CurrentState.LeftButton == ButtonState.Pressed && PrevState.LeftButton != CurrentState.LeftButton)
            {
                if (CurrentRoomNum == MinRoomNum)
                {
                    CurrentRoomNum = MaxRoomNum;
                }
                else
                {
                    CurrentRoomNum--;
                }
                ChangeCommand = new ChangeRoomCommand(CurrentRoomNum);
                //Camera.Instance.ScrollDown(2); //for testing, can remove
            }
            else if (CurrentState.RightButton == ButtonState.Pressed && PrevState.RightButton != CurrentState.RightButton)
            {
                if (CurrentRoomNum == MaxRoomNum)
                {
                    CurrentRoomNum = MinRoomNum;
                }
                else
                {
                    CurrentRoomNum++;
                }
                ChangeCommand = new ChangeRoomCommand(CurrentRoomNum);
                //Camera.Instance.ScrollUp(3); //for testing, can remove
            }
            PrevState = CurrentState;

            game.ActiveCommand = ChangeCommand;
        }
    }
}
