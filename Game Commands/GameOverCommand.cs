using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint5.Link;
 using Sprint5.Blocks;
using Sprint5.Items;
using Sprint5.EnemyAndNPC;
using System.Diagnostics;

namespace Sprint5
{

    public class GameOverCommand : ICommand
    {
        private LinkPlayer Player;


        public  GameOverCommand(LinkPlayer player)
        {
            this.Player = player;
        }

        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            (game as Game1).State.Swap(StateId.GameOver);
        }
    }
}
