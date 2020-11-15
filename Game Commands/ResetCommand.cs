using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint4.Link;
 using Sprint4.Blocks;
using Sprint4.Items;
using Sprint4.EnemyAndNPC;
using System.Diagnostics;

namespace Sprint4
{

    public class ResetCommand : ICommand
    {
        LinkPlayer Player;
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
            LinkCommand linkReset = new LinkCommand(Player, "R");
            Debug.WriteLine("resetting");
            linkReset.Update(Gametime);

            RoomSpawner.Instance.Reset();

        }
    }
}
