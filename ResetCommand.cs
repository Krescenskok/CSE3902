using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Link;
 using Sprint3.Blocks;
using Sprint3.Items;
using Sprint3.EnemyAndNPC;

namespace Sprint3
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
            
            linkReset.Update(Gametime);

            RoomSpawner.Instance.Reset();

        }
    }
}
