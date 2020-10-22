using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprint2Final
{
    public interface INPCState
    {
        public void Die();

        public void Update();
    }
}