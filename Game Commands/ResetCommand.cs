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

    public class ResetCommand : ICommand
    {
        private LinkPlayer Player;


        public  ResetCommand(LinkPlayer player)
        {
            this.Player = player;
        }

        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
                (game as Game1).State.Id = IGameStates.Type.Gameplay;
                LinkCommand linkReset = new LinkCommand(Player, "R");
                linkReset.Update(Gametime);
                RoomSpawner.Instance.Reset();
        }
    }
}
