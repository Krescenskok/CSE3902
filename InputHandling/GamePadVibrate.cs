using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Sprint5;

namespace Sprint5.GamePadVibration
{
    public class GamePadVibrate
    {
        private static readonly GamePadVibrate instance = new GamePadVibrate();

        private int Delay;

        private bool Vibrating;

        public static GamePadVibrate Instance
        {
            get
            {
                return instance;
            }
        }

        private GamePadVibrate()
        {


        }

        public void TakeDamage(String direction)
        {
            if (direction == "Left")
            {
                
                GamePad.SetVibration(PlayerIndex.One, .25f, .25f);
                Delay = 20;
                Vibrating = true;
            } else if (direction == "Right")
            {
                GamePad.SetVibration(PlayerIndex.One, .25f, .25f);
                Delay = 20;
                Vibrating = true;
            } else
            {
                GamePad.SetVibration(PlayerIndex.One, .25f, .25f);
                Delay = 20;
                Vibrating = true;
            }
        }

        public void Update(Game1 game)
        {
            if (game.State.Current.Id == StateId.Pause || game.State.Current.Id == StateId.GameOver)
            {
                GamePad.SetVibration(PlayerIndex.One, 0, 0);
                Vibrating = false;
                Delay = 0;
            }
            else if (Vibrating)
             {
                if (Delay == 0)
                {
                    GamePad.SetVibration(PlayerIndex.One, 0, 0);
                    Vibrating = false;
                }
                else
                {
                    Delay--;
                }

            }
        }
    }
}

