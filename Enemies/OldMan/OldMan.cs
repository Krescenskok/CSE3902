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
    class OldMan : INPC
    {

        public INPCState State;
        private OldManNormalSprite OldManSprite;
        private Vector2 OldManPos;
        private NPCCollider OldManCollider;

        public OldMan(Vector2 initialPos)
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

        public void Draw(SpriteBatch spriteBatch)
        {
            OldManSprite.Draw(spriteBatch, OldManPos, 0, Color.White);
        }

        public NPCCollider GetCollider()
        {
            return OldManCollider;
        }
    }
}
