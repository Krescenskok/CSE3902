using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
 using Sprint5.Blocks;
using Sprint5.Items;
using Sprint5.EnemyAndNPC;
using System.Diagnostics;
using Sprint5.DifficultyHandling;

namespace Sprint5
{

    public class ChangeDifficultyCommand : ICommand
    {
        private String directionOfChange;

        

        public  ChangeDifficultyCommand(String direction, Game1 game)
        {
            directionOfChange = direction;
        }

        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            DifficultyMultiplier.Instance.RotateDifficulty(directionOfChange);
           
           

        }
    }
}
