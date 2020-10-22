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
        LinkItems Items;
        LinkBlocks Blocks;
        public  ResetCommand(LinkPlayer player,  LinkItems items, LinkBlocks blocks)
        {
            this.Player = player;
            this.Items = items;
            this.Blocks = blocks;
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
            BlocksCommand blockReset = new BlocksCommand(spriteBatch, this.Blocks, false, true);
            ItemsCommand itemReset = new ItemsCommand(spriteBatch, this.Items, false, true);

            linkReset.Update(Gametime);
            blockReset.ExecuteCommand(game, Gametime, spriteBatch);
            itemReset.ExecuteCommand(game, Gametime, spriteBatch);

            

        }
    }
}
