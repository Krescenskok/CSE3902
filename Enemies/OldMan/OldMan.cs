using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint2Final.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final.EnemyAndNPC.OldMan
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class OldMan : INPC
    {

        public INPCState State;
        private OldManNormalSprite OldManSprite;
        private Vector2 OldManPos;
        private NPCCollider OldManCollider;

        public OldMan(Vector2 initialPos, int updatePerFlame)
        {
            OldManPos = initialPos;
            State = new OldManNormalState(this, initialPos);
            OldManSprite =(OldManNormalSprite) EnemySpriteFactory.Instance.CreateOldManSprite();
            OldManCollider = new NPCCollider(OldManSprite.getRectangle(initialPos));
        }

        public void SetSprite(ISprite newSprite)
        {
            OldManSprite = (OldManNormalSprite)newSprite;
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

        public NPCCollider GetCollider()
        {
            return OldManCollider;
        }
    }
}
