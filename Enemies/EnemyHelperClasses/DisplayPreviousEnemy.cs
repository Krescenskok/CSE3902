using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Sprint2
{
    public class DisplayPreviousEnemy : ICommand
    {
  

        private bool alreadyPressedKey;
        private Keys pressedKey;


        public DisplayPreviousEnemy(Keys key)
        {
            pressedKey = key;
        }

        public void DoInit(Game game)
        {
            //do nothing
        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            KeyboardState keys = Keyboard.GetState();


            if (keys.IsKeyDown(pressedKey) && !alreadyPressedKey)
            {

                EnemyNPCDisplay.Instance.PreviousEnemy();

                alreadyPressedKey = true;
            }
            else if (!keys.IsKeyDown(pressedKey))
            {
                alreadyPressedKey = false;
            }


        }

        public void Update(GameTime gameTime)
        {
            
        }
    }
}
