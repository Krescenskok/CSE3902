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
        LinkPlayer Player;
        bool start;

        public bool StartAgain { get => start; set => start = value; }

        public  ResetCommand(LinkPlayer player, bool bStart)
        {
            this.Player = player;
            this.StartAgain = bStart;
        }

        public void DoInit(Game game)
        {

        }


        public void Update(GameTime gameTime)
        {

        }

        public void ExecuteCommand(Game game, GameTime Gametime, SpriteBatch spriteBatch)
        {
            if (StartAgain)
            {
                LinkCommand linkReset = new LinkCommand(Player, "R");
                linkReset.Update(Gametime);
                RoomSpawner.Instance.Reset();
                Sounds.Instance.LoadSounds(game);
                Camera.Instance.BackToSquareOne();
            }
           
           

        }
    }
}
