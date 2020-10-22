using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final.EnemyAndNPC.OldMan
{
    /// <summary>
    /// Author: Yuan Hong
    /// </summary>
    class OldManNormalState : INPCState
    {
        private Vector2 OldManPos;
        private OldMan OldManNormal;
        public OldManNormalState(OldMan oldMan, Vector2 initalPos)
        {
            OldManPos = initalPos;
            OldManNormal = oldMan;
        }
        public void Die()
        {
        }


        public void Update()
        {
            //do nothing
        }
    }
}
