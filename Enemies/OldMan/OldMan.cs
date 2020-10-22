using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint3.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint3.EnemyAndNPC.OldMan
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class OldMan : IEnemyNPC
    {

        public INPCState State;
        private ISprite OldManSprite;
        private Vector2 OldManPos;
        public OldMan(Vector2 initialPos, int updatePerFlame)
        {
            OldManPos = initialPos;
            State = new OldManNormalState(this, initialPos);
            OldManSprite = EnemySpriteFactory.Instance.CreateOldManSprite();
        }

        public void SetSprite(ISprite newSprite)
        {
            OldManSprite = newSprite;
        }

        public void Die()
        {
        }

        public void Update()
        {
            State.Update();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime time)
        {
            OldManSprite.Draw(spriteBatch, OldManPos, 0, Color.White);
        }

    }
}
